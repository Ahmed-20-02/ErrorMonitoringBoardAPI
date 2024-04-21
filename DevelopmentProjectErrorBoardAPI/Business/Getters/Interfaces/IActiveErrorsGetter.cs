namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IActiveErrorsGetter
    {
        List<ErrorAndPathModel> Get();
    }
}