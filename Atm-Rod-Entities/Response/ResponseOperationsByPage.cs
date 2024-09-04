using Atm_Rod_Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Response
{
    public class ResponseOperationsByPage
    {
        public List<ResponseTransaction> TransactionList { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public ResponseOperationsByPage()
        {
            TransactionList = new List<ResponseTransaction>();
        }
    }
}
