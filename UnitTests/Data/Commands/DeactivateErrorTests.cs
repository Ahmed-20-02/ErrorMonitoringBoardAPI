namespace UnitTests.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using DevelopmentProjectErrorBoardAPI.Logger;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Commands;
    using Moq;

    public class DeactivateErrorTests : TestBase<DeactivateError>
    {
        [Fact]
        public async Task DeactivateReturnsDeactivatesError()
        {
            var stubErrorId = 10;
            
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Errors);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            Assert.True(context.Errors.FirstOrDefaultAsync(X => X.ErrorId == stubErrorId).Result.IsActive);

            var sut = this.CreateTestSubject();

            var result = await sut.Deactivate(stubErrorId);
            
            Assert.NotNull(result);
            
            Assert.False(result.IsActive);
            Assert.Equal(stubErrorId, result.ErrorId);
        }
        
        [Fact]
        public async Task DeactivateHitsException()
        {
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()))
                .Throws(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Deactivate(It.IsAny<int>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}