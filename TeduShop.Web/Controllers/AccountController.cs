using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using TeduShop.Data;
using TeduShop.Model.Models;
using TeduShop.Web.App_Start;
using TeduShop.Web.Models;
using ToduShop.Common;

namespace TeduShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public AccountController()
        {

        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "RegisterCaptcha", "Mã xác nhận không đúng!")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _userManager.FindByEmailAsync(model.Email);

                if (userEmail  != null)
                {
                    ModelState.AddModelError("Email","Email đã tồn tại");
                    return View(model);
                }

                var userName = await _userManager.FindByEmailAsync(model.UserName);

                if (userName != null)
                {
                    ModelState.AddModelError("UserName", "Tài khoản đã tồn tại");
                    return View(model);
                }
                         
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    Birthday = DateTime.Now,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address

                };

                await _userManager.CreateAsync(user, model.Password);

                var adminUser = await _userManager.FindByEmailAsync(model.Email);

                if (adminUser != null)
                {
                    await _userManager.AddToRolesAsync(adminUser.Id, new string[] { "User" });
                }

                ViewData["SuccessMsg"] = "Đăng kí thành công";
                SendMail(model);
            }

            ModelState.Clear();
            return View();
        }

        private void SendMail(RegisterViewModel registerViewModel)
        {
            string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/user_template.html"));
            content = content.Replace("{{UserName}}", registerViewModel.UserName);
            content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");

            var email = registerViewModel.Email;

            MailHelper.SendMail(email, "Đăng kí thành công", content);
        }

    }
}