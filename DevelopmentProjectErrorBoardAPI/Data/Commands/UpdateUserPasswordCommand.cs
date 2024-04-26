namespace DevelopmentProjectErrorBoardAPI.Data.Commands
{
    using Microsoft.EntityFrameworkCore;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using DevelopmentProjectErrorBoardAPI.Data.Commands.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Services.Interfaces;

    public class UpdateUserPasswordCommand : IUpdateUserPasswordCommand
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IPasswordService _passwordService;

        public UpdateUserPasswordCommand(IDbContextFactory<DataContext> contextFactory, 
            IPasswordService passwordService)
        {
            _contextFactory = contextFactory;
            _passwordService = passwordService;
        }

        public async Task<User> Update(int userId, string password)
        {
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                    
                    if (user != null)
                    {
                        user.Password = _passwordService.HashPassword(password);
                        
                        await context.SaveChangesAsync();
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