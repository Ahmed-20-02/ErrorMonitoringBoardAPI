namespace DevelopmentProjectErrorBoardAPI.Resources.Models
{
    public class CreateErrorModel
    {
        public string InitialFile { get; set; }
        
        public int LineNumber { get; set; }
        
        public int AgentId { get; set; }
        
        public int? DeveloperId { get; set; }
        
        public string Message { get; set; }
        
        public int ProjectId { get; set; }
        
        public int? CustomerId { get; set; }
    }
}