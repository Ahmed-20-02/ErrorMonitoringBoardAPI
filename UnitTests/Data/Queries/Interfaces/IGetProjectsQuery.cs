namespace DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IGetProjectsQuery
    {
        Task<List<Project>> Get();
    }
}