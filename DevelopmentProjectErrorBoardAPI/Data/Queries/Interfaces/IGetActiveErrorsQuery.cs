namespace DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IGetActiveErrorsQuery
    {
        Task<List<Error>> Get();
    }
}