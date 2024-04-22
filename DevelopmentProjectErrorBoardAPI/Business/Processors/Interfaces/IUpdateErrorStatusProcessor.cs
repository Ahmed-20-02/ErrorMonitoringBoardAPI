namespace DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IUpdateErrorStatusProcessor
    {
        Task<ErrorModel> Process(UpdateErrorStatusModel model);
    }
}