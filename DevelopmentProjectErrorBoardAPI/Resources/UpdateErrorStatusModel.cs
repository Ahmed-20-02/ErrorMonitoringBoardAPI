namespace DevelopmentProjectErrorBoardAPI.Resources
{
    public class UpdateErrorStatusModel
    {
        public int ErrorId { get; set; }
        public int StatusId { get; set; }
        public int AgentId { get; set; }
        public int? CustomerId { get; set; }
        public int? DevId { get; set; }
    }
}