namespace DevelopmentProjectErrorBoardAPI.Resources.Models
{
    public class ErrorLogPathModel
    {
        public int ErrorLogPathId { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ErrorId { get; set; }
    }
}