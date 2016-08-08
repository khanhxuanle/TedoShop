using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TeduShop.Model.Models;

namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeduShop.Data.TeduShopDbContext context)
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "Admin",
            //    Email = "khanhlx.56@gmail.com",
            //    EmailConfirmed = true,
            //    Birthday = DateTime.Now,
            //    FullName = "Admin"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("khanhlx.56@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

            CreateProductCategorySample(context);
        }

        private void CreateProductCategorySample(TeduShop.Data.TeduShopDbContext context)
        {

            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategories = new List<ProductCategory>()
                {

                    new ProductCategory()
                    {
                         Name = "Điện lạnh",
                         Alias = "Điện lạnh",
                         Status = true
                    },
                    new ProductCategory()
                    {
                         Name = "Viễn thông",
                         Alias = "Viễn thông",
                         Status = true
                    },
                    new ProductCategory()
                    {
                         Name = "Đồ gia dụng",
                         Alias = "Đồ gia dụng",
                         Status = true
                    },
                    new ProductCategory()
                    {
                         Name = "Mỹ phẩm",
                         Alias = "Mỹ phẩm",
                         Status = true
                    }
                };

                context.ProductCategories.AddRange(listProductCategories);
                context.SaveChanges();
            }
            

            
        }
    }
}
