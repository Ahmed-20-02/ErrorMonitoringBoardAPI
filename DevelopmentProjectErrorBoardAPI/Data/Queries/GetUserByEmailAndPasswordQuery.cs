namespace DevelopmentProjectErrorBoardAPI.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Services.Interfaces;
    
    public class GetUserByEmailAndPasswordQuery : IGetUserByEmailAndPasswordQuery
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IPasswordService _passwordService;
        private readonly ILogger _logger;

        public GetUserByEmailAndPasswordQuery(IDbContextFactory<DataContext> contextFactory,
            ILogger logger, 
            IPasswordService passwordService)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            _passwordService = passwordService;
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
                    return _passwordService.VerifyPassword(password, user.Password) ? user : null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}