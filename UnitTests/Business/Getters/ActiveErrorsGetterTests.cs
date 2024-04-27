namespace UnitTests.Business.Getters
{
    using Moq;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Business.Getters;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Logger;
    using UnitTests;

    public class ActiveErrorsGetterTests : TestBase<ActiveErrorsGetter>
    {
        [Fact]
        public async Task Get_ReturnsCorrectlyMappedErrors()
        {
            var stubErrorId = 10;

            var stubErrors = new List<Error> { new Error { ErrorId = stubErrorId }, new Error { ErrorId = 20 } };
            var stubLogPaths = new List<ErrorLogPath>
                { new ErrorLogPath { ErrorLogPathId = 1, ErrorId = stubErrorId } };

            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));

            this.AutoMocker.GetMock<IGetActiveErrorsQuery>()
                .Setup(x => x.Get())
                .ReturnsAsync(stubErrors);

            this.AutoMocker.GetMock<IGetLogPathForErrorQuery>()
                .Setup(x => x.Get(stubErrorId))
                .ReturnsAsync(stubLogPaths);

            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Setup(x => x.Map(It.IsAny<Error>()))
                .Returns(new ErrorModel { ErrorId = 10 });

            this.AutoMocker.GetMock<IErrorLogPathModelMapper>()
                .Setup(x => x.Map(It.IsAny<ErrorLogPath>()))
                .Returns(new ErrorLogPathModel { ErrorLogPathId = 1, ErrorId = stubErrorId });

            var sut = this.CreateTestSubject();

            var result = await sut.Get();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Single(result.FirstOrDefault()?.LogPaths);

            this.AutoMocker.GetMock<IGetActiveErrorsQuery>()
                .Verify(x => x.Get(), Times.Once);
            this.AutoMocker.GetMock<IGetLogPathForErrorQuery>()
                .Verify(x => x.Get(stubErrorId), Times.Once);
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Verify(x => x.Map(It.IsAny<Error>()), Times.Exactly(2));
            this.AutoMocker.GetMock<IErrorLogPathModelMapper>()
                .Verify(x => x.Map(It.IsAny<ErrorLogPath>()), Times.Once);
        }
        
        [Fact]
        public async Task Get_HitsException()
        {
            this.AutoMocker.GetMock<IGetActiveErrorsQuery>()
                .Setup(x => x.Get())
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Get());
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}