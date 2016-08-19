using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BotDetect.Web.Mvc;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;
using ToduShop.Common;

namespace TeduShop.Web.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        private IFeedBackService feedBackService;
        private IContactDetailService contactDetailService;

        public ContactController(IContactDetailService contactDetailService, IFeedBackService feedBackService)
        {
            this.contactDetailService = contactDetailService;
            this.feedBackService = feedBackService;
        }

        public ActionResult Index()
        {
            var model = new FeedbackViewModel();
            model.ContactDetail = getDetailViewModel();
            return View(model);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ContactCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback feedback = new Feedback();
                feedback.UpdateFeedback(feedbackViewModel);
                feedBackService.Create(feedback);
                feedBackService.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";
                SendMail(feedbackViewModel);

                ModelState.Clear();
            }

            feedbackViewModel.ContactDetail = getDetailViewModel();           
            return View("Index", feedbackViewModel); 
        }

        private ContactDetailViewModel getDetailViewModel()
        {
            var model = contactDetailService.GetDefaultContact();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);

            return contactViewModel;
        }


        private void SendMail(FeedbackViewModel feedbackViewModel)
        {
            string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
            content = content.Replace("{{Name}}", feedbackViewModel.Name);
            content = content.Replace("{{Email}}", feedbackViewModel.Email);
            content = content.Replace("{{Message}}", feedbackViewModel.Message);

            var adminEmail = ConfigHelper.GetByKey("AdminEmail");

            MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);
        }
    }
}