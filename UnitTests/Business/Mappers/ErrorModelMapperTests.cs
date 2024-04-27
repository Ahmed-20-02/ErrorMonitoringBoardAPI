namespace UnitTests.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers;

    public class ErrorModelMapperTests : TestBase<ErrorModelMapper>
    {
        [Fact]
        public void Map_ReturnsCorrectlyMappedError()
        {
            var stubErrorId = 10;

            var stubError = new Error {ErrorId = stubErrorId };

            var sut = this.CreateTestSubject();

            var result = sut.Map(stubError);

            Assert.NotNull(result);
            Assert.Equal(stubErrorId, result.ErrorId);
        }

        [Fact]
        public void Map_HitsException()
        {
            Error error = null;

            Assert.Throws<NullReferenceException>(() => this.CreateTestSubject().Map(error));
        }
    }
}