namespace DevelopmentProjectErrorBoardAPI.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public class CreateLogPathCommand : ICreateLogPathCommand
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public CreateLogPathCommand(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<ErrorLogPath> Create(CreateLogPathModel request, int errorId)
        {
            _logger.Log($"Creating log path for error {errorId}");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    if (!string.IsNullOrEmpty(request.FileName))
                    {
                        var logPath = new ErrorLogPath();

                        logPath.FileName = request.FileName;
                        logPath.CreatedDate = DateTime.Now;
                        logPath.ErrorId = errorId;
                        
                        context.ErrorLogPaths.Add(logPath);
                        
                        await context.SaveChangesAsync();
                        
                        return logPath;
                    }

                    return null;

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