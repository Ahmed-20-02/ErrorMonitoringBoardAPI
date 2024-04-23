namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;
    using Moq;

    public class GetActiveErrorsQueryTests : TestBase<GetActiveErrorsQuery>
    {
        //Data seeded in TestDbContextFactory file
        
        [Fact]
        public async Task GetReturnsActiveErrors()
        {
            var expectedResultOne = 2;
            var expectedResultTwo = 10;
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Errors);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Get();
            
            Assert.NotNull(result);
            
            Assert.Equal(expectedResultOne, result.Count);
            
            Assert.Equal(expectedResultTwo, result.FirstOrDefault()!.ErrorId);
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