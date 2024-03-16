namespace DevelopmentProjectErrorBoardAPI.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("status")]
    public class Status
    {
        [Key]
        [Column("StatusId")]
        public int RoleId { get; set; }
        
        [Column("Name")]
        public string Name { get; set; }
    }
}