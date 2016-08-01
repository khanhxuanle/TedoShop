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
    public class PostCategoryService : IPostCategoryService
    {
        private IPostCategoryRepository postCategoryRepository;
        private IUnitOfWork unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.postCategoryRepository = postCategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(PostCategory postCategory)
        {
            postCategoryRepository.Add(postCategory);
        }

        public void Update(PostCategory postCategory)
        {
            postCategoryRepository.Update(postCategory);
        }

        public void Delete(int id)
        {
            postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return postCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public PostCategory GetById(int id)
        {
            return postCategoryRepository.GetSingleById(id);
        }
    }
}
