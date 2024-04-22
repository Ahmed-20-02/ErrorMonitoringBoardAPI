namespace UnitTests.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data;
    using DevelopmentProjectErrorBoardAPI.Data.Queries;
    using Moq;

    public class GetLogPathForErrorQueryTests : TestBase<GetLogPathForErrorQuery>
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetLogPathForErrorQueryTests(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<ErrorLogPath>> Get(int errorId)
        {
            _logger.Log($"Getting Log path for Error {errorId}");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var result = await context.ErrorLogPaths.Where(x => x.ErrorId == errorId).ToListAsync();
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