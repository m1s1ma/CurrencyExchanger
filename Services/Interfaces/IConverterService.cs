using System.Threading.Tasks;

namespace CurrencyExchanger.Services.Interfaces
{
    public interface IConverterService
    {
        Task<decimal> GetPrice(string from, string to, decimal number);
    }
}
