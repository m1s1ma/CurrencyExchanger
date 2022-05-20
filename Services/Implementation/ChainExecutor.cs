using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CurrencyExchanger.Services.Interfaces;
using CurrencyExchanger.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CurrencyExchanger.Services.Implementation
{
    public class ChainExecutor : IChainExecutor
    {
        private IConverterService _converterService;
        private IConfiguration _configuration;
        public ChainExecutor(IConverterService converterService, IConfiguration configuration)
        {
            _converterService = converterService;
            _configuration = configuration;
        }
        public async Task<decimal> ExecuteChain(string chainCode, decimal number)
        {
            var chainsViewModel = _configuration.Get<ChainsViewModel>();

            if (!chainsViewModel.Paths.Any(x => x.Name == chainCode))
            {
                Console.WriteLine($"Couldn't find path with name {chainCode}");
                return 0;
            }

            var path = chainsViewModel.Paths.First(x => x.Name == chainCode);
            decimal lastResut = number;
            for(int i = 0; i < path.Path.Count(); i++)
            {
                if (i == 0)
                    continue;
                lastResut = await _converterService.GetPrice(path.Path[i - 1], path.Path[i], lastResut);
                if (lastResut == -1)
                    return -1;
            }
            return lastResut;
        }
        
    }
}
