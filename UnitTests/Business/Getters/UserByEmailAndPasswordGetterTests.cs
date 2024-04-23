namespace UnitTests.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters;
    using Moq;    
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Logger;
    
    public class UserByEmailAndPasswordGetterTests : TestBase<UserByEmailAndPasswordGetter>
    {
        [Fact]
        public async Task GetReturnsCorrectUser()
        {
            var stubEmail = "email";
            var stubPassword = "password";
            var stubUserId = 10;
            var stubUser = new User { UserId = stubUserId, EmailAddress = stubEmail, Password = stubPassword };

            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));

            this.AutoMocker.GetMock<IGetUserByEmailAndPasswordQuery>()
                .Setup(x => x.Get(stubEmail, stubPassword))
                .ReturnsAsync(stubUser);

            var sut = this.CreateTestSubject();

            var result = await sut.Get(stubEmail, stubPassword);

            Assert.NotNull(result);
            Assert.Equal(stubUserId, result.UserId);

            this.AutoMocker.GetMock<IGetUserByEmailAndPasswordQuery>()
                .Verify(x => x.Get(stubEmail, stubPassword), Times.Once);
        }

        [Fact]
        public async Task GetHitsException()
        {
            this.AutoMocker.GetMock<IGetUserByEmailAndPasswordQuery>()
                .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Get(It.IsAny<string>(), It.IsAny<string>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}