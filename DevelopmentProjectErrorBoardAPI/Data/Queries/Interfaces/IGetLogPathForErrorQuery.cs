namespace DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IGetLogPathForErrorQuery
    {
        Task<List<ErrorLogPath>> Get(int errorId);
    }
}