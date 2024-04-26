namespace DevelopmentProjectErrorBoardAPI.Services
{
    using System.Net.Mail;
    using System.Text;
    using DevelopmentProjectErrorBoardAPI.Data.Entities;
    using System.ComponentModel;
    using DevelopmentProjectErrorBoardAPI.Enums;
    using DevelopmentProjectErrorBoardAPI.Services.Interfaces;
    
    public class EmailService : IEmailService
    {
        string client = "smtp.gmail.com";
        string Sender = "errorboardsystem.proj@gmail.com";
        string Password = "yfoo qete ognm hlpo";

        public void SendEmail(User agent, User dev, int? customerId, int status, int errorId)
        {
            StringBuilder emailTemplate = BuildEmail(agent, dev, customerId, status, errorId);

            MailMessage mail = new MailMessage(Sender, agent.EmailAddress, $"Error Update for customer id {customerId}",
                emailTemplate.ToString());
            SmtpClient Client = new SmtpClient(client);
            Client.Port = 587;
            Client.Credentials = new System.Net.NetworkCredential(Sender, Password);
            Client.EnableSsl = true;
            Client.Send(mail);
        }

        public StringBuilder BuildEmail(User user, User dev, int? customerId, int status, int errorId)
        {
            StringBuilder template = new StringBuilder();
            
            template.AppendLine($"Hi {user.FirstName},");
            template.AppendLine();
            if (customerId > 0)
            {
                template.AppendLine($"Thank you for your patience. The problem you faced " +
                                    $"while handling the account of the customer with the account Id {customerId} " +
                                    $"has been updated to status '{GetEnumDescription((StatusEnum)status)}' by developer {dev.FirstName} {dev.LastName}.");
            }
            else
            {
                template.AppendLine("Thank you for your patience. Your issue has been updated to status " +
                                    $"'{GetEnumDescription((StatusEnum)status)}' by developer {dev.FirstName} {dev.LastName}.");
            }
            template.AppendLine();
            template.AppendLine($"If you require further information please contact {dev.FirstName} using the following " +
                                $"email address '{dev.EmailAddress}'.");
            template.AppendLine();
            template.AppendLine($"REFERENCE: {errorId}");
            template.AppendLine();
            template.AppendLine("- Development Team");

            return template;
        }
        
        public string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}