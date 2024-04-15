namespace DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IGetUserByUserIdQuery
    {
        Task<User> Get(int userId);
    }
}