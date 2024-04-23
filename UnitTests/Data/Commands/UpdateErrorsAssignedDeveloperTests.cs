namespace UnitTests.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Commands;
    using Moq;

    public class UpdateErrorsAssignedDeveloperTests : TestBase<UpdateErrorsAssignedDeveloper>
    {
        [Fact]
        public async Task UpdateAssignsNewDeveloper()
        {
            var stubErrorId = 10;
            var stubNewDevId = 10;
            
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Errors);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);

            Assert.NotEqual(context.Errors.FirstOrDefaultAsync(X => X.ErrorId == stubErrorId).Result.DeveloperId, stubNewDevId);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Update(stubErrorId, stubNewDevId);
            
            Assert.NotNull(result);
            
            Assert.Equal(stubNewDevId, result.DeveloperId);
        }
        
        [Fact]
        public async Task UpdateHitsException()
        {
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()))
                .Throws(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Update(It.IsAny<int>(), It.IsAny<int>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}