using CareerPortal.Models.Validations;
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
        [CheckYearBetween(1900)]
        [YearLessThan("PassYear",ErrorMessage ="Enrollment Year must be less than or equal to PassYear")]
        public int EnrollYear { get; set; }
        [Required]
        [CheckYearBetween(1900)]
        [YearGreaterThan("EnrollYear",ErrorMessage = "Pass Year must be greater than or equal to Enrollment Year")]
        public int PassYear { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
