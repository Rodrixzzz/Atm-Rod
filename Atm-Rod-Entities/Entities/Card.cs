using Atm_Rod_Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Entity
{
    public class Card: AuditableEntity
    {
        [Required(ErrorMessage = "Card Number is required")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Card Pin is required")]
        public int Pin { get; set; }
        public int State { get; set; }
        [ForeignKey("Account")]
        [Required(ErrorMessage = "AccountID is required")]
        public int AccountID { get; set; }
        public bool? IsBlocked { get; set; }
        public int? LoginCounter { get; set; }
        public Account Account { get; set; }

    }
}
