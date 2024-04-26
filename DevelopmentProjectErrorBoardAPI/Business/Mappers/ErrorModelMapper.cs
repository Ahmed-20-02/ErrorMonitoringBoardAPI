namespace DevelopmentProjectErrorBoardAPI.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;

    public class ErrorModelMapper : IErrorModelMapper
    {
        public ErrorModel Map(Error error)
        {
            try
            {
                return new ErrorModel()
                {
                    ErrorId = error.ErrorId,
                    InitialFile = error.InitialFile,
                    LineNumber = error.LineNumber,
                    CreatedDate = error.CreatedDate,
                    UpdatedDate = error.UpdatedDate,
                    AgentId = error.AgentId,
                    DeveloperId = error.DeveloperId,
                    StatusId = error.StatusId,
                    Message = error.Message,
                    CustomerId = error.CustomerId,
                    IsActive = error.IsActive,
                    ProjectId = error.ProjectId
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}