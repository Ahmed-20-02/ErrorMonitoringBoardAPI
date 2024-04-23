namespace UnitTests.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Business.Getters;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Logger;
    using Moq;
    using DevelopmentProjectErrorBoardAPI.Enums;
    
    public class DevelopersGetterTests : TestBase<DevelopersGetter>
    {
        [Fact]
        public async Task GetReturnsCorrectlyMappedDevelopers()
        {
            var stubUserId = 10;
            var stubRoleId = (int)RolesEnum.Developer;;

            var stubUser = new User{ UserId = stubUserId, RoleId = stubRoleId};

            var stubUsers = new List<User>{ stubUser };

            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));

            this.AutoMocker.GetMock<IGetUsersByRoleIdQuery>()
                .Setup(x => x.Get(stubRoleId))
                .ReturnsAsync(stubUsers);

            this.AutoMocker.GetMock<IUserModelMapper>()
                .Setup(x => x.Map(stubUser))
                .Returns(new UserModel{UserId = stubUser.UserId});

            var sut = this.CreateTestSubject();

            var result = await sut.Get();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            
            this.AutoMocker.GetMock<IGetUsersByRoleIdQuery>()
                .Verify(x => x.Get(stubRoleId), Times.Once);
            this.AutoMocker.GetMock<IUserModelMapper>()
                .Verify(x => x.Map(stubUser), Times.Once);

        }
        
        [Fact]
        public async Task GetHitsException()
        {
            this.AutoMocker.GetMock<IGetUsersByRoleIdQuery>()
                .Setup(x => x.Get(It.IsAny<int>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Get());
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}