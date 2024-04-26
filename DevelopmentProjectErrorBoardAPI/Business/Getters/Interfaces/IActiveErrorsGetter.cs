namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public interface IActiveErrorsGetter
    {
       Task <List<ErrorAndPathModel>> Get();
    }
}