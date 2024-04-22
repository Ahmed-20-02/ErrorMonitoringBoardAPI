namespace DevelopmentProjectErrorBoardAPI.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Services;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;

    public class UpdateUserPassword : IUpdateUserPassword
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public UpdateUserPassword(IDbContextFactory<DataContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<User> Update(int userId, string password)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    // Find the specific record based on ID
                    var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                    
                    // Check if the record exists
                    if (user != null)
                    {
                        // Update the record
                        user.Password = PasswordService.HashPassword(password);
                        
                        context.SaveChanges();
                    }

                    return user;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}