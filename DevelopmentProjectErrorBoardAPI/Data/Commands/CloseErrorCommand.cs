namespace DevelopmentProjectErrorBoardAPI.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class CloseErrorCommand : ICloseErrorCommand
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public CloseErrorCommand(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<Error> Close(int errorId)
        {
            _logger.Log($"Deactivating errorId {errorId}");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var error = await context.Errors.FirstOrDefaultAsync(e => e.ErrorId == errorId);
                    
                    if (error != null)
                    {
                        error.IsActive = false;
                        error.UpdatedDate = DateTime.Now;
                        
                        context.SaveChanges();
                    }

                    return error;
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