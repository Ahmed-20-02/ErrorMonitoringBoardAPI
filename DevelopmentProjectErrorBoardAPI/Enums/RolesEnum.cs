namespace DevelopmentProjectErrorBoardAPI.Enums
{
    using System.ComponentModel;

    public enum RolesEnum
    {
        [Description("System")]
        System = 1,
        [Description("Developer")]
        Developer = 2,
        [Description("Agent")]
        Agent = 3,
        [Description("Customer")]
        Customer = 4
    }
}