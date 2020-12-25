using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerPortal.Controllers
{
    public class EducationController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EducationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            if (!UserInfo.isLoggedIn)
            {
                return RedirectToAction(nameof(Login), "Home");
            }

            IEnumerable<Education> educations = _unitOfWork.Education.GetAll(i => i.UserId == UserInfo.UserID);
            return View(educations);
        }
        public IActionResult Upsert(int? id)
        {
            if (!UserInfo.isLoggedIn)
            {
                return RedirectToAction(nameof(Login), "Home");
            }
            
            Education education;
            if (id == 0 || id == null)
            {
                education = new Education();
                education.UserId = UserInfo.UserID;
                return View(education);
            }

            education = _unitOfWork.Education.Get(id.GetValueOrDefault());
            if (education == null)
            {
                return NotFound();
            }
            return View(education); 
        }




        #region
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Education education)
        {
            if (!UserInfo.isLoggedIn)
            {
                return RedirectToAction(nameof(Login), "Home");
            }
            if (ModelState.IsValid)
            {
                if(education.Id == 0)
                {
                    _unitOfWork.Education.Add(education);
                }
                else
                {
                    _unitOfWork.Education.Update(education);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(education);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Education.Get(id);
            if (category == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            if (category.UserId != UserInfo.UserID)
            {
                return Json(new { success = false, message = "Unauthenticated delete" });
            }
            _unitOfWork.Education.Remove(category);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successfull" });

        }

        #endregion
    }
}
