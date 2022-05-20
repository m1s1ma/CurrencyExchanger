using CurrencyExchanger.ExchangersApi.BaseClasses;
using CurrencyExchanger.ExchangersApi.Interfaces;

namespace CurrencyExchanger.ExchangersApi.Implementation
{
    public class ExchangersFactory : IExchangerFactory
    {
        public ApiExchangerBase CreateHitBtExchanger()
        {
            return new HitBitExchanger();
        }
    }
}
