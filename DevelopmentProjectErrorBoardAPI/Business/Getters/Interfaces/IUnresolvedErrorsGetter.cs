namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IUnresolvedErrorsGetter
    {
        List<Error> Get();
    }
}