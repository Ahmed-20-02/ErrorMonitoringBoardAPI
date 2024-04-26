namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Enums;
    
    public class DevelopersGetter : IDevelopersGetter
    {
        private readonly IGetUsersByRoleIdQuery _getUsersByRoleIdQuery;
        private readonly IUserModelMapper _userModelMapper;

        public DevelopersGetter(IGetUsersByRoleIdQuery getUsersByRoleIdQuery, 
            IUserModelMapper userModelMapper)
        {
            _getUsersByRoleIdQuery = getUsersByRoleIdQuery;
            _userModelMapper = userModelMapper;
        }

        public async Task<List<UserModel>> Get()
        {
            try
            {
                var users = await _getUsersByRoleIdQuery.Get((int)RolesEnum.Developer);
                var userModels = new List<UserModel>();

                userModels.Add(new UserModel()
                {
                    EmailAddress = "Unassigned",
                    FirstName = "Unassigned",
                    LastName = " ",
                    RoleId = 2,
                    UserId = 1
                });
                
                for (int i = 0; i < users.Count; i++)
                {
                    userModels.Add(_userModelMapper.Map(users[i]));
                }

                return userModels;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}