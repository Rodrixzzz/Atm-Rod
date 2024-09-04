using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Response
{
    public class ResponseBalance
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public float Balance { get; set; }
        public DateTime LastTransaction { get; set; }
    }
}
