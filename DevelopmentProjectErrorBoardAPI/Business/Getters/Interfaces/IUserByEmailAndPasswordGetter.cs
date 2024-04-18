namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    
    public interface IUserByEmailAndPasswordGetter
    {
        Task<User> Get(string email, string password);
    }
}