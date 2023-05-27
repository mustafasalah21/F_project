using System.Collections.Generic;
using System.ComponentModel;
using ULearn.DbModel.Models.DB.RoleModels;

namespace ULearn.ModelView.Response
{
    public class LoginUserResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
		public string Phone { get; set; }

		[DefaultValue("")]
        public string Image { get; set; }
		public bool IsSuperAdmin { get; set; }
		public bool IsEmailConfirmed { get; set; }

		public string Email { get; set; }

        public string Token { get; set; }
        public int MyProperty { get; set; }
		public  List<string> Roles { get; set; }

	}
}
