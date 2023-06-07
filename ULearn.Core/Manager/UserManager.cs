using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ULearn.Common.Extensions;
using ULearn.Common.Helper;
using ULearn.Core.Manager.Interfaces;
using ULearn.Core.Managers;
using ULearn.DbModel.Models;
using ULearn.DbModel.Models.DB;
using ULearn.EmailService;
using ULearn.EmailService.Implementation;
using ULearn.infrastructure;
using ULearn.ModelView;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Response;
using ULearn.ModelView.Result;
using ULearn.ModelView.Static;

namespace ULearn.Core.Manager
{
    public class UserManager : IUserManager
    {
        private ulearndbContext _ulearndbContext;
        private IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IConfigurationSettings _configurationSettings;
		private readonly IHelperManager _helperManager;

		public UserManager(ulearndbContext ulearndbContext, IMapper mapper, IEmailSender emailSender, IConfigurationSettings configurationSettings,IHelperManager helperManager)
        {
            _ulearndbContext = ulearndbContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _configurationSettings = configurationSettings;
            _helperManager = helperManager;
        }
        public List<User> GettAll()
        {
            var res = _ulearndbContext.Users
                .Include(m=>m.UserRoles)
                .ThenInclude(m=>m.Role)
                .ToList();
            return res;
        }
        
        
      
      
        public void ChangeUserRole(UserModel loggedInUser, int userId, string newRole)
        {
            if (!loggedInUser.IsSuperAdmin)
            {
                throw new ServiceValidationException("You don't have permission to change user role");
            }
            var role = _ulearndbContext.Roles.FirstOrDefault(a => a.Name == newRole);
            if (role == null)
            {
                throw new ServiceValidationException("the role is not exist");
            }
            var user = _ulearndbContext.Users.FirstOrDefault(a => a.Id == userId);
            if (user == null)
            {
                throw new ServiceValidationException("user not exist");
            }
            var userroles = _ulearndbContext.UserRoles.Where(m => m.UserId == userId).ToList();
            foreach (var userrole in userroles)
            {
                _ulearndbContext.Remove(userrole);
                _ulearndbContext.SaveChanges();
            }
            _ulearndbContext.UserRoles.Add(new() { UserId = userId, RoleId = role.Id, CreatedDate = new DateTime(), UpdatedDate = new DateTime() });
            _ulearndbContext.SaveChanges();
        }

        /*  public UserResponse GetUsers(int page = 1,
                                      int pageSize = 10,
                                      string sortColumn = "",
                                      string sortDirection = "ascending",
                                      string searchText = "")
          {
              var queryRes = _ulearndbContext.Users
                                             .Where(a => string.IsNullOrWhiteSpace(searchText)
                                                         || (a.FirstName.Contains(searchText)
                                                         || (a.LastName.Contains(searchText)
                                                         || (a.Phone.Contains(searchText)
                                                         || (a.Image.Contains(searchText)
                                                         || a.Email.Contains(sortColumn))))));

              if (!string.IsNullOrWhiteSpace(sortColumn)
                  && sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
              {
                  queryRes = queryRes.OrderBy(sortColumn);
              }
              else if (!string.IsNullOrWhiteSpace(sortColumn)
                  && sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
              {
                  queryRes = queryRes.OrderByDescending(sortColumn);
              }

              var res = queryRes.GetPaged(page, pageSize);

              var userIds = res.Data
                               .Select(a => a.Id)
                               .Distinct()
                               .ToList();

              var users = _ulearndbContext.Users
                                          .Where(a => userIds.Contains(a.Id))
                                          .ToDictionary(a => a.Id, x => _mapper.Map<UserResult>(x));

              var data = new UserResponse
              {
                  Users = _mapper.Map<PagedResult<UserModel>>(res),
                  User = users
              };

              data.Course.Sortable.Add("Title", "Title");
              data.Course.Sortable.Add("CreatedDate", "Created Date");

              return data;
          }*/
        #region public 

        public LoginUserResponse Login(LoginModelView userReg)
        {
			User user = _ulearndbContext.Users
                .Include(m=>m.UserRoles)
                                   .FirstOrDefault(a => a.Email
                                                           .Equals(userReg.Email,
                                                                   StringComparison.InvariantCultureIgnoreCase));
            
			if (user == null || !VerifyHashPassword(userReg.Password, user.Password))
            {
                throw new ServiceValidationException(300, "Invalid user name or password received");
            }

			LoginUserResponse res = _mapper.Map<LoginUserResponse>(user);
            res.Roles = new();
            var userRoles=user.UserRoles.ToList();
            var _roles = _ulearndbContext.Roles.ToList();
            foreach(var role in _roles)
            {
                if(userRoles.Any(m=>m.RoleId == role.Id))
                    res.Roles.Add(role.Name);
            }
            res.Token = $"Bearer {GenerateJWTToken(user, res.Roles)}";
            res.Image=_helperManager.GetBase64FromImagePath(user.Image);
            return res;
        }

        public LoginUserResponse SignUp(UserRegistrationModel userReg)
        {
            if (_ulearndbContext.Users
                              .Any(a => a.Email.Equals(userReg.Email,
                                        StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ServiceValidationException("User already exist");
            }

            var hashedPassword = HashPassword(userReg.Password);
			string imgurl = _helperManager.SaveImage(userReg.Base64Image, "wwwroot\\images");

			var user = _ulearndbContext.Users.Add(new User
            {
                FirstName = userReg.FirstName,
                LastName = userReg.LastName,
                Email = userReg.Email.ToLower(),
                Password = hashedPassword,
                Image = imgurl,
                Phone = userReg.Phone??"",
                ConfirmationLink = Guid.NewGuid().ToString().Replace("-", "").ToString()
            }).Entity;

            

        //    if (userReg.Role.Length > 2)
            {
            //    var role = _ulearndbContext.Roles.FirstOrDefault(m => m.Name == userReg.Role);
              //  if(role!=null)
                {
                 //   int roleid=role.Id;
                    int uid = user.Id;
                    _ulearndbContext.UserRoles.Add(new()
                    {
                        CreatedDate = DateTime.Now,
                       // RoleId = roleid,
                        UpdatedDate = DateTime.Now,
                        UserId = uid,
                        IsArchived = false,

                    });
                    _ulearndbContext.SaveChanges();
                }

            }

		/*	var builder = new EmailBuilder(ActionInvocationTypeEnum.EmailConfirmation,
                                new Dictionary<string, string>
                                {
                                    { "AssigneeName", $"{userReg.FirstName} {userReg.LastName}" },
                                    { "Link", $"{user.ConfirmationLink}" }
                                }, "https://localhost:44309");

            var message = new Message(new string[] { user.Email }, builder.GetTitle(), builder.GetBody());
            _emailSender.SendEmail(message);*/

            var res = _mapper.Map<LoginUserResponse>(user);
            res.Token = $"Bearer {GenerateJWTToken(user,new())}";

            return res;
        }

        public UserModel Confirmation(string ConfirmationLink)
        {
            var user = _ulearndbContext.Users
                           .FirstOrDefault(a => a.ConfirmationLink
                                                    .Equals(ConfirmationLink)
                                                && !a.IsEmailConfirmed)
                       ?? throw new ServiceValidationException("Invalid or expired confirmation link received");

            user.IsEmailConfirmed = true;
            user.ConfirmationLink = string.Empty;
            _ulearndbContext.SaveChanges();
            return _mapper.Map<UserModel>(user);
        }

        public UserModel UpdateProfile(UserModel currentUser, UserModel request)
        {
            var user = _ulearndbContext.Users
                                    .FirstOrDefault(a => a.Id == currentUser.Id)
                                    ?? throw new ServiceValidationException("User not found");

            var url = "";

            if (!string.IsNullOrWhiteSpace(request.ImageString))
            {
                url = Helper.SaveImage(request.ImageString, "profileimages");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Phone = request.Phone;

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44309/";
                user.Image = @$"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
            }

            _ulearndbContext.SaveChanges();
            return _mapper.Map<UserModel>(user);
        }

        public void DeleteUser(UserModel currentUser, int id)
        {
            if (currentUser.Id == id)
            {
                throw new ServiceValidationException("You have no access to delete your self");
            }

            /*  var user = _ulearndbContext.Users
                                      .FirstOrDefault(a => a.Id == id)
                                      ?? throw new ServiceValidationException("User not found");
              // for soft delete
              user.IsArchived = true;
              _ulearndbContext.SaveChanges();*/
            var User = _ulearndbContext.Users.Find(id);
            if (User == null)
            {
                throw new ArgumentException("Course not found", nameof(id));
            }
            _ulearndbContext.Users.Remove(User);
            _ulearndbContext.SaveChanges();
        }
        
        public void ChangePassword(UserModel loggedInUser, ChangePasswordModelView changePasswordModel)
        {
            try
            {
                var user = _ulearndbContext.Users
								   .FirstOrDefault(a => a.Id == loggedInUser.Id)
								   ?? throw new ServiceValidationException("User not found");
				var hashedPassword = HashPassword(changePasswordModel.NewPassword);
				var hashedoldPassword = HashPassword(changePasswordModel.OldPassword);
                if(!VerifyHashPassword(changePasswordModel.OldPassword, user.Password))
                {
					throw new ServiceValidationException("old password is not correct");
				}
                user.Password = hashedPassword;
                
                _ulearndbContext.SaveChanges();

			}
            catch (Exception ex)
            {
                throw new ServiceValidationException(ex.Message, ex);
            }
			
		}
		#endregion public 

		#region private 

		private static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }

        private static bool VerifyHashPassword(string password, string HashedPasword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPasword);
        }

        private string GenerateJWTToken(User user,List<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationSettings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            _ = roles.ToString();
            string rs = string.Join(",", roles.Select(s => "\"" + s + "\"")) + "]";

			var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("Roles",string.Join(",", roles)),
                new Claim("DateOfJoining", user.CreatedDate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                        _configurationSettings.Issuer,
                        _configurationSettings.Issuer,
                        claims,
                        expires: DateTime.Now.AddDays(20),
                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion private  
    }
}
