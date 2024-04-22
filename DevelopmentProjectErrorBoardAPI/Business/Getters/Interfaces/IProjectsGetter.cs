namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IProjectsGetter
    {
        Task<List<ProjectModel>>Get();
    }
}