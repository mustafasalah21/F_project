using System.Collections.Generic;
using System.ComponentModel;
using ULearn.DbModel.Models.DB.RoleModels;

namespace ULearn.ModelView.ModelView
{
    public class UserModel
    {
        public UserModel()
        {
            Permissions = new List<UserPermissionView>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DefaultValue("")]
        public string Phone { get; set; }

        [DefaultValue("")]
        public string Image { get; set; }

        public string ImageString { get; set; }

        public string Email { get; set; }

        public bool IsSuperAdmin { get; set; }

        public List<UserPermissionView> Permissions { get; set; }
    }
}