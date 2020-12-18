using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CareerPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SessionInfo UserInfo;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            ////UserInfo = JsonConvert.DeserializeObject<SessionInfo>(HttpContext.Session.GetString("SessionUser"));
            UserInfo = new SessionInfo(){
                isLoggedIn = false,
                UserID = 3
            };
        }

        public IActionResult Index()
        {
            if (UserInfo == null || !UserInfo.isLoggedIn)
            {
                return View();
            }
            else
            {
                return RedirectToRoute(new
                {
                    controller = "Profile",
                    action = "Index"
                });
            }
        }

        public IActionResult Login()
        {
            Login login = new Login();
            return View(login);
        }

        public IActionResult Register()
        {
            User user = new User();
            return View(user);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        #region

        //[HttpPost]
        //public IActionResult Login(Login login)
        //{

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.User.Add(user);
                _unitOfWork.Save();

                var UserInfo = new SessionInfo() {
                    isLoggedIn = false,
                    UserID = _unitOfWork.User.GetFirstOrDefault(i => i.Email == user.Email).Id
                };
                HttpContext.Session.SetString("SessionUser",JsonConvert.SerializeObject(UserInfo));


                return RedirectToRoute(new {
                    controller = "Education",
                    action = "Index"
                });
            }
            return NotFound();
        }
        #endregion
    }
}
