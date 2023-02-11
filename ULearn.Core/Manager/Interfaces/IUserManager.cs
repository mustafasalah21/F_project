using ULearn.ModelView.ModelView;
using ULearn.ModelView.Response;

namespace ULearn.Core.Manager.Interfaces
{
    public interface IUserManager : IManager
    {
        UserModel UpdateProfile(UserModel currentUser, UserModel request);

        LoginUserResponse Login(LoginModelView userReg);

        LoginUserResponse SignUp(UserRegistrationModel userReg);

        void DeleteUser(UserModel currentUser, int id);

        UserModel Confirmation(string ConfirmationLink);
    }
}
