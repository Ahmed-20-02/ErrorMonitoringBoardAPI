namespace DevelopmentProjectErrorBoardAPI.Data.Updaters
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Updaters.Interfaces;

    public class UpdateErrorsAssignedDeveloper : IUpdateErrorsAssignedDeveloper
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public UpdateErrorsAssignedDeveloper(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public Error Update(int errorId, int devId)
        {
            _logger.Log($"Updating errorId {errorId} dev to {devId} ");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    // Find the specific record based on ID
                    var error = context.Errors.FirstOrDefault(e => e.ErrorId == errorId);
                    
                    // Check if the record exists
                    if (error != null)
                    {
                        
                        // Update the record
                        error.DeveloperId = devId== 1 ? null : devId;
                
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