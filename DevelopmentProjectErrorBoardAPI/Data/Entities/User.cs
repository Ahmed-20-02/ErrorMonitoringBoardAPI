namespace DevelopmentProjectErrorBoardAPI.Data.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    
    [Table("user")]
    public class User
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }
        
        [Column("FirstName")]
        public string FirstName { get; set; }
        
        [Column("LastName")]
        public string LastName { get; set; }
        
        [Column("EmailAddress")]
        public string EmailAddress { get; set; }
        
        [Column("Password")]
        public string Password { get; set; }
        
        [Column("RoleId"), ForeignKey("RoleId")]
        public Role Role { get; set; }   
        public int RoleId { get; set; }
    }
}