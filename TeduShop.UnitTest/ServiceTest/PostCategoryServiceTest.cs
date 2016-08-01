using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> repositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private IPostCategoryService categoryService;
        private List<PostCategory> listCategories;

        [TestInitialize]
        public void Initialize()
        {
            repositoryMock = new Mock<IPostCategoryRepository>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            categoryService = new PostCategoryService(repositoryMock.Object, unitOfWorkMock.Object);
            listCategories = new EditableList<PostCategory>
            {
                new PostCategory { ID = 1, Name = "DM1", Status = true},
                new PostCategory { ID = 2, Name = "DM2", Status = true},
                new PostCategory { ID = 3, Name = "DM3", Status = true}
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            // setup method
            repositoryMock.Setup(m => m.GetAll(null)).Returns(listCategories);

            // call action
            var result = categoryService.GetAll() as List<PostCategory>;

            // compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory category = new PostCategory();

            category.Name = "Test";
            category.Alias = "Test";
            category.Status = true;

            repositoryMock.Setup(m => m.Add(category)).Returns((PostCategory p) =>
            {
                p.ID = 1;
                return p;
            });

            //var result = categoryService.Add(category);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(1, result.ID);
        }
    }
}
