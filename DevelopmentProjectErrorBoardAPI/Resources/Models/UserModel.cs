namespace DevelopmentProjectErrorBoardAPI.Resources.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        
        public int RoleId { get; set; }
    }
}