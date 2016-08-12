using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        private IProductService productService;

        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this.productService = productService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage, string keyword, int page, int pageSize = 20)
        {
            return CreatHttpResponseMessage(requestMessage, () =>
            {
                int totalRow = 0;

                var listCategory = productService.GetAll(keyword);

                totalRow = listCategory.Count();

                var query = listCategory.OrderBy(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);


                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = listViewModel,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
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

                var list = productService.GetAll();

                var listViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(list);

                var response = requestMessage.CreateResponse(HttpStatusCode.OK, listViewModel);
                return response;
            });
        }

        [Route("create")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
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
                    var newProduct = new Product();
                    newProduct.UpdateProduct(productViewModel);
                    newProduct.CreatedDate = DateTime.Now;
                    productService.Add(newProduct);
                    productService.Save();

                    var responData = Mapper.Map<Product, ProductViewModel>(newProduct);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, responData);
                }

                return responseMessage;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
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
                    var dbProduct = productService.GetById(productViewModel.ID);
                    dbProduct.UpdateProduct(productViewModel);
                    dbProduct.UpdatedDate = DateTime.Now;
                    productService.Update(dbProduct);
                    productService.Save();

                    var responData = Mapper.Map<Product, ProductViewModel>(dbProduct);

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

                var model = productService.GetById(id);

                var reponseData = Mapper.Map<Product, ProductViewModel>(model);

                var response = requestMessage.CreateResponse(HttpStatusCode.OK, reponseData);
                return response;
            });
        }

        [Route("del")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreatHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldProduct = productService.Delete(id);
                    productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(oldProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delmutile")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProduct)
        {
            return CreatHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedProduct);

                    foreach (var id in listId)
                    {
                        productService.Delete(id);
                    }

                    productService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, listId.Count);
                }

                return response;
            });
        }
    }
}
