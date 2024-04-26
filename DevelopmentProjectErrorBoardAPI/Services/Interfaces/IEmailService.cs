namespace DevelopmentProjectErrorBoardAPI.Services.Interfaces
{
    using System.Text;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;

    public interface IEmailService
    {
        void SendEmail(User agent, User dev, int? customerId, int status, int errorId);
        StringBuilder BuildEmail(User user, User dev, int? customerId, int status, int errorId);
        string GetEnumDescription(Enum value);
    }
}