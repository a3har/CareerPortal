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
    public class ExperienceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExperienceController(IUnitOfWork unitOfWork)
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
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Login"
                });
            }

            IEnumerable<Experience> experiences = _unitOfWork.Experience.GetAll(i => i.UserId == id);
            return View(experiences);



        }
        public IActionResult Upsert(int? id)
        {
            try
            {
                var UserInfo = JsonConvert.DeserializeObject<SessionInfo>(HttpContext.Session.GetString("SessionUser"));

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
            catch (Exception)
            {
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Login"
                });
            }


        }




        #region
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Experience experience)
        {
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
            return View(experience.Id);
        }

        #endregion
    }
}
