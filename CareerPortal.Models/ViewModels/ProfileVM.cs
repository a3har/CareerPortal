using System;
using System.Collections.Generic;
using System.Text;

namespace CareerPortal.Models.ViewModels
{
    public class ProfileVM
    {
        public User User { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
    }
}
