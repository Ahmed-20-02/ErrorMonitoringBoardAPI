namespace DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUpdateUserPassword
    {
        User Update(int userId, string password);
    }
}