using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using CurrencyExchanger.BaseWeb.Implementation;
using CurrencyExchanger.BaseWeb.Interfaces;
using CurrencyExchanger.Exceptions;
using CurrencyExchanger.ViewModels.BaseWeb;

namespace CurrencyExchanger.ExchangersApi.BaseClasses
{
    public abstract class ApiExchangerBase
    {
        protected Uri _baseUri;
        private IHttpClient _httpClient;

        public abstract Task<decimal> GetPrice(string from, string to);

        internal ApiExchangerBase(Uri baseUri)
        {
            _baseUri = baseUri;
            _httpClient = new NetHttpClient();
        }
        protected Task<T> Get<T>(Uri uri)
        {
            return SendApiRequest<T>(uri, HttpMethod.Get);
        }

        protected Task<Response> Get(Uri uri)
        {
            return SendApiRequestWithFullResponse(uri, HttpMethod.Get);
        }

        protected async Task<T> SendApiRequest<T>(Uri uri, HttpMethod httpMethod,
            IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null,
            object body = null)
        {
            var request = CreateRequest(uri, httpMethod, headers, parameters, body);
            var response = await SendSerializedRequest(request);
            return JsonSerializer.Deserialize<T>(response.Body);

        }

        protected async Task<Response> SendApiRequestWithFullResponse(Uri uri, HttpMethod httpMethod,
            IDictionary<string, string> headers = null, IDictionary<string, string> parameters = null,
             object body = null)
        {
            var request = CreateRequest(uri, httpMethod, headers, parameters, body);
            var response = await SendSerializedRequest(request);
            return response;
        }

        protected async Task<Response> SendSerializedRequest(Request request)
        {
            if (request.Body != null)
            {
                if (request.Body is IDictionary<string, string>)
                {
                    var httpContent = new FormUrlEncodedContent((IDictionary<string, string>)request.Body);
                    httpContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    request.Body = httpContent;
                }
                else
                {
                    var body = JsonSerializer.Serialize(request.Body);
                    request.Body = body;
                }
            }
                
            var response = await DoRequest(request);
            return response;
        }

        protected async Task<Response> DoRequest(Request request)
        {
            var response = await _httpClient.DoRequestWithFullResponse(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new APIException(response);

            return response;
        }

        protected Request CreateRequest(Uri uri, HttpMethod httpMethod, IDictionary<string, string> headers, 
                                    IDictionary<string, string> parameters, object body)
        {
            return new Request
            {
                Uri = _baseUri,
                EndPoint = uri,
                Body = body,
                Headers = headers == null ? new Dictionary<string, string>() : headers,
                Method = httpMethod,
                Parameters = parameters == null ? new Dictionary<string, string>() : parameters
            };
        }
    }
}
