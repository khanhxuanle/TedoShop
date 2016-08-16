using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TeduShop.Data.Repositories;
using TeduShop.Service;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService productCategoryService;
        private ICommonService commonService;

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService)
        {
            this.productCategoryService = productCategoryService;
            this.commonService = commonService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly] // chi duoc goi tu tren url tu view ko duoc goi tren partialView
        public ActionResult Footer()
        {
            var footerModel = commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);

            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = productCategoryService.GetAll();
            var listProductCategoryViewModel =  Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}