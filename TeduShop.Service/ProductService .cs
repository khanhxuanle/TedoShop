using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        private IUnitOfWork unitOfWork;
        private IProductTagRepository productTagRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IProductTagRepository productTagRepository)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.productTagRepository = productTagRepository;
        }

        public Product Add(Product postCategory)
        {
            return productRepository.Add(postCategory);
        }

        public void Update(Product postCategory)
        {
            productRepository.Update(postCategory);
        }

        public Product Delete(int id)
        {
            return productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return productRepository.GetAll();
            }

        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetHotProducst(int top)
        {
            return productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListPriProductsByCategoryPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1)*pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetLisProductByName(string keyWord)
        {
            return productRepository.GetMulti(x => x.Status && x.Name.Contains(keyWord)).Select(y=>y.Name);
        } 

        public IEnumerable<Product> Search(string keyWord, int page, int pageSize, string sort, out int totalRow)
        {
            var query = productRepository.GetMulti(x => x.Status && x.Name.Contains(keyWord));

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetReatedProducts(int id, int top)
        {
            var product = productRepository.GetSingleById(id);
            return productRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            return productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y => y.Tag);
        } 

        public void IncreaseView(int id)
        {
            var product = productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
            {
                product.ViewCount += 1;
            }
            else
            {
                product.ViewCount = 1;
            }

        }

        public IEnumerable<Product> GetListProductByTag(int tagId, int page, int pageSize, out int totalRow)
        {
            var model = productRepository.GetMulti(x => x.Status && x.ProductTags.Count(y => y.ProductID == x.ID) > 0, new string[] {"ProductCategory", "ProductTags"});


            totalRow = model.Count();

            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1)*pageSize).Take(page);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
