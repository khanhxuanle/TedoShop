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
    public class PostService : IPostService
    {
        private IPostRepository postRepository;
        private IUnitOfWork unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Post post)
        {
            postRepository.Add(post);
        }

        public void Update(Post post)
        {
            postRepository.Update(post);
        }

        public void Delete(int id)
        {
            postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return postRepository.GetAll(new string[] {"PostCategory"});
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Post GetById(int id)
        {
            return postRepository.GetSingleById(id);
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {          
            return postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
