using Confluent.Kafka;
using System;

namespace KafkaHelpers.Model
{
    public class KafkaSettingEntity
    {
        public string ServerName { get; set; }
        public string SecurityProtocol { get; set; }
        public string SslCaLocation { get; set; }
        public string SslCertificateLocation { get; set; }
        public string SslKeyLocation { get; set; }
        public string SslKeyPassword { get; set; }
        public string SaslMechanism { get; set; }
        public string SaslUsername { get; set; }
        public string SaslPassword { get; set; }
		public string Debug { get; set; }

		public static SecurityProtocol? SettingToSecurityProtocol(string securityProtocolString)
        {
            switch (securityProtocolString)
            {
                case KafkaProtocol.SSL: return Confluent.Kafka.SecurityProtocol.Ssl;
                case KafkaProtocol.PLAINTEXT: return Confluent.Kafka.SecurityProtocol.Plaintext;
                case KafkaProtocol.SASL_SSL: return Confluent.Kafka.SecurityProtocol.SaslSsl;
                case KafkaProtocol.SASL_PLAINTEXT: return Confluent.Kafka.SecurityProtocol.SaslPlaintext;

                default: return null;
            };
        }

        public static SaslMechanism? SettingToSaslMechanism(string mechanism)
        {
            switch (mechanism)
            {
                case KafkaSaslMechanism.GSSAPI: return Confluent.Kafka.SaslMechanism.Gssapi;
                case KafkaSaslMechanism.PLAIN: return Confluent.Kafka.SaslMechanism.Plain;
                case KafkaSaslMechanism.SCRAM_SHA_256: return Confluent.Kafka.SaslMechanism.ScramSha256;
                case KafkaSaslMechanism.SCRAM_SHA_512: return Confluent.Kafka.SaslMechanism.ScramSha512;

                default: return null;
            };
        }
    }

    public static class KafkaProtocol
    {
        public const string PLAINTEXT = "plaintext";
        public const string SSL = "ssl";
        public const string SASL_PLAINTEXT = "sasl_plaintext";
        public const string SASL_SSL = "sasl_ssl";
    }

    public static class KafkaSaslMechanism
    {
        public const string GSSAPI = "GSSAPI";
        public const string PLAIN = "PLAIN";
        public const string SCRAM_SHA_256 = "SCRAM-SHA-256";
        public const string SCRAM_SHA_512 = "SCRAM-SHA-512";
    }


}
