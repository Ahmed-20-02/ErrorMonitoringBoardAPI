namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using DevelopmentProjectErrorBoardAPI.Logger;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;
    using Moq;
    
    public class GetUsersByRoleIdQueryTests : TestBase<GetUsersByRoleIdQuery>
    {
        //Data seeded in TestDbContextFactory file
        
        [Fact]
        public async Task GetReturnsUser()
        {
            var stubRoleId = 2;
            var stubUserId = 5;
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Errors);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Get(stubRoleId);
            
            Assert.NotNull(result);

            Assert.Single(result);
            
            Assert.Equal(stubUserId, result[0].UserId);
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