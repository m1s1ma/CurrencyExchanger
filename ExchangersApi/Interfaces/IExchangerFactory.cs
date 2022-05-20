using CurrencyExchanger.ExchangersApi.BaseClasses;


namespace CurrencyExchanger.ExchangersApi.Interfaces
{
    public interface IExchangerFactory
    {
        public ApiExchangerBase CreateHitBtExchanger();
    }
}
