namespace UnitTests.Business.Processors
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Enums;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Processors;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using Moq;

    public class DevLogInCheckProcessorTests : TestBase<DevLogInCheckProcessor>
    {
        
        [Fact]
        public async Task Process_LogInReturnsNull()
        { 
            var stubExpectedResultMessage = "No user found with that email and password";
            var stubEmail = "testEmail";
            var stubPassword = "testPassword";
            var stubAgentId = (int)RolesEnum.Agent;
            var stubRequest = new LogInRequest {EmailAddress = stubEmail, Password = stubPassword};
            
            this.AutoMocker.GetMock<IUserByEmailAndPasswordGetter>()
                .Setup(x => x.Get(stubRequest.EmailAddress, stubRequest.Password))
                .ReturnsAsync((User)null);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Process(stubRequest);
            
            Assert.NotNull(result);
            
            Assert.Null(result.User);
            
            Assert.False(result.IsAuthenticated);
            
            Assert.Equal(stubExpectedResultMessage, result.Message);
            
            this.AutoMocker.GetMock<IUserByEmailAndPasswordGetter>()
                .Verify(x => x.Get(stubRequest.EmailAddress, stubRequest.Password), Times.Once);
        }
        
        [Fact]
        public async Task Process_LogInReturnsNonDeveloperUser()
        { 
            var stubDevId = 50;
            var stubExpectedResultMessage = "Only developers past this point";
            var stubEmail = "testEmail";
            var stubPassword = "testPassword";
            var stubRoleId= (int)RolesEnum.Agent;
            var stubDev = new User { UserId = stubDevId, EmailAddress = stubEmail, Password = stubPassword, FirstName = "TEST", RoleId = stubRoleId };
            var stubRequest = new LogInRequest {EmailAddress = stubEmail, Password = stubPassword};
            
            this.AutoMocker.GetMock<IUserByEmailAndPasswordGetter>()
                .Setup(x => x.Get(stubRequest.EmailAddress, stubRequest.Password))
                .ReturnsAsync(stubDev);
            
            var sut = this.CreateTestSubject();

            var result = await sut.Process(stubRequest);
            
            Assert.NotNull(result);
            
            Assert.Null(result.User);
            
            Assert.False(result.IsAuthenticated);
            
            Assert.Equal($"{stubExpectedResultMessage} {stubDev.FirstName}", result.Message);
            
            this.AutoMocker.GetMock<IUserByEmailAndPasswordGetter>()
                .Verify(x => x.Get(stubRequest.EmailAddress, stubRequest.Password), Times.Once);
        }
        
        [Fact]
        public async Task Process_LogInReturnsDeveloper()
        { 
            var stubDevId = 50;
            var stubExpectedResultMessage = "Log In Successful";
            var stubEmail = "testEmail";
            var stubPassword = "testPassword";
            var stubRoleId = (int)RolesEnum.Developer;
            var stubDev = new User { UserId = stubDevId, EmailAddress = stubEmail, Password = stubPassword, FirstName = "TEST", RoleId = stubRoleId};
            var stubRequest = new LogInRequest {EmailAddress = stubEmail, Password = stubPassword};
            
            this.AutoMocker.GetMock<IUserByEmailAndPasswordGetter>()
                .Setup(x => x.Get(stubRequest.EmailAddress, stubRequest.Password))
                .ReturnsAsync(stubDev);
            
            this.AutoMocker.GetMock<IUserModelMapper>()
                .Setup(x => x.Map(stubDev))
                .Returns(new UserModel{UserId = stubDevId, EmailAddress = stubEmail});
            
            var sut = this.CreateTestSubject();

            var result = await sut.Process(stubRequest);
            
            Assert.NotNull(result);
            
            Assert.True(result.IsAuthenticated);
            
            Assert.Equal(stubEmail, result.User.EmailAddress);

            Assert.Equal(stubExpectedResultMessage, result.Message);
            
            this.AutoMocker.GetMock<IUserByEmailAndPasswordGetter>()
                .Verify(x => x.Get(stubRequest.EmailAddress, stubRequest.Password), Times.Once);
            
            this.AutoMocker.GetMock<IUserModelMapper>()
                .Verify(x => x.Map(stubDev), Times.Once);
        }
        
        [Fact]
        public async Task Process_HitsException()
        {
            this.AutoMocker.GetMock<IUserByEmailAndPasswordGetter>()
                .Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Process(new LogInRequest()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}