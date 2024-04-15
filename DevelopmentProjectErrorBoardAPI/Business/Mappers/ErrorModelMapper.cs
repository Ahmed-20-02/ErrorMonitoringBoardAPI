namespace DevelopmentProjectErrorBoardAPI.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;

    public class ErrorModelMapper : IErrorModelMapper
    {
        public ErrorModel Map(Error error)
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
                CustomerId = error.CustomerId
            };
        }

    }
}