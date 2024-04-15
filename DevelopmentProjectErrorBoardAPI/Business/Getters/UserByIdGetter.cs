namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    
    public class UserByIdGetter : IUserByIdGetter
    {
        private readonly IGetUserByUserIdQuery _getUserByUserId;

        public UserByIdGetter(IGetUserByUserIdQuery getUserByUserId)
        {
            _getUserByUserId = getUserByUserId;
        }

        public async Task<User> Get(int? userId)
        {
            try
            {
                return _getUserByUserId.Get(userId).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}