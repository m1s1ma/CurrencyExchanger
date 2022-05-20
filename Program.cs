using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using CurrencyExchanger.BaseWeb.Implementation;
using CurrencyExchanger.BaseWeb.Interfaces;
using CurrencyExchanger.ExchangersApi.Implementation;
using CurrencyExchanger.ExchangersApi.Interfaces;
using CurrencyExchanger.Services.Implementation;
using CurrencyExchanger.Services.Interfaces;
using CurrencyExchanger.ViewModels;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CurrencyExchanger
{
    class Program
    {
		static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Console.WriteLine("Enter chain name: ");
            var chainName = Console.ReadLine();
            Console.WriteLine("Enter currency amount: ");
            var stringNumber = Console.ReadLine();

            if(string.IsNullOrEmpty(chainName) || string.IsNullOrEmpty(stringNumber))
            {
                Console.WriteLine("Couldn't process empty values");
                Console.ReadKey();
                return;
            }
                

            decimal number = 0;
            if (stringNumber.Contains(','))
                number = Convert.ToDecimal(stringNumber);
            else
                number = Convert.ToDecimal(stringNumber, new CultureInfo("en-US"));

            IChainExecutor chainExecutor = host.Services.GetRequiredService<IChainExecutor>();
            var res = await chainExecutor.ExecuteChain(chainName, number);
            if(res == -1)
            {
                Console.WriteLine("Something went wrong");
                Console.ReadKey();
                return;
            }
            Console.WriteLine(res.ToString("0.########"));
            Console.ReadKey();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile(GetConfigFilePath());
                })
                .ConfigureServices(services =>
                {
                    services.AddTransient<IChainExecutor, ChainExecutor>();
                    services.AddTransient<IConverterService, ConverterService>();
                    services.AddTransient<IHttpClient, NetHttpClient>();
                    services.AddTransient<IExchangerFactory, ExchangersFactory>();
                    
                });
        }

        private static string GetConfigFilePath()
        {
            var currDir = Directory.GetCurrentDirectory();
            var indexOfBin = currDir.IndexOf("\\bin");
            currDir = currDir.Substring(0, indexOfBin);
            var pathToFile = Path.Combine(currDir, "chains.json");
            return pathToFile;
        }

    }
}
