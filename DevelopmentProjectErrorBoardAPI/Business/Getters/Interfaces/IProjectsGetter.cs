namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IProjectsGetter
    {
        List<ProjectModel> Get();
    }
}