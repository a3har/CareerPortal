using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CareerPortal.Models;
using CareerPortal.Services.IService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CareerPortal.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;

        public S3Service(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<S3Response> UploadFileAsync(MemoryStream memoryStream,string BucketName,string FileKey)
        {
            if (memoryStream == null) return new S3Response { Message = "memoryStream value null" };
            try
            {
                var fileTransferUtility = new TransferUtility(_client);
                await fileTransferUtility.UploadAsync(memoryStream, BucketName, FileKey);
                //await fileTransferUtility.UploadAsync(File, BucketName, "Test");
                return new S3Response
                {
                    Message = "Upload Successfull",
                    UploadSuccessful = true
                };
            }
            catch(AmazonS3Exception e)
            {
                return new S3Response
                {
                    Message = e.Message,
                    Status = e.StatusCode,
                    UploadSuccessful = false
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return new S3Response
                {
                    Message = e.Message,
                    UploadSuccessful = false
                };
            }
        }
    }
}
