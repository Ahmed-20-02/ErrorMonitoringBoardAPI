namespace UnitTests.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Getters;
    using DevelopmentProjectErrorBoardAPI.Logger;
    using Moq;
    
    public class UserByIdGetterTests : TestBase<UserByIdGetter>
    {
        [Fact]
        public async Task GetReturnsCorrectUser()
        {

            var stubUserId = 10;
            var stubUser = new User { UserId = stubUserId};

            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));

            this.AutoMocker.GetMock<IGetUserByIdQuery>()
                .Setup(x => x.Get(stubUserId))
                .ReturnsAsync(stubUser);

            var sut = this.CreateTestSubject();

            var result = await sut.Get(stubUserId);

            Assert.NotNull(result);
            Assert.Equal(stubUserId, result.UserId);

            this.AutoMocker.GetMock<IGetUserByIdQuery>()
                .Verify(x => x.Get(stubUserId), Times.Once);
        }

        [Fact]
        public async Task GetHitsException()
        {
            this.AutoMocker.GetMock<IGetUserByIdQuery>()
                .Setup(x => x.Get(It.IsAny<int>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Get( It.IsAny<int>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}