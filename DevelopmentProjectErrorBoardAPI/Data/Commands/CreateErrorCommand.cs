namespace DevelopmentProjectErrorBoardAPI.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using ILogger = DevelopmentProjectErrorBoardAPI.Logger.ILogger;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Enums;
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public class CreateErrorCommand : ICreateErrorCommand
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ILogger _logger;

        public CreateErrorCommand(IDbContextFactory<DataContext> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<Error> Create(CreateErrorModel request)
        {
            _logger.Log($"Creating error for exception message {request.Message}");

            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    if (!string.IsNullOrEmpty(request.Message))
                    {
                        var error = new Error();

                        error.InitialFile = request.InitialFile;
                        error.LineNumber = request.LineNumber;
                        error.CreatedDate = DateTime.Now;
                        error.UpdatedDate = DateTime.Now;
                        error.Message = request.Message;
                        error.IsActive = true;
                        error.AgentId = request.AgentId;
                        if (request.DeveloperId > 0)
                        {
                            error.DeveloperId = request.DeveloperId;
                        }
                        error.StatusId = (int)StatusEnum.InProgress;
                        if (request.CustomerId > 0)
                        {
                            error.CustomerId = request.CustomerId;
                        }
                        error.ProjectId = request.ProjectId;
                        
                        context.Errors.Add(error);
                        
                        await context.SaveChangesAsync();
                        
                        return error;
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