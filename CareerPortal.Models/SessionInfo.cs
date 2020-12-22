using System;
using System.Collections.Generic;
using System.Text;

namespace CareerPortal.Models
{
    public class SessionInfo
    {
        public SessionInfo()
        {
            this.UserID = 0;
            this.isLoggedIn = false;
        }
        public int UserID { get; set; }
        public bool isLoggedIn { get; set; }
    }
}
