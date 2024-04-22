namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IActiveErrorsGetter
    {
       Task <List<ErrorAndPathModel>> Get();
    }
}