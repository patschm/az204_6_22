using Azure.Storage.Queues;
using System;
using System.Threading.Tasks;

namespace StorageQueueWriter
{
    class Program
    {
        static string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=psstoor;AccountKey=MRfyiUNhkHADEUvyDeK80HTlvx0hYzgPRn2KPpPH+GSVwFSChGfFiF85bsALodd0M0WWt3DN+F1BnyWaBBuEPw==;EndpointSuffix=core.windows.net";
        static string QueueName = "thequeue";
        static async Task Main(string[] args)
        {
            await WriteToQueueAsync();
            Console.WriteLine("Press Enter to Quit");
            Console.ReadLine();
        }

        private static async Task WriteToQueueAsync()
        {
            var client = new QueueClient(ConnectionString, QueueName);
            for (int i = 0; i < 100; i++)
            {
                await client.SendMessageAsync($"Hello Number {i}");
            }
            
        }

    }
}
