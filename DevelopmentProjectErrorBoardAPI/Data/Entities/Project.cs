namespace DevelopmentProjectErrorBoardAPI.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("project")]
    public class Project
    {
        [Key]
        [Column("ProjectId")]
        public int ProjectId { get; set; }
        
        [Column("Name")]
        public string Name { get; set; }
    }
}