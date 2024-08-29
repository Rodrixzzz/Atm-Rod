using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Request
{
    public class RequestLogin
    {
        public int CardNumber { get; set; }
        public int Pin {  get; set; }
    }
}
