using System;
using System.Collections.Generic;

#nullable disable

namespace ULearn.DbModel.Models.DB.RoleModels
{
    public partial class Module
    {
        public Module()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsArchived { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
