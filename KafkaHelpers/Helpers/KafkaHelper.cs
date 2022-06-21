using Confluent.Kafka;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaHelpers
{
    public class KafkaHelper
    {
        AsyncPolicy _consumingPolicy;
        private readonly static string KAFKA_GROPUP = string.Concat("KafkaHelpers", DateTime.Now.ToString("mmss"));
        private static readonly TimeSpan CANCELLING_SEND_TO_KAFKA_TIME = TimeSpan.FromSeconds(3);

        public KafkaHelper(AsyncPolicy consumingPolicy)
        {
            _consumingPolicy = consumingPolicy;
        }

        public static IConsumer<string, string> CreateKafkaConsumer(string server)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = server,
                GroupId = KAFKA_GROPUP,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                EnablePartitionEof = true,
                Acks = Acks.All
            };

            var consumerBuilder = new ConsumerBuilder<string, string>(config);

            return consumerBuilder.SetKeyDeserializer(new KeyDeserializer(Encoding.UTF8)).Build();
        }

        public IProducer<long, string> CreateKafkaProducer(string server)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = server
                //Acks = Acks.Leader
                //EnableIdempotence = true,
                //MaxInFlight = 1,
                ///EnableDeliveryReports = true,
                //MessageTimeoutMs = 0,
                //QueueBufferingMaxMessages = 1,
                //EnableBackgroundPoll = true,

            };

            var producerBuilder = new ProducerBuilder<long, string>(config);

            return producerBuilder.Build();
        }

        public async Task<ConsumeResult<string, string>> ConsumeMessage(
            IConsumer<string, string> consumer,
            CancellationToken stoppingToken
        )
        {
            async Task<ConsumeResult<string, string>> ConsumeEvent(CancellationToken cancellationToken)
            {
                for (; ; )
                {
                    ConsumeResult<string, string> result = null;

                    cancellationToken.ThrowIfCancellationRequested();
                    result = consumer.Consume(TimeSpan.FromMilliseconds(1));

                    if (result != null && !result.IsPartitionEOF) return result;

                    await Task.Delay(1, cancellationToken);
                }
            }

            return await _consumingPolicy.ExecuteAsync(ConsumeEvent, stoppingToken);
        }

        public bool Send(string topic, IProducer<long, string> kafkaProducer, Message<long, string> message, ref CancellationToken stoppingToken)
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
        public async Task<bool> SendToKafka(
            string key,
            string topic,
            string value,
            IProducer<long, string> kafkaProducer,
            CancellationToken stoppingToken)
        {
            long.TryParse(key, out long _key);

            Message<long, string> message = new Message<long, string>
            {
                Key = _key,
                Value = value
            };

            return await SendAsync(topic, kafkaProducer, message, stoppingToken);
        }

        public async Task<bool> SendAsync(string topic, IProducer<long, string> kafkaProducer, Message<long, string> message, CancellationToken stoppingToken)
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
                    await kafkaProducer.ProduceAsync(topic, message);
                    //kafkaProducer.Poll(new TimeSpan(0));
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
    }
}
