using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using CurrencyExchanger.BaseWeb.Interfaces;
using CurrencyExchanger.Extensions;
using CurrencyExchanger.ViewModels.BaseWeb;

namespace CurrencyExchanger.BaseWeb.Implementation
{
    public class NetHttpClient : IHttpClient
    {
        private HttpClient _httpClient;

        public NetHttpClient()
        {
            _httpClient = new();
        }

        public async Task<Response> DoRequestWithFullResponse(Request request)
        {
            var reqMessage = BuildRequestMessage(request);
            var httpResponse = await _httpClient.SendAsync(reqMessage);
            var response = await BuildResponse(httpResponse);
            return response;

        }

        public async Task<T> DoRequest<T>(Request request)
        {
            var response = await DoRequestWithFullResponse(request);
            return JsonSerializer.Deserialize<T>(response.Body);
        }


        private async Task<Response> BuildResponse(HttpResponseMessage responseMsg)
        {
            var body = await responseMsg.Content.ReadAsStringAsync();
            var headers = responseMsg.Headers.ToDictionary(header => header.Key, header => header.Value.First());
            var stastusCode = responseMsg.StatusCode;

            return new Response(headers)
            {
                Uri = responseMsg.Headers.Location,
                ContentType = responseMsg.Content.Headers?.ContentType?.MediaType,
                StatusCode = stastusCode,
                Body = body
            };

        }

        private HttpRequestMessage BuildRequestMessage(Request request)
        {
            var fullUri = new Uri(request.Uri, request.EndPoint).ApplyParameters(request.Parameters);
            var requestMsg = new HttpRequestMessage(request.Method, fullUri);
            foreach (var header in request.Headers)
                requestMsg.Headers.Add(header.Key, header.Value);


            switch (request.Body)
            {
                case string body:
                    requestMsg.Content = new StringContent(body, Encoding.UTF8, "application/json");
                    break;
                case HttpContent body:
                    requestMsg.Content = body;
                    break;
            }
            return requestMsg;
        }

 
    }
}
