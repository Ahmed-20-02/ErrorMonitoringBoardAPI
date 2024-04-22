namespace DevelopmentProjectErrorBoardAPI.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Enums;

    public class GetActiveErrorsQuery : IGetActiveErrorsQuery
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetActiveErrorsQuery(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<List<Error>> Get()
        {
            _logger.Log("Getting Errors");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var errors = await context.Errors.Where(x => x.IsActive).ToListAsync();
                    return errors;
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