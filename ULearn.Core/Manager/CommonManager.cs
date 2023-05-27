using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using ULearn.Common.Extensions;
using ULearn.Core.Manager.Interfaces;
using ULearn.DbModel.Models;
using ULearn.DbModel.Models.DB.RoleModels;
using ULearn.ModelView.ModelView;

namespace ULearn.Core.Manager
{
    public class CommonManager : ICommonManager
    {
        private ulearndbContext _ulearndbContext;
        private IMapper _mapper;

        public CommonManager(ulearndbContext ulearndbContext, IMapper mapper)
        {
            _ulearndbContext = ulearndbContext;
            _mapper = mapper;
        }

        public UserModel GetUserRole(UserModel user)
        {
            var dbUser = _ulearndbContext.Users
                                      .FirstOrDefault(a => a.Id == user.Id)
                                      ?? throw new ServiceValidationException("Invalid user id received");

            var mappedUser = _mapper.Map<UserModel>(dbUser);

            //mappedUser.Permissions = _ulearndbContext.UserPermissionView.Where(a => a.UserId == user.Id).ToList();
            var userRoles = _ulearndbContext.UserRoles.Where(m => m.UserId == user.Id).ToList();
			var rolesPermissions = _ulearndbContext.RolePermissions.ToList();
            List<RolePermission> userRolesPermissions = new();
            List<Permission> Permissions = new();
            var permissionList = _ulearndbContext.Permissions.ToList();
            mappedUser.Permissions = new();

			foreach (var rp in rolesPermissions)
            {
                if (userRolesPermissions.Any(m=>m.PermissionId==rp.PermissionId))
                    continue;
                if (userRoles.Any(m=>m.RoleId== rp.RoleId))
                {
                    var p =permissionList.FirstOrDefault(m=>m.Id==rp.PermissionId);
                    mappedUser.Permissions.Add(new()
                    {
                        Code = p.Code,
                        ModuleId = p.ModuleId,
                        RoleId=rp.RoleId,
                        RoleName=rp.Role.Name??"",
                        Title=p.Title,
                        UserId = user.Id,
                    });
	 //               Permissions.Add(p);

					//userRolesPermissions.Add(rp);
				}
            }
            return mappedUser;
        }
    }
}
