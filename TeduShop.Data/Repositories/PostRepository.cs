using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            //var query = DbContext.Posts.Join(DbContext.PostTags,
            //    p => p.ID,
            //    pT => pT.PostID,
            //    (p, pT) => new {Post = p, PostTag = pT})
            //    .Where(ppT => ppT.PostTag.TagID == tag)
            //    .Select(ppT => new
            //    {
            //        ppT.Post
            //    });

            var query = from p in DbContext.Posts
                join pt in DbContext.PostTags
                    on p.ID equals pt.PostID
                where pt.TagID == tag
                select p;

            totalRow = query.Count();

            query = query.Skip((pageIndex - 1)*pageSize).Take(pageSize);

            return query;

        }
    }
}

