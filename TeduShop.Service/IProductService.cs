using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductService  
    {
        Product Add(Product postCategory);
        void Update(Product postCategory);
        Product Delete(int id);
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        IEnumerable<Product> GetAll(string keyword);
        IEnumerable<Product> GetLastest(int top);
        IEnumerable<Product> GetHotProducst(int top);
        IEnumerable<Product> GetListPriProductsByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);
        void Save();
    }
}
