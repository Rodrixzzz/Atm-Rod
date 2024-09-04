using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Exceptions
{
    public class CustomException: Exception
    {
        public HttpStatusCode statusCode;
        public CustomException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            statusCode = httpStatusCode;
        }
    }
}
