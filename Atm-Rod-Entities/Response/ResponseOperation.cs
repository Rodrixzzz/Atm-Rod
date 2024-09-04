using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Response
{
    public class ResponseOperation
    {
        public string OperationType { get; set; }
        public float BeforeBalance { get; set; }
        public float AfterBalance { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
