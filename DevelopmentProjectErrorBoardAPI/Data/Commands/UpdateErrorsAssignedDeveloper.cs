namespace DevelopmentProjectErrorBoardAPI.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

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

        public async Task<Error> Update(int errorId, int devId)
        {
            _logger.Log($"Updating errorId {errorId} dev to {devId} ");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    // Find the specific error
                    var error = await context.Errors.FirstOrDefaultAsync(e => e.ErrorId == errorId);
                    
                    // Check if the error exists
                    if (error != null)
                    {
                        // Update the error
                        error.DeveloperId = devId == 1 ? null : devId;
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