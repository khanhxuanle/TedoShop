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
using TeduShop.Web.Models;
using TeduShop.Web.Infrastructure.Extensions;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this.postCategoryService = postCategoryService;
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryViewModel)
        {
            return CreatHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    var postCategory = new PostCategory();

                    postCategory.UpdatePostCategory(postCategoryViewModel);

                    var category = postCategoryService.Add(postCategory);
                    postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }

                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryViewModel)
        {
            return CreatHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategory = postCategoryService.GetById(postCategoryViewModel.ID);
                    postCategory.UpdatePostCategory(postCategoryViewModel);

                    postCategoryService.Update(postCategory);
                    postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreatHttpResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    postCategoryService.Delete(id);
                    postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreatHttpResponseMessage(request, () =>
            {                
                var listCategory = postCategoryService.GetAll();

                var listCategoryViewModel = Mapper.Map<List<PostCategoryViewModel>>(listCategory);


                var response = request.CreateResponse(HttpStatusCode.OK, listCategoryViewModel);

                return response;
            });
        }
    }
}