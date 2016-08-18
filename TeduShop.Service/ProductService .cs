using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using ToduShop.Common;

namespace TeduShop.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        private IUnitOfWork unitOfWork;
        private IProductTagRepository productTagRepository;
        private ITagRepository tagRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IProductTagRepository productTagRepository, ITagRepository tagRepository)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.productTagRepository = productTagRepository;
            this.tagRepository = tagRepository;
        }

        public Product Add(Product productCategory)
        {
            var product = productRepository.Add(productCategory);
            unitOfWork.Commit();
            if (!string.IsNullOrEmpty(productCategory.Tags))
            {
                string[] tags = productCategory.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tag = tags[i].Trim();
                    var tagId = StringHelper.ConvertToUnSign(tag);
                    if (tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag newtag = new Tag();
                        newtag.ID = tagId;
                        newtag.Name = tag;
                        newtag.Type = CommonConstants.ProductTag;
                        tagRepository.Add(newtag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = productCategory.ID;
                    productTag.TagID = tagId;
                    productTagRepository.Add(productTag);
                }
            }
            return product;
        }

        public void Update(Product productCategory)
        {
            productRepository.Update(productCategory);
            if (!string.IsNullOrEmpty(productCategory.Tags))
            {
                string[] tags = productCategory.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tag = tags[i].Trim();
                    
                    var tagId = StringHelper.ConvertToUnSign(tag);
                    if (tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag newtag = new Tag();
                        newtag.ID = tagId;
                        newtag.Name = tag;
                        newtag.Type = CommonConstants.ProductTag;
                        tagRepository.Add(newtag);
                    }
                    productTagRepository.DeleteMulti(x => x.ProductID == productCategory.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = productCategory.ID;
                    productTag.TagID = tagId;
                    productTagRepository.Add(productTag);
                }

            }
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

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var listProduct = productRepository.GetListProductByTag(tagId, page, pageSize, out totalRow);

            return listProduct;
        }

        public Tag GetTag(string tagId)
        {
            return tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
