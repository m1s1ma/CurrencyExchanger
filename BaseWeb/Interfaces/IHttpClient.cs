using System.Threading.Tasks;

using CurrencyExchanger.ViewModels.BaseWeb;


namespace CurrencyExchanger.BaseWeb.Interfaces
{
    public interface IHttpClient
    {
        Task<Response> DoRequestWithFullResponse(Request request);
        Task<T> DoRequest<T>(Request request);
    }
}
