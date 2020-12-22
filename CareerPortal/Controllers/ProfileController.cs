using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models.ViewModels;
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
            ProfileVM profile = new ProfileVM()
            {
                User = _unitOfWork.User.GetFirstOrDefault(i => i.Id == UserInfo.UserID),
                Educations = _unitOfWork.Education.GetAll(i => i.UserId == UserInfo.UserID),
                Experiences = _unitOfWork.Experience.GetAll(i => i.UserId == UserInfo.UserID)
            };
            return View(profile);
        }
    }
}
