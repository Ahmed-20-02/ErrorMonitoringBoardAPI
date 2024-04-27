namespace UnitTests.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Commands;
    using Moq;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public class CreateLogPathCommandTests : TestBase<CreateLogPathCommand>
    {
         [Fact]
        public async Task CreateReturnsNewLogPath()
        {
            var stubErrorId = 10;
            var stubFile = "TEST MESSAGE";

            var stubRequest = new CreateLogPathModel{FileName = stubFile};
            
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Errors);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            Assert.False(context.ErrorLogPaths.Any(x => x.FileName == stubFile));

            var sut = this.CreateTestSubject();

            var result = await sut.Create(stubRequest, stubErrorId);
            
            Assert.NotNull(result);

            Assert.Equal(stubErrorId, result.ErrorId);
            Assert.Equal(stubFile, result.FileName);

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

            var result = await sut.Create(new CreateLogPathModel(), It.IsAny<int>());
            
            Assert.Null(result);
        }
        
        [Fact]
        public async Task CreateHitsException()
        {
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()))
                .Throws(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Create(new CreateLogPathModel(), It.IsAny<int>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}