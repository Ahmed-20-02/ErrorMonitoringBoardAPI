namespace DevelopmentProjectErrorBoardAPI.Resources
{
    public class ErrorAndPathModel
    {
        public ErrorModel Error { get; set; }
        public List<ErrorLogPathModel> LogPaths { get; set; }
    }
}