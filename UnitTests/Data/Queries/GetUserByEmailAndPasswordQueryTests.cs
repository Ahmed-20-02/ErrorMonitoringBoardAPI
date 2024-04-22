namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;    
    using BCrypt.Net;
    using DevelopmentProjectErrorBoardAPI.Services;
    using Moq;
    
    public class GetUserByEmailAndPasswordQueryTests : TestBase<GetUserByEmailAndPasswordQuery>
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetUserByEmailAndPasswordQueryTests(IDbContextFactory<DataContext> contextFactory,
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
                    var user = await context.Users.FirstOrDefaultAsync(x => x.EmailAddress == email);
                    if (user == null)
                    {
                        return null;
                    }
                   return PasswordService.VerifyPassword(password, user.Password) ? user : null;
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