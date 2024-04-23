namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using DevelopmentProjectErrorBoardAPI.Logger;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;    
    using DevelopmentProjectErrorBoardAPI.Services.Interfaces;
    using Moq;
    
    public class GetUserByEmailAndPasswordQueryTests : TestBase<GetUserByEmailAndPasswordQuery>
    {
        //Data seeded in TestDbContextFactory file
        
        [Fact]
        public async Task GetReturnsCorrectUser()
        {
            string stubEmail = "test@gmail.com";
            string stubPassword = "hello";
                
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Users);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            this.AutoMocker.GetMock<IPasswordService>()
                .Setup(x => x.VerifyPassword(stubPassword, It.IsAny<string>() ))
                .Returns(true);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Get(stubEmail, stubPassword);
            
            Assert.NotNull(result);
            
            Assert.Equal(5, result.UserId);
        }
        
        [Fact]
        public async Task GetReturnsNull()
        {
            string stubEmail = "test@gmail.com";
            string stubPassword = "hello";
                
            var context = new TestDbContextFactory().CreateDbContext();
            
            Assert.NotEmpty(context.Users);
            
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));
            
            this.AutoMocker.GetMock<IDbContextFactory<DataContext>>()
                .Setup(x => x.CreateDbContext())
                .Returns(context);
            
            this.AutoMocker.GetMock<IPasswordService>()
                .Setup(x => x.VerifyPassword(stubPassword, It.IsAny<string>() ))
                .Returns(false);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Get(stubEmail, stubPassword);
            
            Assert.Null(result);
        }
        
        [Fact]
        public async Task GetHitsException()
        {
            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()))
                .Throws(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Get(It.IsAny<string>(), It.IsAny<string>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}