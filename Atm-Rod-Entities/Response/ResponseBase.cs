using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Response
{
    public class ResponseBase
    {
        public int ResponseCode {  get; set; }
        public string Message {  get; set; }
    }
}
