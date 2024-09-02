using Atm_Rod_Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Entity
{
    public class Card: AuditableEntity
    {
        public int Number { get; set; }
        public int Pin { get; set; }
        public int State { get; set; }
        public int AccountID { get; set; }
        public bool IsBlocked { get; set; }
        public int LoginCounter { get; set; }

    }
}
