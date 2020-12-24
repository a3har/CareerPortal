using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models.ViewModels;
using CareerPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerPortal.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            ViewData["ProfileImgSrc"]=SD.S3BaseURL+"/"+SD.ProfileImageFolder+"/"+ UserInfo.UserID.ToString();
            ViewData["ResumeSrc"] = SD.S3BaseURL + "/" + SD.ResumeFolder + "/" + UserInfo.UserID.ToString();

            return View(profile);
        }
    }
}
