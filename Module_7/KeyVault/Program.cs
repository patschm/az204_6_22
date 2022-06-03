
using System;
using System.Threading.Tasks;

#region KeyVault
using Microsoft.Identity.Client;
#endregion

#region AppConfiguration
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
#endregion


namespace KeyVault
{
    class Program
    {
        static string tenentId = "030b09d5-7f0f-40b0-8c01-03ac319b2d71";
        static string clientId = "28ecd1c7-059d-40a4-86b9-2f04ab4f9178";
        static string clientSecret = "w2i8Q~rabw5wMCRtLRRn5SgUbILL1OE8IUKzNa2Z";
        static string kvUri = "https://ps-sleutelbosje.vault.azure.net/";
        
        static async Task Main(string[] args)
        {
           //await ReadKeyVault();
            await ReadAppConfigurationAsync();

            Console.WriteLine("Done");
            Console.ReadLine();
        }
        private static async Task ReadKeyVault()
        {
            ClientSecretCredential cred = new ClientSecretCredential(tenentId, clientId, clientSecret);
            SecretClient kvClient = new SecretClient(new Uri(kvUri), cred);
                
            var result = await kvClient.GetSecretAsync("Victoria");
            Console.WriteLine($"Hello {result.Value?.Value}");
        }

        private static async Task ReadAppConfigurationAsync()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json")
                   .AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();

           

            //ReadLocal();
            await ReadRemoteAsync();

            void ReadLocal()
            {
                Console.WriteLine(configuration["MySetings:hello"]);
                Console.WriteLine(configuration["ConnectionString"]);
            }

            async Task ReadRemoteAsync()
            {
                builder.AddAzureAppConfiguration(opts => {
                    opts.ConfigureKeyVault(kvopts =>
                    {
                        kvopts.SetCredential(new ClientSecretCredential(tenentId, clientId, clientSecret));
                    }).UseFeatureFlags();
                    opts.Connect(configuration["ConnectionString"]);    
                   
                });
                IConfiguration conf = builder.Build();

                Console.WriteLine($"{conf["Production:Connectionstring"]}");
                Console.WriteLine($"Hello {conf["Sleutel"]}");

                IServiceCollection services = new ServiceCollection();
                services.AddSingleton<IConfiguration>(conf).AddFeatureManagement();

                using (var svcProvider = services.BuildServiceProvider())
                {
                    var featureManager = svcProvider.GetRequiredService<IFeatureManager>();
                    if (await featureManager.IsEnabledAsync("test"))
                    {
                        Console.WriteLine("We have a new feature");
                    }
                }

            }
        }

    }
}
