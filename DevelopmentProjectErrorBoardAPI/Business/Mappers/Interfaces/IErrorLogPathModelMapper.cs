namespace DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Resources;
    
    public interface IErrorLogPathModelMapper
    {
        ErrorLogPathModel Map(ErrorLogPath logPath);
    }
}