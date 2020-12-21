using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerPortal.Models
{

    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public long PhoneNumber { get; set; }
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller:"Validation",ErrorMessage ="Email already in use")]
        public string Email { get; set; }
        [MaxLength(20,ErrorMessage ="Password cannot be more than 20 characters long")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage="Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
