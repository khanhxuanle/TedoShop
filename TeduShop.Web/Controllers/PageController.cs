using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class PageController : Controller
    {
        private IPageService pageService;

        public PageController(IPageService pageService)
        {
            this.pageService = pageService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            var page = pageService.GetByAlias(alias);
            var model = Mapper.Map<Page, PageViewModel>(page);

            return View(model);
        }
    }
}