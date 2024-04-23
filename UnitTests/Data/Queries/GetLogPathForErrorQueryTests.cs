namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;
    using Moq;

    public class GetLogPathForErrorQueryTests : TestBase<GetLogPathForErrorQuery>
    {
        //Data seeded in TestDbContextFactory file
        
        [Theory]
        [InlineData(10, 2)]
        [InlineData(12, 1)]
        [InlineData(14, 0)]
        public async Task GetReturnsCorrectLogPaths(int errorId, int numberOfPaths)
        {
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.ErrorLogPaths);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Get(errorId);
            
            Assert.NotNull(result);
            
            Assert.Equal(numberOfPaths, result.Count);
        }
        
        [Fact]
        public async Task GetHitsException()
        {
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()))
                .Throws(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Get(It.IsAny<int>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}