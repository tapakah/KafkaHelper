using Confluent.Kafka;
using KafkaHelpers.Helpers;
using KafkaHelpers.Model;
using Newtonsoft.Json.Linq;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace KafkaHelpers
{
	public class KafkaHelper
	{
		private readonly AsyncPolicy _consumingPolicy;
		private static readonly TimeSpan CANCELLING_SEND_TO_KAFKA_TIME = TimeSpan.FromSeconds(3);
		private static string _log = string.Empty;

		public KafkaHelper(AsyncPolicy consumingPolicy)
		{
			_consumingPolicy = consumingPolicy;
		}

		public static IConsumer<TKey, TMessage> CreateKafkaConsumer<TKey, TMessage>(
			string server,
			KafkaSettingEntity setting)
		{
			ConsumerConfig config = GetConsumerConfig(server, setting);
			var consumerBuilder = new ConsumerBuilder<TKey, TMessage>(config);
			return consumerBuilder
					.SetLogHandler((_, logMessage) =>
					{
						LogHelper.AppendLog(logMessage.Message);
					})
					.SetStatisticsHandler((_, jsonStats) => HandleStatistics(jsonStats))

				.Build();
		}

		public static string  GetLog()
		{
			return _log;
		}

		private static void HandleStatistics(string jsonStats)
		{
			// Parse JSON stats
			var stats = JObject.Parse(jsonStats);

			// 1. Broker Connection Metrics
			var brokerName = stats["brokers"]?.First?.Path;
			var brokerOutbufCnt = stats["brokers"]?[brokerName]?["outbuf_cnt"]?.Value<int>() ?? 0;
			var brokerTxBytes = stats["brokers"]?[brokerName]?["txbytes"]?.Value<long>() ?? 0;
			var brokerRxBytes = stats["brokers"]?[brokerName]?["rxbytes"]?.Value<long>() ?? 0;

			Console.WriteLine($"Broker: {brokerName}");
			Console.WriteLine($"Outbound Buffer Count: {brokerOutbufCnt}");
			Console.WriteLine($"Bytes Sent: {brokerTxBytes}");
			Console.WriteLine($"Bytes Received: {brokerRxBytes}");

			// 2. Topic Metrics
			var topicName = "ls-mphub-transactions-cosep";
			var topicMessageRate = stats["topics"]?[topicName]?["msg_rate"]?.Value<double>() ?? 0.0;
			var topicByteRate = stats["topics"]?[topicName]?["msg_size_avg"]?.Value<double>() ?? 0.0;

			Console.WriteLine($"Topic: {topicName}");
			Console.WriteLine($"Message Rate: {topicMessageRate} messages/s");
			Console.WriteLine($"Average Message Size: {topicByteRate} bytes");

			// 3. Partition Metrics
			var partitionId = "0"; // Replace with your partition ID
			var partitionLag = stats["topics"]?[topicName]?["partitions"]?[partitionId]?["consumer_lag"]?.Value<long>() ?? 0;
			var partitionOffset = stats["topics"]?[topicName]?["partitions"]?[partitionId]?["committed_offset"]?.Value<long>() ?? 0;

			Console.WriteLine($"Partition: {partitionId}");
			Console.WriteLine($"Lag: {partitionLag}");
			Console.WriteLine($"Committed Offset: {partitionOffset}");

			// 4. Latency Metrics
			var avgLatency = stats["brokers"]?[brokerName]?["rtt"]?["avg"]?.Value<int>() ?? 0;
			Console.WriteLine($"Average Broker Latency: {avgLatency} ms");

			// 5. Error and Retry Metrics
			var txErrors = stats["brokers"]?[brokerName]?["txerrs"]?.Value<int>() ?? 0;
			var rxErrors = stats["brokers"]?[brokerName]?["rxerrs"]?.Value<int>() ?? 0;

			Console.WriteLine($"Transmit Errors: {txErrors}");
			Console.WriteLine($"Receive Errors: {rxErrors}");

		}

		public static ConsumerConfig GetConsumerConfig(string server, KafkaSettingEntity setting)
		{
			var config = new ConsumerConfig
			{
				BootstrapServers = server,
				GroupId = string.Concat("KafkaHelpers", DateTime.Now.ToString("mmss")),
				AutoOffsetReset = AutoOffsetReset.Earliest,
				EnableAutoCommit = false,
				EnableAutoOffsetStore = false,
				EnablePartitionEof = true,				
				MaxPartitionFetchBytes = 10485880,
				StatisticsIntervalMs = 10000,

				SecurityProtocol = KafkaSettingEntity.SettingToSecurityProtocol(setting.SecurityProtocol),

				Debug = string.IsNullOrEmpty(setting.Debug) ? null : setting.Debug
			};

			if (config.SecurityProtocol == SecurityProtocol.Ssl || config.SecurityProtocol == SecurityProtocol.SaslSsl)
			{
				config.SslCaLocation = setting.SslCaLocation;
				config.SslCertificateLocation = setting.SslCertificateLocation;
				config.SslKeyLocation = setting.SslKeyLocation;
				config.SslKeyPassword = setting.SslKeyPassword;
			}

			if (config.SecurityProtocol == SecurityProtocol.SaslSsl || config.SecurityProtocol == SecurityProtocol.SaslPlaintext)
			{
				config.SaslMechanism = KafkaSettingEntity.SettingToSaslMechanism(setting.SaslMechanism);
				config.SaslUsername = setting.SaslUsername;
				config.SaslPassword = setting.SaslPassword;
			}

			return config;
		}

		public IProducer<TKey, TMessage> CreateKafkaProducer<TKey, TMessage>(string server, KafkaSettingEntity setting)
		{
			ProducerConfig config = GetProducerConfig(server, setting);
			var producerBuilder = new ProducerBuilder<TKey, TMessage>(config);
			return producerBuilder.Build();
		}

		public static ProducerConfig GetProducerConfig(string server, KafkaSettingEntity setting)
		{
			var config =  new ProducerConfig
			{
				BootstrapServers = server,
				MessageMaxBytes = 10485880,
				SecurityProtocol = KafkaSettingEntity.SettingToSecurityProtocol(setting.SecurityProtocol),
				Debug = string.IsNullOrEmpty(setting.Debug) ? null : setting.Debug
			};

			if (config.SecurityProtocol == SecurityProtocol.Ssl || config.SecurityProtocol == SecurityProtocol.SaslSsl)
			{
				config.SslCaLocation = setting.SslCaLocation;
				config.SslCertificateLocation = setting.SslCertificateLocation;
				config.SslKeyLocation = setting.SslKeyLocation;
				config.SslKeyPassword = setting.SslKeyPassword;
			}

			if (config.SecurityProtocol == SecurityProtocol.SaslSsl || config.SecurityProtocol == SecurityProtocol.SaslPlaintext)
			{
				config.SaslMechanism = KafkaSettingEntity.SettingToSaslMechanism(setting.SaslMechanism);
				config.SaslUsername = setting.SaslUsername;
				config.SaslPassword = setting.SaslPassword;
			}

			return config;
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