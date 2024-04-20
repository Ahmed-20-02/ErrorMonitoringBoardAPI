namespace DevelopmentProjectErrorBoardAPI.Data.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUpdateUserPassword
    {
        User Update(int userId, string password);
    }
}