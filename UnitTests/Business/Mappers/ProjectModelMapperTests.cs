namespace UnitTests.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers;
    
    public class ProjectModelMapperTests : TestBase<ProjectModelMapper>
    {
        [Fact]
        public void Map_ReturnsCorrectlyMappedProject()
        {
            var stubProjectId = 10;

            var stubProject = new Project { ProjectId = stubProjectId};

            var sut = this.CreateTestSubject();

            var result = sut.Map(stubProject);

            Assert.NotNull(result);
            Assert.Equal(stubProjectId, result.ProjectId);
        }

        [Fact]
        public void Map_HitsException()
        {
            Project project = null;

            Assert.Throws<NullReferenceException>(() => this.CreateTestSubject().Map(project));
        }
    }
}