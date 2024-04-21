namespace DevelopmentProjectErrorBoardAPI.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class DeactivateError : IDeactivateError
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public DeactivateError(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public Error Deactivate(int errorId)
        {
            _logger.Log($"Deactivating errorId {errorId}");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    // Find the specific error
                    var error = context.Errors.FirstOrDefault(e => e.ErrorId == errorId);
                    
                    // Check if the error exists
                    if (error != null)
                    {
                        // Update the error
                        error.IsActive = false;
                        
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