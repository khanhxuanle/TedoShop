using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.ID = postCategoryViewModel.ID;

            postCategory.Name = postCategoryViewModel.Name;

            postCategory.Alias = postCategoryViewModel.Alias;

            postCategory.Description = postCategoryViewModel.Description;

            postCategory.ParentID = postCategoryViewModel.ParentID;

            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;

            postCategory.Image = postCategoryViewModel.Image;

            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;

            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;

            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;

            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;

            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;

            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;

            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;

            postCategory.Status = postCategoryViewModel.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postViewModel)
        {
            post.ID = postViewModel.ID;

            post.Name = postViewModel.Name;

            post.Alias = postViewModel.Alias;

            post.CategoryID = postViewModel.CategoryID;

            post.Image = postViewModel.Image;

            post.Description = postViewModel.Description;

            post.Content = postViewModel.Content;

            post.HomeFlag = postViewModel.HomeFlag;

            post.HotFlag = postViewModel.HotFlag;

            post.ViewCount = postViewModel.ViewCount;

            post.CreatedDate = postViewModel.CreatedDate;

            post.CreatedBy = postViewModel.CreatedBy;

            post.UpdatedDate = postViewModel.UpdatedDate;

            post.UpdatedBy = postViewModel.UpdatedBy;

            post.MetaKeyword = postViewModel.MetaKeyword;

            post.MetaDescription = postViewModel.MetaDescription;

            post.Status = postViewModel.Status;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.ID = productCategoryViewModel.ID;

            productCategory.Name = productCategoryViewModel.Name;

            productCategory.Alias = productCategoryViewModel.Alias;

            productCategory.Description = productCategoryViewModel.Description;

            productCategory.ParentID = productCategoryViewModel.ParentID;

            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;

            productCategory.Image = productCategoryViewModel.Image;

            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;

            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;

            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;

            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;

            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;

            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;

            productCategory.MetaDescription = productCategoryViewModel.MetaDescription;

            productCategory.Status = productCategoryViewModel.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
            product.ID = productViewModel.ID;

            product.Name = productViewModel.Name;

            product.Alias = productViewModel.Alias;

            product.Description = productViewModel.Description;

            product.CategoryID = productViewModel.CategoryID;

            product.MoreImages = productViewModel.MoreImages;

            product.Image = productViewModel.Image;

            product.HomeFlag = productViewModel.HomeFlag;

            product.CreatedDate = productViewModel.CreatedDate;

            product.CreatedBy = productViewModel.CreatedBy;

            product.UpdatedDate = productViewModel.UpdatedDate;

            product.UpdatedBy = productViewModel.UpdatedBy;

            product.Price = productViewModel.Price;

            product.PromotionPrice = productViewModel.PromotionPrice;

            product.Warranty = productViewModel.Warranty;

            product.Content = productViewModel.Content;

            product.HotFlag = productViewModel.HotFlag;

            product.ViewCount = productViewModel.ViewCount;

            product.Quantity = productViewModel.Quantity;

            product.OriginalPrice = productViewModel.OriginalPrice;

            product.Status = productViewModel.Status;

        }
    }
}