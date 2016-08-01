using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IProductCategoryREpository
    {
        IEnumerable<ProductCategory> GetAsAlias(string alias);
    }
}
