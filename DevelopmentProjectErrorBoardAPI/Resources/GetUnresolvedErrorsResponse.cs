namespace DevelopmentProjectErrorBoardAPI.Resources
{
    public class GetUnresolvedErrorsResponse
    {
        public ErrorAndPathListModel Errors { get; set; }
        public string Message { get; set; }
    }
}