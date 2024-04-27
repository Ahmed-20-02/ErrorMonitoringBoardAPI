namespace UnitTests.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers;

    public class ErrorLogPathModelMapperTests : TestBase<ErrorLogPathModelMapper>
    {
       [Fact]
        public void Map_ReturnsCorrectlyMappedLogPath()
        {
            var stubErrorId = 10;

            var stubLogPath = new ErrorLogPath { ErrorLogPathId = 1, ErrorId = stubErrorId } ;

            var sut = this.CreateTestSubject();

            var result = sut.Map(stubLogPath);

            Assert.NotNull(result);
            Assert.Equal(stubErrorId, result.ErrorId);
        }
        
        [Fact]
        public void Map_HitsException()
        {
            ErrorLogPath nullLogPath = null;

            Assert.Throws<NullReferenceException>(() => this.CreateTestSubject().Map(nullLogPath));
        }
    }
}