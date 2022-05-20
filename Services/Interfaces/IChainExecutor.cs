using System.Threading.Tasks;

namespace CurrencyExchanger.Services.Interfaces
{
    public interface IChainExecutor
    {
        Task<decimal> ExecuteChain(string chainCode, decimal number);
    }
}
