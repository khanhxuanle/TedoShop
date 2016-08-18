using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TeduShop.Model.Models;
using ToduShop.Common;

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
            CreateProductCategorySample(context);
            CreateSlide(context);
            CreatePage(context);
            CreateContactDetails(context);
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

        private void CreateSlide(TeduShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlides = new List<Slide>()
                {
                    new Slide()
                    {
                        Name = "Slide 1",
                        DisplayOrder = 1,
                        Status = true,
                        Url = "#",
                        Image = "/Assets/client/images/bag.jpg",
                        Content = @"<h2>FLAT 50% 0FF</h2>
								<label>FOR ALL PURCHASE <b>VALUE</b></label>
								<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>					
								<span class=""on-get"">GET NOW</span>" },
                    new Slide()
                    {
                        Name = "Slide 2",
                        DisplayOrder = 2,
                        Status = true,
                        Url = "#",
                        Image = "/Assets/client/images/bag1.jpg",
                        Content = @"<h2>FLAT 50% 0FF</h2>
								<label>FOR ALL PURCHASE <b>VALUE</b></label>
								<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>					
								<span class=""on-get"">GET NOW</span>" }
                };

                context.Slides.AddRange(listSlides);
                context.SaveChanges();
            }
        }

        private void CreateUser(TeduShopDbContext context)
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
        }

        private void CreatePage(TeduShopDbContext context)
        {
            if (!context.Pages.Any())
            {
                var page = new Page()
                {
                    Alias = "gioi-thieu",
                    Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium,
                    totam rem aperiam,
                    eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit,
                    sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.Neque porro quisquam est,
                    qui dolorem ipsum quia dolor sit amet,
                    consectetur,
                    adipisci velit,
                    sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Ut enim ad minima veniam,
                    quis nostrum exercitationem ullam corporis suscipit laboriosam,
                    nisi ut aliquid ex ea commodi consequatur ? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur,
                    vel illum qui dolorem eum fugiat quo voluptas nulla pariatur ? ",
                    Status = true,
                    Name = "Giới thiệu"
                };

                context.Pages.Add(page);
                context.SaveChanges();
            }
        }

        private void CreateContactDetails(TeduShopDbContext context)
        {
            if (!context.ContactDetails.Any())
            {
                var contactDetails  = new ContactDetail()
                {
                    Name = "Shop thời trang TEDU",
                    Status = true,
                    Address = "Ngõ 175 Xuân Thủy, Dịch Vọng Hậu, Hà Nội",
                    Email = "khanhlx.56@gmail.com",
                    Lat = 21.0365433,
                    Lng = 105.784703,
                    Phone = "0123456789",
                    Website = "Google.com",
                    Other = ""
                };

                context.ContactDetails.Add(contactDetails);
                context.SaveChanges();
            }
        }
    }
}
