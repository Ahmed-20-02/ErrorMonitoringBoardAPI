namespace DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces
{
    using DevelopmentProjectErrorBoardAPI.Resources;

    public interface IUsersByRoleIdGetter
    {
        List<UserModel> Get(int roleId);
    }
}