namespace UnitTests
{
    using Microsoft.EntityFrameworkCore;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    
    public class TestDbContextFactory : IDbContextFactory<DataContext>
    {
        private DbContextOptions<DataContext> _options;
        
        public TestDbContextFactory(string databaseName = "InMemoryTest")
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        public DataContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique database name for each context instance
                .Options;
            
            var context = new DataContext(options);
           // var context = new DataContext(_options);
            
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            var role = new Role { RoleId = 2, Name = "dev" };
            var roleTwo = new Role { RoleId = 3, Name = "agent" };
            var user = new User { UserId = 5, Password = "hello",Role = role, RoleId = role.RoleId, EmailAddress = "test@gmail.com", FirstName = "", LastName = ""};
            var userTwo = new User { UserId = 10, Password = "hello",Role = roleTwo, RoleId = roleTwo.RoleId, EmailAddress = "test@gmail.com", FirstName = "", LastName = ""};

            var project = new Project { ProjectId = 5, Name = ""};
            var projectTwo = new Project { ProjectId = 7, Name = "ProjectTwo"};
            var status = new Status { StatusId = 2, Name = "Active"};
            var statusTwo = new Status { StatusId = 4, Name = "Inactive"};


            var errorOne = new Error
            {
                ErrorId = 10, IsActive = true,
                Agent = user, AgentId = user.UserId, 
                Project = project, ProjectId = project.ProjectId,
                Message = "UNIT TEST", Status = status,
                StatusId = status.StatusId, CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now, LineNumber = 1,
                InitialFile = "file"
            };
            var errorTwo = new Error
            {
                ErrorId = 12, IsActive = false,
                Agent = userTwo, AgentId = userTwo.UserId,
                Project = project, ProjectId = project.ProjectId,
                Message = "UNIT TEST", Status = status,
                StatusId = status.StatusId, CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now, LineNumber = 1,
                InitialFile = "file"
            };
            
            var errorThree= new Error
            {
                ErrorId = 14, IsActive = true,
                Agent = user, AgentId = user.UserId,
                Project = projectTwo, ProjectId = projectTwo.ProjectId,
                Message = "UNIT TEST", Status = status,
                StatusId = status.StatusId, CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now, LineNumber = 1,
                InitialFile = "file"
            };

            var logPathOne = new ErrorLogPath
            {
                ErrorLogPathId = 40,
                ErrorId = errorOne.ErrorId,
                CreatedDate = DateTime.Now,
                FileName = "LOGPATHTEST1",
                Error = errorOne
            };
            
            var logPathTwo = new ErrorLogPath
            {
                ErrorLogPathId = 45,
                ErrorId = errorOne.ErrorId,
                CreatedDate = DateTime.Now,
                FileName = "LOGPATHTEST2",
                Error = errorOne
            };
            
            var logPathThree = new ErrorLogPath
            {
                ErrorLogPathId = 50,
                ErrorId = errorTwo.ErrorId,
                CreatedDate = DateTime.Now,
                FileName = "LOGPATHTEST3",
                Error = errorTwo
            };
            
            context.Projects.Add(project);
            
            context.Statuses.AddRange(new List<Status>{status, statusTwo});
            
            context.Roles.AddRange(new List<Role>{role, roleTwo});
            
            context.Users.AddRange(new List<User>{user, userTwo});
            
            context.Errors.AddRange(new List<Error>
            {errorOne, errorTwo, errorThree});
            
            context.ErrorLogPaths.AddRange(new List<ErrorLogPath>
            { logPathOne, logPathTwo, logPathThree});
            
            context.SaveChanges();

            return context;
        }
    }
}

//References
//https://stackoverflow.com/questions/66101618/moq-idbcontextfactory-with-in-memory-ef-core
//https://stackoverflow.com/questions/71115151/blazor-mocking-idbcontextfactory
//03/01/2024