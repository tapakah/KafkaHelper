using Confluent.Kafka;
using KafkaHelpers.Model;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaHelpers
{
    public class KafkaHelper
    {
        private readonly AsyncPolicy _consumingPolicy;
        private static readonly TimeSpan CANCELLING_SEND_TO_KAFKA_TIME = TimeSpan.FromSeconds(3);

        public KafkaHelper(AsyncPolicy consumingPolicy)
        {
            _consumingPolicy = consumingPolicy;
        }

        public static IConsumer<TKey, TMessage> CreateKafkaConsumer<TKey, TMessage>(string server, KafkaSettingEntity setting)
        {
            ConsumerConfig config = GetConsumerConfig(server, setting);
            var consumerBuilder = new ConsumerBuilder<TKey, TMessage>(config);
            return consumerBuilder.Build();
        }

        public static ConsumerConfig GetConsumerConfig(string server, KafkaSettingEntity setting)
        {
            return new ConsumerConfig
            {
                BootstrapServers = server,
                GroupId = string.Concat("KafkaHelpers", DateTime.Now.ToString("mmss")),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                EnablePartitionEof = true,
                Acks = Acks.All,
                MaxPartitionFetchBytes = 10485880,

                SecurityProtocol = KafkaSettingEntity.SettingToSecurityProtocol(setting.SecurityProtocol),
                SslCaLocation = setting.SslCaLocation,
                SslCertificateLocation = setting.SslCertificateLocation,
                SslKeyLocation = setting.SslKeyLocation,
                SslKeyPassword = setting.SslKeyPassword,

                SaslMechanism = KafkaSettingEntity.SettingToSaslMechanism(setting.SaslMechanism),
                SaslUsername = setting.SaslUsername,
                SaslPassword = setting.SaslPassword
            };
        }

        public IProducer<TKey, TMessage> CreateKafkaProducer<TKey, TMessage>(string server, KafkaSettingEntity setting)
        {
            ProducerConfig config = GetProducerConfig(server, setting);
            var producerBuilder = new ProducerBuilder<TKey, TMessage>(config);
            return producerBuilder.Build();
        }

        public static ProducerConfig GetProducerConfig(string server, KafkaSettingEntity setting)
        {
            return new ProducerConfig
            {
                BootstrapServers = server,
                MessageMaxBytes = 10485880,

                SecurityProtocol = KafkaSettingEntity.SettingToSecurityProtocol(setting.SecurityProtocol),
                SslCaLocation = setting.SslCaLocation,
                SslCertificateLocation = setting.SslCertificateLocation,
                SslKeyLocation = setting.SslKeyLocation,
                SslKeyPassword = setting.SslKeyPassword,

                SaslMechanism = KafkaSettingEntity.SettingToSaslMechanism(setting.SaslMechanism),
                SaslUsername = setting.SaslUsername,
                SaslPassword = setting.SaslPassword
            };
        }

        public async Task<ConsumeResult<TKey, TMessage>> ConsumeMessage<TKey, TMessage>(
            IConsumer<TKey, TMessage> consumer,
            CancellationToken stoppingToken
        )
        {
            async Task<ConsumeResult<TKey, TMessage>> ConsumeEvent(CancellationToken cancellationToken)
            {
                for (; ; )
                {
                    ConsumeResult<TKey, TMessage> result = null;

                    cancellationToken.ThrowIfCancellationRequested();
                    result = consumer.Consume(TimeSpan.FromMilliseconds(1));

                    if (result != null && !result.IsPartitionEOF) return result;

                    await Task.Delay(1, cancellationToken);
                }
            }

            return await _consumingPolicy.ExecuteAsync(ConsumeEvent, stoppingToken);
        }

        public bool Send<TKey, TMessage>(string topic, IProducer<TKey, TMessage> kafkaProducer, Message<TKey, TMessage> message, ref CancellationToken stoppingToken)
        {
            void DefferedKafkaCancelAction(CancellationTokenSource cancellingSource)
            {
                cancellingSource.CancelAfter(CANCELLING_SEND_TO_KAFKA_TIME);
            }

            using (CancellationTokenSource cancelKafkaSource = new CancellationTokenSource())
            using (stoppingToken.Register(() => DefferedKafkaCancelAction(cancelKafkaSource)))
            {
                try
                {
                    kafkaProducer.ProduceAsync(topic, message);
                    kafkaProducer.Flush(cancelKafkaSource.Token);
                    return true;
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (System.Exception ex)
                {
                    string logMessage = $"Error of sending to Kafka data.";
                    throw new InvalidOperationException(logMessage, ex);
                }
            }
        }

        public async Task<DeliveryResult<TKey, TMessage>> SendToKafka<TKey, TMessage>(
            TKey key,
            string topic,
            TMessage value,
            IProducer<TKey, TMessage> kafkaProducer,
            CancellationToken stoppingToken)
        {
            Message<TKey, TMessage> message = new Message<TKey, TMessage>
            {
                Key = key,
                Value = value
            };

            try
            {
                return await kafkaProducer.ProduceAsync(topic, message, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                string logMessage = $"Error of sending to Kafka data.";
                throw new InvalidOperationException(logMessage, ex);
            }
        }

        public dynamic GetMessage(string keyType)
        {
            dynamic result;
            switch (keyType)
            {
                case "int":
                    result = new MessageEntity<int>();
                    break;

                case "long":
                    result = new MessageEntity<long>();
                    break;

                case "ignore":
                    result = new MessageEntity<Ignore>();
                    break;

                default:
                    result = new MessageEntity<string>();
                    break;
            };

            return result;
        }

        public dynamic GetDeliviryResult(string keyType)
        {
            dynamic result;
            switch (keyType)
            {
                case "int":
                    result = new DeliveryResult<int, string>();
                    break;

                case "long":
                    result = new DeliveryResult<long, string>();
                    break;

                case "ignore":
                    result = new DeliveryResult<Ignore, string>();
                    break;

                default:
                    result = new DeliveryResult<string, string>();
                    break;
            };

            return result;
        }

        private IProducer<TKey, TValue> CreateProducer<TKey, TValue>(string server, KafkaSettingEntity setting)
        {
            return this.CreateKafkaProducer<TKey, TValue>(server, setting);
        }

        public dynamic GetProducer(string server, string keyType, KafkaSettingEntity setting)
        {
            dynamic producer;
            switch (keyType)
            {
                case "int":
                    producer = CreateProducer<int, string>(server, setting);
                    break;

                case "long":
                    producer = CreateProducer<long, string>(server, setting);
                    break;

                case "ignore":
                    producer = CreateProducer<Null, string>(server, setting);
                    break;

                default:
                    producer = CreateProducer<string, string>(server, setting);
                    break;
            };

            return producer;
        }
    }
}