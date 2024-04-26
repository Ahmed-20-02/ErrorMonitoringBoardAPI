namespace UnitTests.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Getters;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Logger;
    using Moq;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public class ProjectsGetterTests : TestBase<ProjectsGetter>
    {
        [Fact]
        public async Task GetReturnsCorrectlyMappedProjects()
        {
            var stubProjectId = 10;

            var stubProjects = new List<Project> { new Project { ProjectId = stubProjectId, Name = "ProjOne"}, 
                new Project{ ProjectId = 15, Name = "ProjTwo"} };

            this.AutoMocker.GetMock<ILogger>()
                .Setup(x => x.Log(It.IsAny<string>()));

            this.AutoMocker.GetMock<IGetProjectsQuery>()
                .Setup(x => x.Get())
                .ReturnsAsync(stubProjects);

            this.AutoMocker.GetMock<IProjectModelMapper>()
                .Setup(x => x.Map(It.IsAny<Project>()))
                .Returns(new ProjectModel{ ProjectId = stubProjectId, ProjectName = "ProjOne" });
            
            this.AutoMocker.GetMock<IProjectModelMapper>()
                .Setup(x => x.Map(It.IsAny<Project>()))
                .Returns(new ProjectModel{ ProjectId = 15, ProjectName = "ProjTwo" });

            var sut = this.CreateTestSubject();

            var result = await sut.Get();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            
            this.AutoMocker.GetMock<IGetProjectsQuery>()
                .Verify(x => x.Get(), Times.Once);
            this.AutoMocker.GetMock<IProjectModelMapper>()
                .Verify(x => x.Map(It.IsAny<Project>()), Times.Exactly(2));
        }
        
        [Fact]
        public async Task GetHitsException()
        {
            this.AutoMocker.GetMock<IGetProjectsQuery>()
                .Setup(x => x.Get())
                .ThrowsAsync(new Exception("ExceptionMessage"));

            var sut = this.CreateTestSubject();

            var ex = await Assert.ThrowsAsync<Exception>(() => sut.Get());
    
            Assert.IsType<Exception>(ex);
            Assert.Equal("ExceptionMessage", ex.Message);
        }
    }
}