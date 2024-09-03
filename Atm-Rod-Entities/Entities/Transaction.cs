using Atm_Rod_Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Entity
{
    public class Transaction: AuditableEntity
    {
        [ForeignKey("Account")]
        public int AccountID { get; set; }
        public int TransacType { get; set; }
        public float Amount { get; set; }
        public Account Account { get; set; }
    }
}
