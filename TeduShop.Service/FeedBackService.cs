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
    public class FeedBackService : IFeedBackService
    {
        private IFeedbackRepository feedbackRepository;
        private IUnitOfWork unitOfWork;

        public FeedBackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this.feedbackRepository = feedbackRepository;
            this.unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            return feedbackRepository.Add(feedback);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
