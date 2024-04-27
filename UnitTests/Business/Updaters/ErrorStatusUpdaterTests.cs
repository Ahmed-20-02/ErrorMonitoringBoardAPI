namespace UnitTests.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Updaters;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using Moq;

    public class ErrorStatusUpdaterTests : TestBase<ErrorStatusUpdater>
    {
        [Fact]
        public async Task Update_UpdatesStatus()
        {
            var stubErrorId = 10;
            var statusId = 15;
            var statusIdTwo = 20;
            var stubError = new Error { ErrorId = stubErrorId, StatusId = statusId};
            
            this.AutoMocker.GetMock<IUpdateErrorStatusCommand>()
                .Setup(x => x.Update(stubErrorId, statusIdTwo))
                .ReturnsAsync(stubError);
            
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Setup(x => x.Map(stubError))
                .Returns(new ErrorModel { ErrorId = stubErrorId, StatusId = statusIdTwo});
            
            var sut = this.CreateTestSubject();

            var result = await sut.Update(stubErrorId, statusIdTwo);
            
            Assert.NotNull(result);
            
            Assert.Equal(stubErrorId, result.ErrorId);
            Assert.Equal(statusIdTwo, result.StatusId);
            
            this.AutoMocker.GetMock<IUpdateErrorStatusCommand>()
                .Verify(x => x.Update(stubErrorId, statusIdTwo), Times.Once);
            
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Verify(x => x.Map(stubError), Times.Once);
        }
        
        [Fact]
        public async Task UpdateHitsException()
        {
            this.AutoMocker.GetMock<IUpdateErrorStatusCommand>()
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Update(It.IsAny<int>(),It.IsAny<int>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}