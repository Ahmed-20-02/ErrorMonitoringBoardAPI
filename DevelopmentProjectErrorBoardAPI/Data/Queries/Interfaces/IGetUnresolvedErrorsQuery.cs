namespace DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IGetUnresolvedErrorsQuery
    {
        Task<List<Error>> Get();
    }
}