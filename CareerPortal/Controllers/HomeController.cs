using CareerPortal.DataAccess.Repository.IRepository;
using CareerPortal.Models;
using Microsoft.AspNetCore.Authorization;
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
        public SessionInfo UserInfo;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var UserFromDb = _unitOfWork.User.GetFirstOrDefault(i => i.Email == login.Email);
                if(UserFromDb == null)
                {
                    ModelState.AddModelError("password", "Username or Password incorrect");
                    return View(login);
                }
                if (UserFromDb.Password.Equals(login.Password))
                {
                    var UserInfo = new SessionInfo()
                    {
                        isLoggedIn = true,
                        UserID = UserFromDb.Id
                    };
                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(UserInfo));
                    return RedirectToRoute(new
                    {
                        controller = "Profile",
                        action = "Index"
                    });
                }
            }
            ModelState.AddModelError("password", "Password incorrect");
            return View(login);
        }

        
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
            return View(user);
        }
        #endregion
    }
}
