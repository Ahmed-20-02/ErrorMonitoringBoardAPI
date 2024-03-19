namespace DevelopmentProjectErrorBoardAPI.Business.Mappers
{
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public class ErrorLogPathModelMapper
    {
        public ErrorLogPathModel Map(ErrorLogPath logPath)
        {
            return new ErrorLogPathModel()
            {
                ErrorLogPathId = logPath.ErrorLogPathId,
                ErrorId = logPath.ErrorId,
                FileName = logPath.FileName,
                CreatedDate = logPath.CreatedDate,
            };
        }
    }
}