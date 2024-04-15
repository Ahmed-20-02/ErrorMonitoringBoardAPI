namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUserByIdGetter
    {
        Task<User> Get(int? userId);
    }
}