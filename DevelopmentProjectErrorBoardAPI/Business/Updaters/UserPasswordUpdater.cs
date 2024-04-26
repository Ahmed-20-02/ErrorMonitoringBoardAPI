namespace DevelopmentProjectErrorBoardAPI.Business.Updaters
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Updaters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class UserPasswordUpdater : IUserPasswordUpdater
    {
        private readonly IUpdateUserPasswordCommand _updateUserPasswordCommand;

        public UserPasswordUpdater(IUpdateUserPasswordCommand updateUserPasswordCommand)
        {
            _updateUserPasswordCommand = updateUserPasswordCommand;
        }

        public async Task<User> Update(int userId, string password)
        {
            try
            {
                var user = await this._updateUserPasswordCommand.Update(userId, password);
            
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