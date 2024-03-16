namespace DevelopmentProjectErrorBoardAPI.Data.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    [Table("error")]
    public class Error
    {
        [Key]
        [Column("ErrorId")]
        public int ErrorId { get; set; }
        
        [Column("InitialFile")]
        public string InitialFile { get; set; }
        
        [Column("LineNumber")]
        public string LineNumber { get; set; }
        
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
        
        [Column("UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
        
        [Column("AgentId"), ForeignKey("AgentId")]
        public User Agent { get; set; } 
        public int AgentId { get; set; }
        
        [Column("DeveloperId"), ForeignKey("DeveloperId")]
        public User Developer { get; set; } 
        public int DeveloperId { get; set; }
        
        [Column("StatusId"), ForeignKey("StatusId")]
        public Status Status { get; set; } 
        public int StatusId { get; set; }
    }
}