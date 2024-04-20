namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    
    public class UsersByRoleIdGetter : IUsersByRoleIdGetter
    {
        private readonly IGetUsersByRoleIdQuery _getUsersByRoleIdQuery;
        private readonly IUserModelMapper _userModelMapper;

        public UsersByRoleIdGetter(IGetUsersByRoleIdQuery getUsersByRoleIdQuery, 
            IUserModelMapper userModelMapper)
        {
            _getUsersByRoleIdQuery = getUsersByRoleIdQuery;
            _userModelMapper = userModelMapper;
        }

        public List<UserModel> Get(int roleId)
        {
            try
            {
                var users = _getUsersByRoleIdQuery.Get(roleId).Result;
                var userModels = new List<UserModel>();

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