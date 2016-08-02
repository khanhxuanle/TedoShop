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
    public class ErrorService : IErrorService
    {
        private IErrorRepository errorRepository;
        private IUnitOfWork unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this.errorRepository = errorRepository;
            this.unitOfWork = unitOfWork;
        }

        public Error Create(Error error)
        {
            return errorRepository.Add(error);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
