using System;
using System.Threading.Tasks;

using CurrencyExchanger.Exceptions;
using CurrencyExchanger.ExchangersApi.Interfaces;
using CurrencyExchanger.Services.Interfaces;

namespace CurrencyExchanger.Services.Implementation
{
    public class ConverterService : IConverterService
    {
        private IExchangerFactory _exchangersFactory;

        public ConverterService(IExchangerFactory exchangerFactory)
        {
            _exchangersFactory = exchangerFactory;
        }
        public async Task<decimal> GetPrice(string from, string to, decimal number)
        {
            try
            {
                var exchanger = _exchangersFactory.CreateHitBtExchanger();
                var price = await exchanger.GetPrice(from.ToString(), to.ToString());
                return number * price;
            }
            catch(APIException ex)
            {
                Console.WriteLine("Something went wrong");
                return -1;
            }
        }
    }
}
