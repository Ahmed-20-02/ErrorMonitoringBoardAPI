namespace UnitTests.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using Moq;

    public class ErrorCloserTests : TestBase<ErrorCloser>
    {
        [Fact]
        public async Task Close_SetsIsActiveToFalse()
        {
            var stubErrorId = 10;
            var stubError = new Error { ErrorId = stubErrorId, IsActive = true};
            
            this.AutoMocker.GetMock<ICloseErrorCommand>()
                .Setup(x => x.Close(stubErrorId))
                .ReturnsAsync(stubError);
            
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Setup(x => x.Map(stubError))
                .Returns(new ErrorModel { ErrorId = stubErrorId, IsActive = false});
            
            var sut = this.CreateTestSubject();

            var result = await sut.Close(stubErrorId);
            
            Assert.NotNull(result);
            
            Assert.Equal(stubErrorId, result.ErrorId);
            Assert.False(result.IsActive);
            
            this.AutoMocker.GetMock<ICloseErrorCommand>()
                .Verify(x => x.Close(stubErrorId), Times.Once);
            
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Verify(x => x.Map(stubError), Times.Once);
        }
        
        [Fact]
        public async Task Close_HitsException()
        {
            this.AutoMocker.GetMock<ICloseErrorCommand>()
                .Setup(x => x.Close(It.IsAny<int>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Close(It.IsAny<int>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}