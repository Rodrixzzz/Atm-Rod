using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Response
{
    public class ResponseLogin:ResponseBase
    {
        public string Token { get; set; }

        public int ExpireInMinutes { get; set; }

        public ResponseLogin(string token, int expireInMinutes)
        {
            Token = token;
            ExpireInMinutes = expireInMinutes;
        }
    }
}
