namespace DevelopmentProjectErrorBoardAPI.Resources
{
    public class ErrorModel
    {
        public int ErrorId { get; set; }
        
        public string InitialFile { get; set; }
        
        public int LineNumber { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }
        
        public int AgentId { get; set; }
        
        public int DeveloperId { get; set; }
        
        public int StatusId { get; set; }
    }
}