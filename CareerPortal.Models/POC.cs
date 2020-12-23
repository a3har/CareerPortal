using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerPortal.Models
{
    public class POC
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile URL { get; set; }
    }
}
