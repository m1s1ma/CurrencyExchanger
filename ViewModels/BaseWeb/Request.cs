using System;
using System.Collections.Generic;
using System.Net.Http;


namespace CurrencyExchanger.ViewModels.BaseWeb
{
    public class Request
    {
        public Uri Uri { get; set; }
        public Uri EndPoint { get; set; }
        public HttpMethod Method { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
        public object? Body { get; set; }
    }
}
