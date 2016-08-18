using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
            var product = productService.GetById(id);
            var productModelView = Mapper.Map<Product, ProductViewModel>(product);

            var relateProduct = productService.GetReatedProducts(id, 6);

            ViewBag.RelateProduct = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relateProduct);

            var moreImages = productModelView.MoreImages;
            List<string> listImage = new JavaScriptSerializer().Deserialize<List<string>>(moreImages);
            ViewBag.MoreImage = listImage;
          
            var tags = productService.GetListTagByProductId(id);
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(tags);

            return View(productModelView);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = productService.GetListPriProductsByCategoryPaging(id, page, pageSize, sort, out totalRow);

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

        public ActionResult ListByTag(string tagid, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = productService.GetListProductByTag(tagid, page, pageSize, out totalRow);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            ViewBag.KeyWord = Mapper.Map<Tag, TagViewModel>(productService.GetTag(tagid));
             
            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = productService.Search(keyword, page, pageSize, sort, out totalRow);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            ViewBag.KeyWord = keyword;

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = productService.GetLisProductByName(keyword);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);

        }
        
    }
}