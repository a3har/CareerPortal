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
    public class ExperienceController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExperienceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            if (!UserInfo.isLoggedIn)
            {
                return RedirectToAction(nameof(Login), "Home");
            }

            IEnumerable<Experience> experiences = _unitOfWork.Experience.GetAll(i => i.UserId == UserInfo.UserID);
            return View(experiences);



        }
        public IActionResult Upsert(int? id)
        {
            if (!UserInfo.isLoggedIn)
            {
                return RedirectToAction(nameof(Login), "Home");
            }
            Experience experience;
            if (id == 0 || id == null)
            {
                experience = new Experience();
                experience.UserId = UserInfo.UserID;
                return View(experience);
            }

            experience = _unitOfWork.Experience.Get(id.GetValueOrDefault());
            if (experience == null)
            {
                return NotFound();
            }
            return View(experience);
        }




        #region
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Experience experience)
        {
            if (!UserInfo.isLoggedIn)
            {
                return RedirectToAction(nameof(Login), "Home");
            }
            if (ModelState.IsValid)
            {
                if (experience.Id == 0)
                {
                    _unitOfWork.Experience.Add(experience);
                }
                else
                {
                    _unitOfWork.Experience.Update(experience);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Experience.Get(id);
            if (category == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            if (category.UserId != UserInfo.UserID)
            {
                return Json(new { success = false, message = "Unauthenticated delete" });
            }
            _unitOfWork.Experience.Remove(category);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successfull" });

        }

        #endregion
    }
}
