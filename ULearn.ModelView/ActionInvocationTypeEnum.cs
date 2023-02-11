using System.ComponentModel;

namespace ULearn.ModelView
{
    public enum ActionInvocationTypeEnum
    {
        [Description("Email Confirmation")]
        EmailConfirmation = 1,
        [Description("Reset Password")]
        ResetPassword = 2
    }
}
