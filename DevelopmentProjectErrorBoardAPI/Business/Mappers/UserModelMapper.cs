namespace DevelopmentProjectErrorBoardAPI.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    
    public class UserModelMapper : IUserModelMapper
    {
        public UserModel Map(User user)
        {
            return new UserModel()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress, 
                RoleId = user.RoleId,
            };
        }
    }
}