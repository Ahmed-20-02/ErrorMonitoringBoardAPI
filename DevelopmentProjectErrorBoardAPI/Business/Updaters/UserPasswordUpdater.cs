namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class UserPasswordUpdater : IUserPasswordUpdater
    {
        private readonly IUpdateUserPassword _updateUserPassword;

        public UserPasswordUpdater(IUpdateUserPassword updateUserPassword)
        {
            _updateUserPassword = updateUserPassword;
        }

        public User Update(int userId, string password)
        {
            try
            {
                var user = this._updateUserPassword.Update(userId, password);
            
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}