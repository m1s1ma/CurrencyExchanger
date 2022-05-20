using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;

using CurrencyExchanger.Constants;
using CurrencyExchanger.ExchangersApi.BaseClasses;
using CurrencyExchanger.ViewModels.ResponseViewModels;

namespace CurrencyExchanger.ExchangersApi.Implementation
{
    public class HitBitExchanger : ApiExchangerBase
    {
        public HitBitExchanger() : base(HitBtApiEndpoints.HitBitBaseUri) { }

        public override async Task<decimal> GetPrice(string from, string to)
        {
            var response = await Get(HitBtApiEndpoints.GetPrice(from, to));
            var jsonDictionary = JsonSerializer.Deserialize<Dictionary<string, CryptoCurrencyMetadata>>(response.Body);
            var rate = Convert.ToDecimal(jsonDictionary[from].price, new CultureInfo("en-US"));
            return rate;
        }
    }
}
