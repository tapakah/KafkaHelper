KafkaHelpers

KafkaHelpers is a .NET Framework 4.8 library designed to simplify the interaction with Apache Kafka. It provides helper methods for creating Kafka consumers and producers, consuming messages, sending messages, and retrieving Kafka metrics. Features • Create Kafka consumers and producers with custom configurations. • Consume messages from Kafka topics. • Send messages to Kafka topics. • Retrieve Kafka metrics using the Confluent.Kafka library. • Retrieve Kafka metrics using Jolokia (JMX-to-HTTP bridge). Installation

Clone the repository:

git clone https://github.com/your-repo/KafkaHelpers.git

Open the solution in Visual Studio.

Install the required NuGet packages:

• Confluent.Kafka
• Newtonsoft.Json 
• Polly

You can install these packages via the NuGet Package Manager in Visual Studio or by running the following commands in the Package Manager Console:

Install-Package Confluent.Kafka

Install-Package Newtonsoft.Json

Install-Package Polly
