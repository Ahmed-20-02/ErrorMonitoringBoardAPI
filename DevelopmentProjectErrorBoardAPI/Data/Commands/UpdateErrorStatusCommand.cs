namespace DevelopmentProjectErrorBoardAPI.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Enums;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class UpdateErrorStatusCommand : IUpdateErrorStatusCommand
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public UpdateErrorStatusCommand(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<Error> Update(int errorId, int statusId)
        {
            _logger.Log($"Updating errorId {errorId} status to {(StatusEnum)statusId} ");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var error = await context.Errors.FirstOrDefaultAsync(e => e.ErrorId == errorId);
                    
                    if (error != null)
                    {
                        error.StatusId = statusId;
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