using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerPortal.Models
{
    public class Login
    {
        [EmailAddress]
        [Remote(action: "DoesEmailExist", controller: "Validation", ErrorMessage = "Email does not exist")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
