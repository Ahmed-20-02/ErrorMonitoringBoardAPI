namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;
    using Moq;

    public class GetProjectsQueryTests : TestBase<GetProjectsQuery>
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetProjectsQueryTests(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<Project>> Get()
        {
            _logger.Log("Getting Projects");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var result = await context.Projects.ToListAsync();
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