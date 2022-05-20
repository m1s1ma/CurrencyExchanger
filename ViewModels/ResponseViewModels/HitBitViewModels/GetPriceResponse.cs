using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchanger.ViewModels.ResponseViewModels
{
    public class GetPriceResponse
    {
        public Dictionary<string, CryptoCurrencyMetadata> CryptoCurrency { get; set; }
    }

    public class CryptoCurrencyMetadata
    {
        public string currency { get; set; }
        public string price { get; set; }
        public string timestamp { get; set; }
    }
}
