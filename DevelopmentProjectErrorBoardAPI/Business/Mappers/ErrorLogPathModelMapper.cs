namespace DevelopmentProjectErrorBoardAPI.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;

    public class ErrorLogPathModelMapper : IErrorLogPathModelMapper
    {
        public ErrorLogPathModel Map(ErrorLogPath logPath)
        {
            try
            {
                return new ErrorLogPathModel()
                {
                    ErrorLogPathId = logPath.ErrorLogPathId,
                    ErrorId = logPath.ErrorId,
                    FileName = logPath.FileName,
                    CreatedDate = logPath.CreatedDate,
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