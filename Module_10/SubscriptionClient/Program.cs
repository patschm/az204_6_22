﻿using Azure;
using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace SubscriptionClient
{
    class Program
    {
        static string EndPoint = "zeurbus.servicebus.windows.net";
        static (string Name, string KeY) SasKeyReader = ("Reader", "jodJSDcS+v8xVkYoQz2iCzqsxH0IH0JR4d81J45WdAE=");
        static string TopicName = "topicje";

        static async Task Main(string[] args)
        {
            //await ReadQueueAsync();
            await ReadQueueProcessorAsync();
            Console.WriteLine("Press Enter to Quit");
            Console.ReadLine();
        }

        private static async Task ReadQueueAsync()
        {
            var cred = new AzureNamedKeyCredential(SasKeyReader.Name, SasKeyReader.KeY);
            var client = new ServiceBusClient(EndPoint, cred);
            var receiver = client.CreateReceiver(TopicName, "Support");
            do
            {
                var msg = await receiver.ReceiveMessageAsync();
                Console.WriteLine($"Lock Duration: {msg.LockedUntil} Lock Token: {msg.LockToken}");
                var data = msg.Body.ToString();
                Console.WriteLine(data);
                await receiver.CompleteMessageAsync(msg);
                //await receiver.AbandonMessageAsync(msg);
                //await receiver.RenewMessageLockAsync(msg);
                await Task.Delay(1000);
            }
            while (true);
        }
        private static async Task ReadQueueProcessorAsync()
        {
            var supportReceiver = CreateProcessor("Support"); 
            var salesReceiver = CreateProcessor("Sales");
            var ceoReceiver = CreateProcessor("Management");

            await supportReceiver.StartProcessingAsync();
            await salesReceiver.StartProcessingAsync();
            await ceoReceiver.StartProcessingAsync();
            Console.WriteLine("Press Enter to quit processing");
            Console.ReadLine();
            await supportReceiver.StopProcessingAsync();
            await salesReceiver.StopProcessingAsync();
            await ceoReceiver.StopProcessingAsync();

        }

        private static ServiceBusProcessor CreateProcessor(string subscription)
        {
            var cred = new AzureNamedKeyCredential(SasKeyReader.Name, SasKeyReader.KeY);
            var client = new ServiceBusClient(EndPoint, cred);
            var processor = client.CreateProcessor(TopicName, subscription);
            processor.ProcessMessageAsync += evtArg => {
                Console.WriteLine($"{subscription} receiced: {evtArg.Message.Body.ToString()}");
                //Console.WriteLine(evtArg.Message.Body.ToString());
                return Task.CompletedTask;
            };
            processor.ProcessErrorAsync += evtArg => {
                Console.WriteLine($"{subscription} fout");
                Console.WriteLine(evtArg.Exception.Message);
                return Task.CompletedTask;
            };
            return processor;
        }
    }
}
