namespace DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    
    public interface IProjectModelMapper
    {
        ProjectModel Map(Project project);
    }
}