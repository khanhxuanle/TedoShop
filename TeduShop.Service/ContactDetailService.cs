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
    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository contactDetailRepository;
        private IUnitOfWork unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this.contactDetailRepository = contactDetailRepository;
            this.unitOfWork = unitOfWork;
        }
        public ContactDetail GetDefaultContact()
        {
            return contactDetailRepository.GetSingleByCondition(x => x.Status);
        }
    }
}
