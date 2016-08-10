using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this.productCategoryService = productCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage, string keyword, int page, int pageSize = 20)
        {
            return CreatHttpResponseMessage(requestMessage, () =>
            {
                int totalRow = 0;

                var listCategory = productCategoryService.GetAll(keyword);

                totalRow = listCategory.Count();
 
                var query = listCategory.OrderBy(x => x.CreatedDate).Skip(page*pageSize).Take(pageSize);

                var listCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>,IEnumerable<ProductCategoryViewModel>>(query);


                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listCategoryViewModel,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int) Math.Ceiling((decimal) totalRow/pageSize)
                };

                var response = requestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage)
        {
            return CreatHttpResponseMessage(requestMessage, () =>
            {

                var listCategory = productCategoryService.GetAll();

                var listCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listCategory);

                var response = requestMessage.CreateResponse(HttpStatusCode.OK, listCategoryViewModel);
                return response;
            });
        }

        [Route("create")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage requestMessage, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreatHttpResponseMessage(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;

                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdatePostCategory(productCategoryViewModel);
                    newProductCategory.CreatedDate = DateTime.Now;
                    productCategoryService.Add(newProductCategory);
                    productCategoryService.Save();

                    var responData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responData);
                }
                
                return responseMessage;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]        
        public HttpResponseMessage Update(HttpRequestMessage requestMessage, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreatHttpResponseMessage(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;

                if (!ModelState.IsValid)
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbProductCategory = productCategoryService.GetById(productCategoryViewModel.ID);
                    dbProductCategory.UpdatePostCategory(productCategoryViewModel);
                    dbProductCategory.UpdatedDate = DateTime.Now;
                    productCategoryService.Update(dbProductCategory);
                    productCategoryService.Save();

                    var responData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responData);
                }

                return responseMessage;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage requestMessage, int id)
        {
            return CreatHttpResponseMessage(requestMessage, () =>
            {

                var model = productCategoryService.GetById(id);

                var reponseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);

                var response = requestMessage.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }


    }
}
