namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;
    using Moq;

    public class GetUserByIdQueryTests : TestBase<GetUserByIdQuery>
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetUserByIdQueryTests(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<User> Get(int? userId)
        {
            _logger.Log($"Getting user by user id {userId}");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var result = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                    return result;                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}