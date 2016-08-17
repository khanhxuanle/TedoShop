using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;
using ToduShop.Common;

namespace TeduShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;
        private IProductCategoryService productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this.productService = productService;
            this.productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Detail(int id)
        {     
            return View();
        }

        public ActionResult Category(int id, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = productService.GetListPriProductsByCategoryPaging(id, page, pageSize, out totalRow);

            int totalPage = (int) Math.Ceiling((double) totalRow/pageSize);

            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            var category = productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            return View(paginationSet);
        }
        
    }
}