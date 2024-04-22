namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUpdateUserPassword
    {
        Task<User> Update(int userId, string password);
    }
}