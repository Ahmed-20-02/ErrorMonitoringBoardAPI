namespace DevelopmentProjectErrorBoardAPI.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using BCrypt.Net;
    using DevelopmentProjectErrorBoardAPI.Services;
    
    public class GetUserByEmailAndPasswordQuery : IGetUserByEmailAndPasswordQuery
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetUserByEmailAndPasswordQuery(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<User> Get(string email, string password)
        {
            _logger.Log($"Getting user by email {email}");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var user = context.Users.FirstOrDefaultAsync(x => x.EmailAddress == email);
                    if (user.Result == null)
                    {
                        return null;
                    }
                     return PasswordService.VerifyPassword(password, user.Result.Password) ? user.Result : null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Verify(password, hash);
        }
    }
}