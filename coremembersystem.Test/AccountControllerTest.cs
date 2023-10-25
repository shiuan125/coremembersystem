using coremembersystem.Controllers;
using coremembersystem.Data;
using coremembersystem.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace coremembersystem.Test
{
    [TestClass]
    public class AccountControllerTest
    {
        static IWebHost _webHost = null;
        static T GetService<T>()
        {
            var scope = _webHost.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
        }

        [TestMethod]
        public void Verify_Home_GetData_JsonResult_Equal_Test_String()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<AccountController>();

            var ctx = GetService<DefaultContext>();

            //// Arrage
            var homeController = new AccountController(ctx);

            var expected = "Test";

            //// Act
            var actual = homeController.GetData() as JsonResult;

            //// Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Value);
        }
        [TestMethod]
        public async Task Verify_accountservice_login_success()
        {
            // Arrange
            //Mock<IMembershipService> membership = new Mock<IMembershipService>();
            //membership.Setup(m => m.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
            //          .Returns(false);
            //Mock<IFormsService> forms = new Mock<IFormsService>();

            var ctx = GetService<DefaultContext>();

            var logonModel = new Member() { Email = "hank.chen", Password = "123456" };
            var controller = new AccountController(ctx);

            // Act
            var result = await controller.Login(logonModel) as ViewResult;

            // Assert
            //Assert.AreEqual(result.ViewName, Is.EqualTo("Index"));
            Assert.IsTrue(controller.ModelState.IsValid);
            //Assert.That(controller.ModelState[""],Is.EqualTo("The user name or password provided is incorrect."));

        }
    }
}
