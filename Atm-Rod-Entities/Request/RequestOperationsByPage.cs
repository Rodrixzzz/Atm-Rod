﻿using System.ComponentModel.DataAnnotations;

namespace Atm_Rod_Entities.Request
{
    public class RequestOperationsByPage
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        [Required]
        public int CardNumber { get; set; }
    }
}
