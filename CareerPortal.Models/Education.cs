using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerPortal.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string  Institution { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public int EnrollYear { get; set; }
        [Required]
        public int PassYear { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
