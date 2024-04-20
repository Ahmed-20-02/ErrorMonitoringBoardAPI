namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;

    public class UserPasswordUpdater : IUserPasswordUpdater
    {
        private readonly IUpdateUserPassword _updateUserPassword;

        public UserPasswordUpdater(IUpdateUserPassword updateUserPassword)
        {
            _updateUserPassword = updateUserPassword;
        }

        public User Update(int userId, string password)
        {
            var user = this._updateUserPassword.Update(userId, password);
            
            return user;
        }
    }
}