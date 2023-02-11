using System.Collections.Generic;
using ULearn.ModelView.ModelView;

namespace ULearn.Core.Manager.Interfaces
{
    public interface IRoleManager : IManager
    {
        bool CheckAccess(UserModel userModel, List<string> persmissions);
    }
}
