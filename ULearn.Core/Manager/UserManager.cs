using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ULearn.Common.Extensions;
using ULearn.Common.Helper;
using ULearn.Core.Manager.Interfaces;
using ULearn.DbModel.Models;
using ULearn.DbModel.Models.DB;
using ULearn.EmailService;
using ULearn.EmailService.Implementation;
using ULearn.infrastructure;
using ULearn.ModelView;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Response;
using ULearn.ModelView.Static;

namespace ULearn.Core.Manager
{
    public class UserManager : IUserManager
    {
        private ulearndbContext _ulearndbContext;
        private IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IConfigurationSettings _configurationSettings;


        public UserManager(ulearndbContext ulearndbContext, IMapper mapper, IEmailSender emailSender, IConfigurationSettings configurationSettings)
        {
            _ulearndbContext = ulearndbContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _configurationSettings = configurationSettings;
        }

        #region public 

        public LoginUserResponse Login(LoginModelView userReg)
        {
            var user = _ulearndbContext.Users
                                   .FirstOrDefault(a => a.Email
                                                           .Equals(userReg.Email,
                                                                   StringComparison.InvariantCultureIgnoreCase));

            if (user == null || !VerifyHashPassword(userReg.Password, user.Password))
            {
                throw new ServiceValidationException(300, "Invalid user name or password received");
            }

            var res = _mapper.Map<LoginUserResponse>(user);
            res.Token = $"Bearer {GenerateJWTToken(user)}";
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

            var user = _ulearndbContext.Users.Add(new User
            {
                FirstName = userReg.FirstName,
                LastName = userReg.LastName,
                Email = userReg.Email.ToLower(),
                Password = hashedPassword,
                Image = string.Empty,
                Phone = string.Empty,
                ConfirmationLink = Guid.NewGuid().ToString().Replace("-", "").ToString()
            }).Entity;

            _ulearndbContext.SaveChanges();


            var builder = new EmailBuilder(ActionInvocationTypeEnum.EmailConfirmation,
                                new Dictionary<string, string>
                                {
                                    { "AssigneeName", $"{userReg.FirstName} {userReg.LastName}" },
                                    { "Link", $"{user.ConfirmationLink}" }
                                }, "https://localhost:44309");

            var message = new Message(new string[] { user.Email }, builder.GetTitle(), builder.GetBody());
            _emailSender.SendEmail(message);

            var res = _mapper.Map<LoginUserResponse>(user);
            res.Token = $"Bearer {GenerateJWTToken(user)}";

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

            var user = _ulearndbContext.Users
                                    .FirstOrDefault(a => a.Id == id)
                                    ?? throw new ServiceValidationException("User not found");
            // for soft delete
            user.IsArchived = true;
            _ulearndbContext.SaveChanges();
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

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationSettings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
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
