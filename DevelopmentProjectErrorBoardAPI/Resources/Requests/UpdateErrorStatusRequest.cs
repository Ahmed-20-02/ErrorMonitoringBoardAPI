namespace DevelopmentProjectErrorBoardAPI.Resources.Requests
{
    public class UpdateErrorStatusRequest
    {
        public int ErrorId { get; set; }
        public int StatusId { get; set; }
        public int AgentId { get; set; }
        public int? CustomerId { get; set; }
        public int DevId { get; set; }
    }
}