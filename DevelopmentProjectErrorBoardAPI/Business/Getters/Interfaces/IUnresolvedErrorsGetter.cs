namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IUnresolvedErrorsGetter
    {
        ErrorAndPathListModel Get();
    }
}