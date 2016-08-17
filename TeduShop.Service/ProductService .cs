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

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
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

        public IEnumerable<Product> GetListPriProductsByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            var query = productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            totalRow = query.Count();

            return query.Skip((page - 1)*pageSize).Take(pageSize);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
