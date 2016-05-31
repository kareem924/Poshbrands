using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlogApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BlogApp.Models.UnitOfWork;
using System.Web.Routing;
namespace BlogApp.Tests.ControllerTests
{
    [TestClass]
    public class PostsControllerTest
    {
        Mock<IUnitOfWork> fakeUoW;
        [TestInitialize]
        public void Initialize()
        {
            fakeUoW = new Mock<IUnitOfWork>();

            var posts = new List<Models.Post>{
                 new Models.Post{ ID = 1, Title = "Title1", Content = "Content1"},
                 new Models.Post{ ID = 2, Title = "Title2", Content = "Content2"},
            };

            fakeUoW.Setup(uow => uow.Posts.List(null)).Returns(posts.AsQueryable());
        }

        [TestMethod]
        public void Check_if_Index_returns_ViewResult_with_Model()
        {
            //Arrange 
            PostsController controller = new PostsController(fakeUoW.Object);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert 
            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result.Model, typeof(IQueryable<Models.Post>));

            Assert.AreEqual(2, ((IQueryable<Models.Post>)result.Model).Count());
        }
        [TestMethod]
        public void Check_if_Details_returns_ViewResult_with_Model()
        {
            //Arrange
            Models.Post existedPost = new Models.Post { ID = 5, Content = "Content5", Title = "Title5" };
            fakeUoW.Setup(uow => uow.Posts.Find(5)).Returns(existedPost);

            PostsController controller = new PostsController(fakeUoW.Object);

            //Act
            ViewResult result = controller.Details(5) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Model);

            Assert.AreEqual(5, ((Models.Post)result.Model).ID);
            Assert.AreEqual("Title5", ((Models.Post)result.Model).Title);
        }
        [TestMethod]
        public void Check_if_Details_returns_ViewResult_with_Model_if_id_NotExited()
        {
            //Arrange
            Models.Post existedPost = null;
            fakeUoW.Setup(uow => uow.Posts.Find(15)).Returns(existedPost);

            PostsController controller = new PostsController(fakeUoW.Object);

            //Act
            ViewResult result = controller.Details(15) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }
        [TestMethod]
        public void Check_if_Details_returns_ViewResult_with_Model_if_id_Equal_Zero()
        {
            //Arrange
            //Models.Post existedPost = null;
            //fakeUoW.Setup(uow => uow.Posts.Find(0)).Returns(existedPost);

            PostsController controller = new PostsController();

            //Act
            ViewResult result = controller.Details(0) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        //AddComment
        [TestMethod]
        public void Check_if_AddComment_Added_Successfuly()
        {
            //Arrange
            Models.Comment newComment = new Models.Comment { PostID = 5, Body = "Body1" };

            fakeUoW.Setup(uow => uow.Comments.Add(newComment)).Callback(() => { });

            fakeUoW.Setup(uow => uow.Save()).Callback(() => { });

            PostsController controller = new PostsController(fakeUoW.Object);

            //Act
            JsonResult result = controller.AddComment("Body1", 5);

            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(5, ((Models.Comment)result.Data).PostID);
        }

        [TestMethod]
        public void Check_if_Create_returns_ViewResult_Create_If_ModelSate_IsInvalid()
        {
            //Arrange 
            PostsController controller = new PostsController();
            controller.ModelState.AddModelError("key", "error");

            //Act
            ViewResult result = controller.Create(new Models.Post() { ID = 5 }, null) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);

            Assert.IsNotNull(result.Model);

            Assert.AreEqual(5, ((Models.Post)result.Model).ID);
        }

        [TestMethod]
        public void Check_if_Create_returns_Redirect_To_Index_if_ModelState_IsValid()
        {
            //Arrange

            var razorTag = new Models.Tag { Name = "Razor" };
            fakeUoW.Setup(uow => uow.Tags.List(t => t.Name == "Razor")).Returns(new[] { razorTag }.AsQueryable());
            var aspTag = new Models.Tag { Name = "ASP.NET" };
            fakeUoW.Setup(uow => uow.Tags.List(t => t.Name == "ASP.NET")).Returns(new[] { aspTag }.AsQueryable());
            var newPost = new Models.Post { ID = 10, Title = "Title10" };
            fakeUoW.Setup(uow => uow.Posts.Add(newPost)).Callback(() => { });

            fakeUoW.Setup(uow => uow.Save()).Callback(() => { });

            PostsController controller = new PostsController(fakeUoW.Object);

            //Act
            RedirectToRouteResult result = controller.Create(newPost, "Razor,ASP.NET") as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Check_if_Create_returns_ErrorView_if_Exception()
        {
            //Arrange
            PostsController controller = new PostsController();

            //Act
            ViewResult result = controller.Create(null, null) as ViewResult;

            //Assert
            Assert.IsNotNull(result);

            Assert.AreEqual("error", result.ViewName);
        }
    }
}
