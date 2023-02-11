using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using ULearn.Common.Extensions;
using ULearn.Core.Manager.Interfaces;
using ULearn.DbModel.Models;
using ULearn.ModelView.ModelView;

namespace ULearn.Core.Manager
{
    public class RoleManager : IRoleManager
    {
        private ulearndbContext _ulearndbContext;
        private IMapper _mapper;
        public RoleManager(ulearndbContext ulearndbContext, IMapper mapper)
        {
            _ulearndbContext = ulearndbContext;
            _mapper = mapper;
        }

        public bool CheckAccess(UserModel userModel, List<string> persmissions)
        {
            var userTest = _ulearndbContext.Users
                                        .FirstOrDefault(a => a.Id == userModel.Id)
                                        ?? throw new ServiceValidationException("Invalid user id");

            if (userTest.IsSuperAdmin)
            {
                return true;
            }

            var userPermissions = _ulearndbContext.UserPermissionView.Where(a => a.UserId == userTest.Id).ToList();

            return userPermissions.Any(r => persmissions.Contains(r.Code));
        }
    }
}
