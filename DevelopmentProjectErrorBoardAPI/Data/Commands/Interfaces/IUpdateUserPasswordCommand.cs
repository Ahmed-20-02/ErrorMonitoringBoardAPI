namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUpdateUserPasswordCommand
    {
        Task<User> Update(int userId, string password);
    }
}