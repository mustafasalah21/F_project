using System.Collections.Generic;
using ULearn.DbModel.Models.DB;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Response;

namespace ULearn.Core.Manager.Interfaces
{
    public interface IUserManager : IManager
    {
        UserModel UpdateProfile(UserModel currentUser, UserModel request);
        void ChangeUserRole(UserModel loggedInUser, int userId, string newRole);
        LoginUserResponse Login(LoginModelView userReg);
        List<User> GettAll();

        LoginUserResponse SignUp(UserRegistrationModel userReg);

        void DeleteUser(UserModel currentUser, int id);

        UserModel Confirmation(string ConfirmationLink);
		void ChangePassword(UserModel loggedInUser, ChangePasswordModelView changePasswordModel);
	}
}
