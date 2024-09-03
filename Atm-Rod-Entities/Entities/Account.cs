using Atm_Rod_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Entity
{
    public class Account: AuditableEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public float? Balance { get; set; }
        public List<Card> Cards { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
