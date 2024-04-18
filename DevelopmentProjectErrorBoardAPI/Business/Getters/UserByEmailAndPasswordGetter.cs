namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    
    public class UserByEmailAndPasswordGetter : IUserByEmailAndPasswordGetter
    {
        private readonly IGetUserByEmailAndPasswordQuery _getUserByUserIdByEmailAndPasswordQuery;

        public UserByEmailAndPasswordGetter(IGetUserByEmailAndPasswordQuery getUserByUserIdByEmailAndPasswordQuery)
        {
            _getUserByUserIdByEmailAndPasswordQuery = getUserByUserIdByEmailAndPasswordQuery;
        }

        public async Task<User> Get(string email, string password)
        {
            try
            {
                return _getUserByUserIdByEmailAndPasswordQuery.Get(email, password).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}