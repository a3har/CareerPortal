using CareerPortal.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerPortal.Services.IService
{
    public interface IS3Service
    {
        public Task<S3Response> UploadFileAsync(IFormFile File,string BucketName);
    }
}
