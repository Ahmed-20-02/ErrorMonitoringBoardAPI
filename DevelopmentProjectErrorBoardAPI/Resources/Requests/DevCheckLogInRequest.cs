namespace DevelopmentProjectErrorBoardAPI.Resources.Requests
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public class DevCheckLogInRequest
    {
        public UserModel? User { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}