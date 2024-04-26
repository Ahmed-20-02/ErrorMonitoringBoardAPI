namespace DevelopmentProjectErrorBoardAPI.Resources.Requests
{
    using DevelopmentProjectErrorBoardAPI.Resources.Models;

    public class CreateErrorRequest
    {
        public CreateErrorModel Error { get; set; }
        public List<CreateLogPathModel> LogPaths { get; set; }
    }
}