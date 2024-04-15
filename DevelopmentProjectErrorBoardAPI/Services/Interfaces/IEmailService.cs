namespace DevelopmentProjectErrorBoardAPI.Services
{
    using System.Text;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IEmailService
    {
        void SendEmail(User agent, User dev, int? customerId, int status);
        StringBuilder BuildEmail(User user, User dev, int? customerId, int status);
        string GetEnumDescription(Enum value);
    }
}