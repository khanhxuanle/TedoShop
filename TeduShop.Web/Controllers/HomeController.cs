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
        private IProductService productService;

        public HomeController(IProductCategoryService productCategoryService, IProductService productService, ICommonService commonService)
        {
            this.productCategoryService = productCategoryService;
            this.commonService = commonService;
            this.productService = productService;
        }

        public ActionResult Index()
        {
            var slides = commonService.GetSlides();
            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slides);

            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideViewModel;

            var lastetProduct = productService.GetLastest(3);
            var topSaleProduct = productService.GetHotProducst(3);

            var lastetProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastetProduct);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProduct);

            homeViewModel.LastestProducts = lastetProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;

            return View(homeViewModel);
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