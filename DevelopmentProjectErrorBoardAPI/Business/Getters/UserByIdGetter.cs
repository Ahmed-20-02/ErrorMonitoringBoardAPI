namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    
    public class UserByIdGetter : IUserByIdGetter
    {
        private readonly IGetUserByIdQuery _getUserByUserId;

        public UserByIdGetter(IGetUserByIdQuery getUserByUserId)
        {
            _getUserByUserId = getUserByUserId;
        }

        public async Task<User> Get(int? userId)
        {
            try
            {
                var result = await _getUserByUserId.Get(userId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}