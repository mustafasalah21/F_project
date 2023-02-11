using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ULearn.DbModel.Models.DB.RoleModels;

#nullable disable

namespace ULearn.DbModel.Models.DB
{
    public partial class User
    {
        public User()
        {
            Courses = new HashSet<Course>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        [DefaultValue("")]
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string ConfirmationLink { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }
        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool IsArchived { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
