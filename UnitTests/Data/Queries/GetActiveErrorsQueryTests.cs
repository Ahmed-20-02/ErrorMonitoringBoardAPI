namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;
    using Moq;

    public class GetActiveErrorsQueryTests : TestBase<GetActiveErrorsQuery>
    {
        [Fact]
        public void GetReturnsActiveErrors()
        {
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.Empty(context.Errors);

            var role = new Role { RoleId = 2, Name = "" };
            // Add required related entities
            var user = new User { UserId = 5, Password = "",Role = role, RoleId = role.RoleId, EmailAddress = "", FirstName = "", LastName = ""};
            var project = new Project { ProjectId = 5, Name = ""};
            var status = new Status { StatusId = 2, Name = ""};
            
            context.Users.Add(user);
            context.Projects.Add(project);
            context.Statuses.Add(status);
            
            context.Errors.AddRange(new List<Error>
            {
                new Error{
                    ErrorId = 10,
                    IsActive = true,
                    Agent = user,
                    AgentId = user.UserId,
                    Project = project,
                    ProjectId = project.ProjectId,
                    Message = "UNIT TEST",
                    Status = status,
                    StatusId = status.StatusId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    LineNumber = 1,
                    InitialFile = "file"
                },
                new Error{
                    ErrorId = 12,
                    IsActive = false,
                    Agent = user,
                    AgentId = user.UserId,
                    Project = project,
                    ProjectId = project.ProjectId,
                    Message = "UNIT TEST",
                    Status = status,
                    StatusId = status.StatusId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    LineNumber = 1,
                    InitialFile = "file"
                }
            });
            
             context.SaveChanges();

            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            var sut = this.CreateTestSubject();

            var result = sut.Get();
            
            Assert.NotNull(result);
            
            Assert.Single(result.Result);
            
            Assert.Equal(10, result.Result.FirstOrDefault()!.ErrorId);
        }
        
        [Fact]
        public async Task GetHitsException()
        {
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()))
                .Throws(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Get());
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}