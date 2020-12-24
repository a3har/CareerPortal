using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerPortal.Models
{
    public class FileUploadObject
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile URL { get; set; }
        public string Type { get; set; }
    }
}
