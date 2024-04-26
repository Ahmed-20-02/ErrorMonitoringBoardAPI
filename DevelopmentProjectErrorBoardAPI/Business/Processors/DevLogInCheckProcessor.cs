namespace DevelopmentProjectErrorBoardAPI.Business.Processors
{
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Enums;
    using DevelopmentProjectErrorBoardAPI.Resources.Requests;
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

        public async Task<DevCheckLogInRequest> Process(LogInRequest logInRequest)
        {
            try
            {
                var user = await _userByEmailAndPasswordGetter.Get(logInRequest.EmailAddress, logInRequest.Password);

                DevCheckLogInRequest request = new DevCheckLogInRequest();

                if (user == null)
                {
                    request.Message = "No user found with that email and password";
                    request.IsAuthenticated = false;
                }

                else if (user.FirstName != string.Empty && user.RoleId != (int)RolesEnum.Developer)
                {
                    request.Message = $"Only developers past this point {user.FirstName}";
                    request.IsAuthenticated = false;
                }

                else if (user.FirstName != string.Empty && user.RoleId == (int)RolesEnum.Developer)
                {
                    request.User = _userModelMapper.Map(user);
                    request.Message = "Log In Successful";
                    request.IsAuthenticated = true;
                }

                return request;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}