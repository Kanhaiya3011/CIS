using CIS.api.Controllers;
using CIS.DAL;
using CIS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CIS.Tests
{
    public class UserControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetUsers_Should_Get_List()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<UserController>();
            var dbContext = await GetDatabaseContext();
            UserController userController = new UserController(logger,dbContext);
           
            var result = await userController.Get();
            Assert.NotNull((List<CIS.Models.User>)((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
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
                    Password = "A12345",
                    RoleId = 1
                });

                databaseContext.Users.Add(new User()
                {
                    Id = 2,
                    Email = "user@cis.com",
                    FirstName = "User",
                    LastName = "User",
                    Password = "A12345",
                    RoleId = 2
                });

                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
    }
}