namespace UnitTests.Business.Processors
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    using DevelopmentProjectErrorBoardAPI.Services.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Processors;
    using DevelopmentProjectErrorBoardAPI.Enums;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using Moq;

    public class UpdateErrorStatusProcessorTests : TestBase<UpdateErrorStatusProcessor>
    {
        [Fact]
        public async Task Process_UpdatesStatusAndSendsEmail()
        {
            var stubErrorId = 10;
            var stubCustomerId = 80;
            var stubDevId = 50;
            var stubStatusIdTwo = 20;
            var stubAgentId = (int)RolesEnum.Agent;
            var stubAgent = new User() { UserId = stubAgentId };
            var stubDev = new User() { UserId = stubDevId };
            var stubRequest = new UpdateErrorStatusRequest {ErrorId = stubErrorId, StatusId = stubStatusIdTwo, AgentId = stubAgentId, CustomerId = stubCustomerId, DevId = stubDevId};
            
            this.AutoMocker.GetMock<IErrorStatusUpdater>()
                .Setup(x => x.Update(stubErrorId, stubStatusIdTwo))
                .ReturnsAsync(new ErrorModel{ErrorId = stubErrorId, StatusId = stubStatusIdTwo});
            
            this.AutoMocker.GetMock<IUserByIdGetter>()
                .Setup(x => x.Get(stubAgentId))
                .ReturnsAsync(stubAgent);
            
            this.AutoMocker.GetMock<IUserByIdGetter>()
                .Setup(x => x.Get(stubDevId))
                .ReturnsAsync(stubDev);
            
            this.AutoMocker.GetMock<IEmailService>()
                .Setup(x => x.SendEmail(stubAgent, stubDev, stubRequest.CustomerId, stubRequest.StatusId, stubRequest.ErrorId ));
            
            var sut = this.CreateTestSubject();

            var result = await sut.Process(stubRequest);
            
            Assert.NotNull(result);
            
            Assert.Equal(stubErrorId, result.ErrorId);
            
            this.AutoMocker.GetMock<IErrorStatusUpdater>()
                .Verify(x => x.Update(stubErrorId, stubStatusIdTwo), Times.Once);
            
            this.AutoMocker.GetMock<IUserByIdGetter>()
                .Verify(x => x.Get(stubAgentId), Times.Once);
            
            this.AutoMocker.GetMock<IUserByIdGetter>()
                .Verify(x => x.Get(stubDevId), Times.Once);
            
            this.AutoMocker.GetMock<IEmailService>()
                .Verify(x => x.SendEmail(stubAgent, stubDev, stubRequest.CustomerId, stubRequest.StatusId, stubRequest.ErrorId ), Times.Once);
        }
        
        [Fact]
        public async Task Process_UpdatesStatusAndDoesntSendEmail()
        {
            var stubErrorId = 10;
            var stubCustomerId = 80;
            var stubDevId = 50;
            var stubStatusIdTwo = 20;
            var stubAgentId = (int)RolesEnum.System;
            var stubAgent = new User() { UserId = stubAgentId };
            var stubDev = new User() { UserId = stubDevId };
            var stubRequest = new UpdateErrorStatusRequest {ErrorId = stubErrorId, StatusId = stubStatusIdTwo, AgentId = stubAgentId, CustomerId = stubCustomerId, DevId = stubDevId};
            
            this.AutoMocker.GetMock<IErrorStatusUpdater>()
                .Setup(x => x.Update(stubErrorId, stubStatusIdTwo))
                .ReturnsAsync(new ErrorModel{ErrorId = stubErrorId, StatusId = stubStatusIdTwo});
            
            var sut = this.CreateTestSubject();

            var result = await sut.Process(stubRequest);
            
            Assert.NotNull(result);
            
            Assert.Equal(stubErrorId, result.ErrorId);
            
            this.AutoMocker.GetMock<IErrorStatusUpdater>()
                .Verify(x => x.Update(stubErrorId, stubStatusIdTwo), Times.Once);
            
            this.AutoMocker.GetMock<IUserByIdGetter>()
                .Verify(x => x.Get(stubAgentId), Times.Never);
            
            this.AutoMocker.GetMock<IUserByIdGetter>()
                .Verify(x => x.Get(stubDevId), Times.Never);
            
            this.AutoMocker.GetMock<IEmailService>()
                .Verify(x => x.SendEmail(stubAgent, stubDev, stubRequest.CustomerId, stubRequest.StatusId, stubRequest.ErrorId ), Times.Never);
        }
        
        [Fact]
        public async Task Process_HitsException()
        {
            this.AutoMocker.GetMock<IErrorStatusUpdater>()
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Process(new UpdateErrorStatusRequest()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}