using CurrencyExchanger.ViewModels.BaseWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchanger.Exceptions
{
    public class APIException : Exception
    {
        public Response Response { get; set; }

        public APIException(Response response) : base("Something wend wrong")
        {
            Response = response;
        }

    }
}
