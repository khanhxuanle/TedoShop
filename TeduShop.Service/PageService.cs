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
    public class PageService : IPageService
    {
        private IPageRepository pageRepository;
        private IUnitOfWork unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this.pageRepository = pageRepository;
            this.unitOfWork = unitOfWork;
        }
        public Page GetByAlias(string alias)
        {
            return pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }

    }
}
