using CIS.api.Controllers;
using CIS.DAL;
using CIS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CIS.Tests
{
    public class LoginControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Login_Successful()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<UserController>();
            var dbContext = await GetDatabaseContext();
            LoginController home = new LoginController(logger,dbContext);
            var user = new User();
            user.Email = "admin@cis.com";
            user.Password = "A12345";

            var result = await home.Post(user);
            Assert.AreEqual("Admin", ((CIS.Models.User)((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value).FirstName);
        }

        [Test]
        public async Task Login_Fail()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<UserController>();
            var dbContext = await GetDatabaseContext();
            LoginController home = new LoginController(logger, dbContext);
            var user = new User();
            user.Email = "admin@cis1.com";
            user.Password = "A12345";

            var result = await home.Post(user);
            Assert.AreEqual(null, ((CIS.Models.User)((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value));
        }

        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Users.CountAsync() <= 0)
            {

                databaseContext.Users.Add(new User()
                {
                    Id = 1,
                    Email = "admin@cis.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Password = "A12345"
                });

                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
    }
}