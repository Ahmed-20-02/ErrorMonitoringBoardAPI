namespace DevelopmentProjectErrorBoardAPI.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Enums;

    public class GetUnresolvedErrorsQuery : IGetUnresolvedErrorsQuery
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetUnresolvedErrorsQuery(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<Error>> Get()
        {
            _logger.Log("Getting Errors");

            using (var context = _contextFactory.CreateDbContext())
            {
                return context.Errors.Where(x => x.StatusId != (int)StatusEnum.Resolved
                && x.StatusId != (int)StatusEnum.Cancelled).ToListAsync().Result;
            }
        }
    }
}