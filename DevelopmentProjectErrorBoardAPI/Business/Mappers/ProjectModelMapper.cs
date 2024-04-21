namespace DevelopmentProjectErrorBoardAPI.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    
    public class ProjectModelMapper : IProjectModelMapper
    {
        public ProjectModel Map(Project project)
        {
            return new ProjectModel()
            {
                ProjectId = project.ProjectId,
                ProjectName = project.Name
            };
        }
    }
}