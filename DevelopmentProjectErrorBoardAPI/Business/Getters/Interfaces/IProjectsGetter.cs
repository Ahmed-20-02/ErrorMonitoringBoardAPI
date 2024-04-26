namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public interface IProjectsGetter
    {
        Task<List<ProjectModel>>Get();
    }
}