namespace DevelopmentProjectErrorBoardAPI.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("role")]
    public class Role
    {
        [Key]
        [Column("RoleId")]
        public int RoleId { get; set; }
        
        [Column("Name")]
        public string Name { get; set; }
    }
}