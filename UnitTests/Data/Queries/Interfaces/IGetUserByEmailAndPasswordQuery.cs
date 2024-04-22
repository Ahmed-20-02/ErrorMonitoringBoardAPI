namespace DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IGetUserByEmailAndPasswordQuery
    {
        Task<User> Get(string email, string password);
    }
}