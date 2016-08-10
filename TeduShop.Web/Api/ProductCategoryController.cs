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
    }
}
