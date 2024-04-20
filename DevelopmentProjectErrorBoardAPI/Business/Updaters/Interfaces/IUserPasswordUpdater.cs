namespace DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUserPasswordUpdater
    {
        User Update(int userId, string password);
    }
}