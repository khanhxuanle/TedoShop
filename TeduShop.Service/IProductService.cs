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
        IEnumerable<Product> GetListPriProductsByCategoryPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);
        IEnumerable<string> GetLisProductByName(string keyWord);
        IEnumerable<Product>  Search(string keyWord, int page, int pageSize, string sort, out int totalRow);
        IEnumerable<Product> GetReatedProducts(int id, int top);
        IEnumerable<Tag> GetListTagByProductId(int id);
        void IncreaseView(int id);
        IEnumerable<Product> GetListProductByTag(int tagId, int page, int pageSize, out int totalRow);
        void Save();
    }
}
