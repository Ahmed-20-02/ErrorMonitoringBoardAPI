namespace DevelopmentProjectErrorBoardAPI.Enums
{
    using System.ComponentModel;

    public enum StatusEnum
    {
        [Description("In Progress")]
        InProgress = 1,
        [Description("Awaiting Information")]
        AwaitingInformation = 2,
        [Description("Escalated")]
        Escalated = 3,
        [Description("Cancelled")]
        Cancelled = 4,
        [Description("Resolved")]
        Resolved = 5
    }
}