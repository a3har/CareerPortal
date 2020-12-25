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
        [Range(1,100)]
        public int Score { get; set; }
        [Required]
        [CheckYearBetween(1900,ErrorMessage ="Year must be greater than 1900 and less than current year")]
        public int EnrollYear { get; set; }
        [Required]
        [CheckYearBetween(1900, ErrorMessage = "Year must be greater than 1900 and less than current year")]
        [YearGreaterThan("EnrollYear", ErrorMessage = "Pass Year must be greater than or equal to Enrollment Year")]
        public int PassYear { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
