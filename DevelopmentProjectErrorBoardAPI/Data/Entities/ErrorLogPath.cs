namespace DevelopmentProjectErrorBoardAPI.Data.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    [Table("error_log_path")]
    public class ErrorLogPath
    {
        [Key]
        [Column("ErrorLogPathId")]
        public int ErrorLogPathId { get; set; }
        
        [Column("FileName")]
        public string FileName { get; set; }
        
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
        
        [Column("ErrorId"), ForeignKey("ErrorId")]
        public Error Error { get; set; } 
        public int ErrorId { get; set; }
    }
}