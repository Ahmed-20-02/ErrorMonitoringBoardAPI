namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;
    using Moq;
    
    public class GetUsersByRoleIdQueryTests : TestBase<GetUsersByRoleIdQuery>
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetUsersByRoleIdQueryTests(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<User>> Get(int roleId)
        {
            _logger.Log($"Getting users by role id {roleId}");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var result = await context.Users.Where(u => u.RoleId == roleId).ToListAsync();
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