using System.ComponentModel.DataAnnotations;

namespace Atm_Rod_Entities.Request
{
    public class RequestOperationsByPage
    {
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        [Required]
        public int CardNumber { get; set; }
    }
}
