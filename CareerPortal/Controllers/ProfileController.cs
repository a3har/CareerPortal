using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using CareerPortal.Models.ViewModels;
using CareerPortal.Services.IService;
using CareerPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CareerPortal.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IS3Service _service;
        public ProfileController(IUnitOfWork unitOfWork,IS3Service service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }
        
        public IActionResult Index()
        {
            if (!UserInfo.isLoggedIn) return RedirectToAction("Login", "Home");
            ProfileVM profile = new ProfileVM()
            {
                User = _unitOfWork.User.GetFirstOrDefault(i => i.Id == UserInfo.UserID),
                Educations = _unitOfWork.Education.GetAll(i => i.UserId == UserInfo.UserID),
                Experiences = _unitOfWork.Experience.GetAll(i => i.UserId == UserInfo.UserID)
            };
            ViewData["ProfileImgSrc"]=SD.S3BaseURL+"/"+SD.ProfileImageFolder+"/"+ UserInfo.UserID.ToString()+".png";
            ViewData["ResumeSrc"] = SD.S3BaseURL + "/" + SD.ResumeFolder + "/" + UserInfo.UserID.ToString()+".pdf";

            return View(profile);
        }

        public IActionResult AddFile(string type = "Image")
        {
            if (!UserInfo.isLoggedIn) return RedirectToAction("Login", "Home");
            FileUploadObject file = new FileUploadObject();
            file.Type = type;
            return View(file);
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> AddFile(FileUploadObject test)
        {

            if (!UserInfo.isLoggedIn) return RedirectToAction("Login", "Home");
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await test.URL.CopyToAsync(memoryStream);
                    if (memoryStream.Length < 2097152)
                    {
                        var extension = Path.GetExtension(test.URL.FileName).ToLower();
                        if ((extension.Equals(".png") && test.Type.Equals("Image")) || (extension.Equals(".pdf") && test.Type.Equals("Resume")))
                        {
                            var FileKey = UserInfo.UserID.ToString() + extension;
                            var BucketName = test.Type.Equals("Image") ? SD.BucketName + @"/" + SD.ProfileImageFolder : SD.BucketName + @"/" + SD.ResumeFolder;
                            var response = await _service.UploadFileAsync(memoryStream, BucketName, FileKey);
                            if (response.UploadSuccessful) return RedirectToAction(nameof(Index), "Profile");
                            else return Ok(response);
                        }
                        else
                        {
                            ModelState.AddModelError("url", "Extension must be png / pdf");
                            return View(test);
                        }
                    }

                    ModelState.AddModelError("url", "File must be less than 2 mb");
                }
            }
            return View(test);
        }
    }
}
