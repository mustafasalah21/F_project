using System;
using System.Collections.Generic;

#nullable disable

namespace ULearn.DbModel.Models.DB.RoleModels
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsArchived { get; set; }

        public virtual Module Module { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
