namespace UnitTests.Business.Creators
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Creators;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using Moq;

    public class LogPathCreatorTests : TestBase<LogPathCreator>
    {
        [Fact]
        public async Task Create_CreatesNewLogPath()
        {
            var stubErrorId = 10;
            var stubFile = "stubFile";
            var stubRequest = new CreateLogPathModel{FileName = stubFile};
            
            var stubLogpath = new ErrorLogPath() { ErrorId = stubErrorId, FileName = stubFile};
            
            this.AutoMocker.GetMock<ICreateLogPathCommand>()
                .Setup(x => x.Create(stubRequest, stubErrorId))
                .ReturnsAsync(stubLogpath);
            
            this.AutoMocker.GetMock<IErrorLogPathModelMapper>()
                .Setup(x => x.Map(stubLogpath))
                .Returns(new ErrorLogPathModel() { ErrorId = stubErrorId, FileName = stubLogpath.FileName});
            
            var sut = this.CreateTestSubject();

            var result = await sut.Create(stubRequest, stubErrorId);
            
            Assert.NotNull(result);
            
            Assert.Equal(stubErrorId, result.ErrorId);
            
            Assert.Equal(stubFile, result.FileName);

            
            this.AutoMocker.GetMock<ICreateLogPathCommand>()
                .Verify(x => x.Create(stubRequest, stubErrorId), Times.Once);
            
            this.AutoMocker.GetMock<IErrorLogPathModelMapper>()
                .Verify(x => x.Map(stubLogpath), Times.Once);
        }
        
        [Fact]
        public async Task Create_HitsException()
        {
            this.AutoMocker.GetMock<ICreateLogPathCommand>()
                .Setup(x => x.Create(It.IsAny<CreateLogPathModel>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Create(new CreateLogPathModel(), It.IsAny<int>()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}