namespace UnitTests.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Commands;
    using Moq;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public class CreateErrorCommandTests : TestBase<CreateErrorCommand>
    {
        [Fact]
        public async Task CreateReturnsNewError()
        {
            var stubErrorId = 10;
            var stubMessage = "TEST MESSAGE";

            var stubRequest = new CreateErrorModel{AgentId = 123, Message = stubMessage, CustomerId = 45, DeveloperId = 2, InitialFile = "TestFile", LineNumber = 23, ProjectId = 4};
            
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Errors);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            Assert.False(context.Errors.Any(x => x.Message == stubMessage));

            var sut = this.CreateTestSubject();

            var result = await sut.Create(stubRequest);
            
            Assert.NotNull(result);

            Assert.True(result.IsActive);
            Assert.Equal(stubMessage, result.Message);
        }
        
        [Fact]
        public async Task CreateReturnsNull()
        {
            
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Errors);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Create(new CreateErrorModel());
            
            Assert.Null(result);
        }
        
        [Fact]
        public async Task CreateHitsException()
        {
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()))
                .Throws(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Create(new CreateErrorModel()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}