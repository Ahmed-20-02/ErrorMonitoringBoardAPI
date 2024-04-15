namespace DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IUpdateErrorStatusProcessor
    {
        ErrorModel Process(UpdateErrorStatusModel model);
    }
}