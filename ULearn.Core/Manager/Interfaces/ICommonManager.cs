using ULearn.ModelView.ModelView;

namespace ULearn.Core.Manager.Interfaces
{
    public interface ICommonManager : IManager
    {
        UserModel GetUserRole(UserModel user);
    }
}
