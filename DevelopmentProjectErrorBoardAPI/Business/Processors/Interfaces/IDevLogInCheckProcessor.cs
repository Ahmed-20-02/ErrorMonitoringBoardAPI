namespace DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IDevLogInCheckProcessor
    {
        Task<DevCheckLogInModel> Process(LogInModel logInModel);
    }
}