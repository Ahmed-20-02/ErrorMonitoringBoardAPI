namespace UnitTests.Business.Creators
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Creators;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using Moq;

    public class ErrorCreatorTests : TestBase<ErrorCreator>
    {
        [Fact]
        public async Task Create_CreatesNewError()
        {
            var stubErrorId = 10;
            var stubMessage = "stubMessage";
            var stubRequest = new CreateErrorModel{Message = stubMessage};
            
            var stubError = new Error { ErrorId = stubErrorId, Message = "stubMessage"};
            
            this.AutoMocker.GetMock<ICreateErrorCommand>()
                .Setup(x => x.Create(stubRequest))
                .ReturnsAsync(stubError);
            
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Setup(x => x.Map(stubError))
                .Returns(new ErrorModel { ErrorId = stubErrorId, Message = stubError.Message});
            
            var sut = this.CreateTestSubject();

            var result = await sut.Create(stubRequest);
            
            Assert.NotNull(result);
            
            Assert.Equal(stubErrorId, result.ErrorId);
            
            Assert.Equal(stubMessage, result.Message);

            
            this.AutoMocker.GetMock<ICreateErrorCommand>()
                .Verify(x => x.Create(stubRequest), Times.Once);
            
            this.AutoMocker.GetMock<IErrorModelMapper>()
                .Verify(x => x.Map(stubError), Times.Once);
        }
        
        [Fact]
        public async Task Create_HitsException()
        {
            this.AutoMocker.GetMock<ICreateErrorCommand>()
                .Setup(x => x.Create(It.IsAny<CreateErrorModel>()))
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Create(new CreateErrorModel()));
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}