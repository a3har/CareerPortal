using CareerPortal.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerPortal.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        [DateLessThan("StartDate", ErrorMessage = "Must be greater than or equal to Start Date")]
        public DateTime EndDate { get; set; }
        [Required]
        public int UserId { get; set; } 
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
