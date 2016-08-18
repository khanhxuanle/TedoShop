using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

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
            var model = contactDetailService.GetDefaultContact();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return View(contactViewModel);
        }

        //public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Feedback feedback = new Feedback();
        //        feedback.UpdateFeedback(feedbackViewModel);
        //        feedBackService.Create(feedback);
        //        feedBackService.Save();
        //    }
        //}

        //private
    }
}