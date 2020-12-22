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
    public class EducationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EducationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;                
        }
        public IActionResult Index()
        {
            int id;
            try
            {
                var UserInfo = JsonConvert.DeserializeObject<SessionInfo>(HttpContext.Session.GetString("SessionUser"));
                id = UserInfo.UserID;    
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Login), nameof(HomeController));
            }

            IEnumerable<Education> educations = _unitOfWork.Education.GetAll(i => i.UserId == id);
            return View(educations);



        }
        public IActionResult Upsert(int? id)
        {
            try
            {
                var UserInfo = JsonConvert.DeserializeObject<SessionInfo>(HttpContext.Session.GetString("SessionUser"));
              
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
            catch(Exception)
            {
                return RedirectToAction(nameof(Login), nameof(HomeController));
            }

            
        }




        #region
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Education education)
        {
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
            return View(education.Id);
        }

        #endregion
    }
}
