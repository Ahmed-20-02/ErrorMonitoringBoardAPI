namespace DevelopmentProjectErrorBoardAPI.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;

    public class GetLogPathForErrorQuery : IGetLogPathForErrorQuery
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetLogPathForErrorQuery(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<ErrorLogPath>> Get(int errorId)
        {
            _logger.Log($"Getting Log path fot Error {errorId}");

            using (var context = _contextFactory.CreateDbContext())
            {
                return context.ErrorLogPaths.Where(x => x.ErrorId == errorId).ToListAsync().Result;
            }
        }
    }
}