namespace UnitTests.Business.Processors
{
    using DevelopmentProjectErrorBoardAPI.Business.Creators.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    using DevelopmentProjectErrorBoardAPI.Business.Processors;
    using DevelopmentProjectErrorBoardAPI.Enums;
    using Moq;

    public class CreateErrorProcessorTests : TestBase<CreateErrorProcessor>
    {
        [Fact]
        public async Task Process_CreatesError()
        {
            var stubErrorId = 10;
            var stubCustomerId = 80;
            var stubDevId = 50;
            var stubAgentId = (int)RolesEnum.Agent;
            var stubMessage = "stubMessage";
            var stubFile = "stubFile";
            var stubRequest = new CreateErrorRequest
            {
                Error = new CreateErrorModel{Message = stubMessage, 
                    DeveloperId = stubDevId, 
                    AgentId = stubAgentId, 
                    LineNumber = 50, 
                    ProjectId = 23, 
                    CustomerId = stubCustomerId, 
                    InitialFile = "initial"}, 
                LogPaths = new List<CreateLogPathModel>{new CreateLogPathModel
                {
                    FileName = stubFile
                }}
            };
            var stubErrorModel = new ErrorModel { ErrorId = stubErrorId, Message = stubMessage};
            var stubLogPathModel = new ErrorLogPathModel { ErrorId = stubErrorId, FileName = stubFile};
            
            this.AutoMocker.GetMock<IErrorCreator>()
                .Setup(x => x.Create(stubRequest.Error))
                .ReturnsAsync(stubErrorModel);
            
            this.AutoMocker.GetMock<ILogPathCreator>()
                .Setup(x => x.Create(stubRequest.LogPaths[0], stubErrorId))
                .ReturnsAsync(stubLogPathModel);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Process(stubRequest);
            
            Assert.NotNull(result);
            
            Assert.Equal(stubErrorId, result.Error.ErrorId);
            
            this.AutoMocker.GetMock<IErrorCreator>()
                .Verify(x => x.Create(stubRequest.Error), Times.Once);
            
            this.AutoMocker.GetMock<ILogPathCreator>()
                .Verify(x => x.Create(stubRequest.LogPaths[0], stubErrorId), Times.Once);
        }
        
        [Fact]
        public async Task Process_HitsException()
        {
            this.AutoMocker.GetMock<IErrorCreator>()
                .Setup(x => x.Create(It.IsAny<CreateErrorModel>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Process(new CreateErrorRequest()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}