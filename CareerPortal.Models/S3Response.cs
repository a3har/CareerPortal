using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CareerPortal.Models
{
    public class S3Response
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public bool UploadSuccessful { get; set; }
    }
}
