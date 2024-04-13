namespace DevelopmentProjectErrorBoardAPI.Data.Updaters
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Enums;

    public class UpdateErrorStatus : IUpdateErrorStatus
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public UpdateErrorStatus(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public Error Update(int errorId, int statusId)
        {
            _logger.Log($"Updating errorId {errorId} status to {(StatusEnum)statusId} ");

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
                        error.StatusId = statusId;
                
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