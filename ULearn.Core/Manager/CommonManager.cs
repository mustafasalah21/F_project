using AutoMapper;
using System.Linq;
using ULearn.Common.Extensions;
using ULearn.Core.Manager.Interfaces;
using ULearn.DbModel.Models;
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

            mappedUser.Permissions = _ulearndbContext.UserPermissionView.Where(a => a.UserId == user.Id).ToList();
            return mappedUser;
        }
    }
}
