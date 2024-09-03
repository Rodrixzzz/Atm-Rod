using System.ComponentModel.DataAnnotations;

namespace Atm_Rod_Entities.Request
{
    public class RequestOperation
    {
        [Required]
        public int CardNumber {  get; set; }
        [Required]
        public float Amount { get; set; }
    }
}
