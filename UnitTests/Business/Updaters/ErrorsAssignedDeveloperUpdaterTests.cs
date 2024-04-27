namespace UnitTests.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    using Moq;

    public class ErrorsAssignedDeveloperUpdaterTests : TestBase<ErrorsAssignedDeveloperUpdater>
    {
        [Fact]
        public async Task Update_UpdatesDeveloper()
        {
            var stubErrorId = 10;
            var stubDevIdOne = 15;
            var stubDevIdTwo = 20;
            var stubError = new Error { ErrorId = stubErrorId, DeveloperId = stubDevIdOne};
            
            this.AutoMocker.GetMock<IUpdateErrorsAssignedDeveloperCommand>()
                .Setup(x => x.Update(stubErrorId, stubDevIdTwo))
                .ReturnsAsync(stubError);
            
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Setup(x => x.Map(stubError))
                .Returns(new ErrorModel { ErrorId = stubErrorId, DeveloperId = stubDevIdTwo});
            
            var sut = this.CreateTestSubject();

            var result = await sut.Update(new UpdateErrorsAssignedDeveloperRequest{ErrorId = stubErrorId, DevId = stubDevIdTwo});
            
            Assert.NotNull(result);
            
            Assert.Equal(stubErrorId, result.ErrorId);
            Assert.Equal(stubDevIdTwo, result.DeveloperId);
            
            this.AutoMocker.GetMock<IUpdateErrorsAssignedDeveloperCommand>()
                .Verify(x => x.Update(stubErrorId, stubDevIdTwo), Times.Once);
            
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Verify(x => x.Map(stubError), Times.Once);
        }
        
        [Fact]
        public async Task Update_HitsException()
        {
            this.AutoMocker.GetMock<IUpdateErrorsAssignedDeveloperCommand>()
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Update(new UpdateErrorsAssignedDeveloperRequest()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}