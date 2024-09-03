using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Request
{
    public class RequestLogin
    {
        [Required]
        public int CardNumber { get; set; }
        [Required]
        public int Pin {  get; set; }
    }
}
