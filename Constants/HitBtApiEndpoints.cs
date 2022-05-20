using System;

namespace CurrencyExchanger.Constants
{
    public class HitBtApiEndpoints
    {
        public static readonly Uri HitBitBaseUri = new("https://api.hitbtc.com/");
        public static Uri GetPrice(string from, string to) => new Uri($"/api/3/public/price/rate?from={from}&to={to}", UriKind.Relative);
    }
}