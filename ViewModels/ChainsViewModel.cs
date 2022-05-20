using System.Collections.Generic;

namespace CurrencyExchanger.ViewModels
{
    public class ChainsViewModel
    {
        public List<ChainViewModel> Paths { get; set; }
    }

    public class ChainViewModel
    {
        public string Name { get; set; }
        public string[] Path { get; set; }
    }
}
