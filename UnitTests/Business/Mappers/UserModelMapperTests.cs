namespace UnitTests.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers;
    
    public class UserModelMapperTests : TestBase<UserModelMapper>
    {
        [Fact]
        public void Map_ReturnsCorrectlyMappedUser()
        {
            var stubUserId = 10;

            var stubUser = new User { UserId = stubUserId} ;

            var sut = this.CreateTestSubject();

            var result = sut.Map(stubUser);

            Assert.NotNull(result);
            Assert.Equal(stubUserId, result.UserId);
        }
        
        [Fact]
        public void Map_HitsException()
        {
            User user = null;

            Assert.Throws<NullReferenceException>(() => this.CreateTestSubject().Map(user));
        }
    }
}