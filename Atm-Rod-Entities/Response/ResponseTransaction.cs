using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Response
{
    public class ResponseTransaction
    {
        public int AccountID { get; set; }
        public TransacEnum TransacType { get; set; }
        public string TransacTypeDesc { get; set; }
        public float Amount { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ResponseTransaction(Transaction trans)
        {
            AccountID = trans.AccountID;
            TransacType =  (TransacEnum)trans.TransacType;
            TransacTypeDesc = TransacType.ToString();
            Amount = trans.Amount;
            CreatedAt = trans.CreatedAt;
            CreatedBy = trans.CreatedBy;
        }
    }
}
