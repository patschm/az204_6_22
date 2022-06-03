using System;
using System.Threading.Tasks;

// New
using NewNS = Azure.Messaging.EventHubs;
using NewPS = Azure.Messaging.EventHubs.Processor;
using NewCS = Azure.Messaging.EventHubs.Consumer;
using System.Text;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs;
using System.Collections.Concurrent;

namespace EvtHubConsumer
{
    class Program
    {
        private const string conStr = "Endpoint=sb://ps-hup.servicebus.windows.net/;SharedAccessKeyName=Lezert;SharedAccessKey=IGDq/XVxblCzDWoKoAZ3aVJ+MP9NL9SnFQYGDDzYgl4=;EntityPath=een-hub";
        private const string hubName = "een-hub";

        private const string checkpointStorage = "DefaultEndpointsProtocol=https;AccountName=psstoor;AccountKey=MRfyiUNhkHADEUvyDeK80HTlvx0hYzgPRn2KPpPH+GSVwFSChGfFiF85bsALodd0M0WWt3DN+F1BnyWaBBuEPw==;EndpointSuffix=core.windows.net";

        static async Task Main(string[] args)
        {
            // Check!! AZ-204 book describes EvenProcessorHost from an obsolete package
            // Use this solution instead
            //await NewStyle();
            await UsingProcessors();
            Console.WriteLine("Started...");
            Console.ReadLine();
        }

        private static async Task UsingProcessors()
        {
            // For checkpoints
            // Checkpoints keep track of what you read (stored in blob)
            // Otherwise you'll see the same events over and over again
            var partitionEventCount = new ConcurrentDictionary<string, int>();
            BlobContainerClient blobContainerClient = new BlobContainerClient(checkpointStorage, "eventhub");
            blobContainerClient.CreateIfNotExists();

            var processor = new EventProcessorClient(blobContainerClient, NewCS.EventHubConsumerClient.DefaultConsumerGroupName, conStr, hubName);
           
            processor.ProcessEventAsync += async partitionEvent => {
                var partID = partitionEvent.Partition.PartitionId;
                Console.WriteLine($"Event Read ({partID}): { Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray()) }");

                // Set new checkpoint
                int eventsSince = partitionEventCount.AddOrUpdate(partID, 1, (str, cnt) => cnt + 1);

                if (eventsSince >= 25)
                {
                    await partitionEvent.UpdateCheckpointAsync();
                    partitionEventCount[partID] = 0;
                }
            };
            processor.ProcessErrorAsync += errorEvent => {
                Console.WriteLine($"Ooops (Partition: {errorEvent.PartitionId}): { errorEvent.Exception.Message}");
                return Task.CompletedTask;
            };

            await processor.StartProcessingAsync();
            Console.WriteLine("Processor is running. Press Enter to stop");
            Console.ReadLine();
            await processor.StopProcessingAsync();

        }

        private static async Task NewStyle()
        {
            await using (var consumerClient = new NewCS.EventHubConsumerClient(
                NewCS.EventHubConsumerClient.DefaultConsumerGroupName, 
                //"extraconsumenten",
                conStr, 
                hubName))
            {
                int eventsRead = 0;
                
                //NewCS.ReadEventOptions opts = new NewCS.ReadEventOptions 
                await foreach (NewCS.PartitionEvent partitionEvent in consumerClient.ReadEventsAsync())
                {
                    Console.WriteLine($"Event Read ({partitionEvent.Partition.PartitionId}): { Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray()) }");
                    eventsRead++;
                }
                Console.WriteLine($"Events read: {eventsRead}");
            }
            
        }

    }
}
