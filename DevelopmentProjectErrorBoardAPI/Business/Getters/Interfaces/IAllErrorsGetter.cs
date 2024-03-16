namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IAllErrorsGetter
    {
        List<Error> Get();
    }
}