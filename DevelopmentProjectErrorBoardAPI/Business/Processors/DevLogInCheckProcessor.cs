namespace DevelopmentProjectErrorBoardAPI.Business.Processors
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Enums;
    using DevelopmentProjectErrorBoardAPI.Resources;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Processors.Interfaces;

    public class DevLogInCheckProcessor : IDevLogInCheckProcessor
    {
        private readonly IUserByEmailAndPasswordGetter _userByEmailAndPasswordGetter;
        private readonly IUserModelMapper _userModelMapper;

        public DevLogInCheckProcessor(IUserByEmailAndPasswordGetter userByEmailAndPasswordGetter, 
            IUserModelMapper userModelMapper)
        {
            _userByEmailAndPasswordGetter = userByEmailAndPasswordGetter;
            _userModelMapper = userModelMapper;
        }

        public DevCheckLogInModel Process(LogInModel logInModel)
        {
            try
            {
                var user = _userByEmailAndPasswordGetter.Get(logInModel.EmailAddress, logInModel.Password);

                DevCheckLogInModel model = new DevCheckLogInModel();

                if (user.Result == null)
                {
                    model.Message = "No user found with that email and password";
                    model.IsAuthenticated = false;
                }

                else if (user.Result.FirstName != string.Empty && user.Result.RoleId != (int)RolesEnum.Developer)
                {
                    model.Message = $"You should not be here {user.Result.FirstName}, fired.";
                    model.IsAuthenticated = false;
                }

                else if (user.Result.FirstName != string.Empty && user.Result.RoleId == (int)RolesEnum.Developer)
                {
                    model.User = _userModelMapper.Map(user.Result);
                    model.Message = "Log In Successful";
                    model.IsAuthenticated = true;
                }

                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}