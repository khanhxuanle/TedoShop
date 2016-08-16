using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using ToduShop.Common;

namespace TeduShop.Service
{
    public class CommonService : ICommonService
    {
        private IFooterRepository footerRepository;
        private IUnitOfWork unitOfWork;
        private ISlideRepository slideRepository;
        public CommonService(IFooterRepository footerRepository, ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            this.footerRepository = footerRepository;
            this.unitOfWork = unitOfWork;
            this.slideRepository = slideRepository;
        }
        public Footer GetFooter()
        {
            return footerRepository.GetSingleByCondition(x=>x.ID == CommonConstants.DEFAULTFOOTERID);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return slideRepository.GetMulti(x=>x.Status);
        }
    }
}
