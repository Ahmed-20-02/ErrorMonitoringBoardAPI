namespace DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IDevLogInCheckProcessor
    {
        DevCheckLogInModel Process(LogInModel logInModel);
    }
}