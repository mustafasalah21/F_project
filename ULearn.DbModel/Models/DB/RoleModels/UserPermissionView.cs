﻿namespace ULearn.DbModel.Models.DB.RoleModels
{
    public class UserPermissionView
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public string ModuleKey { get; set; }
    }
}
