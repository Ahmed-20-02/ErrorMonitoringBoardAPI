namespace DevelopmentProjectErrorBoardAPI.Data.Queries
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;

    public class GetProjectsQuery : IGetProjectsQuery
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public GetProjectsQuery(IDbContextFactory<DataContext> contextFactory,
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
                    return context.Projects.ToListAsync().Result;
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