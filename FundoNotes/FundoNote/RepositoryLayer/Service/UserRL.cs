using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration _Appsettings;

        private readonly FundoContext fundoContext;

        public object UserId { get; private set; }

        public UserRL(FundoContext fundoContext, IConfiguration _Appsettings)
        {
            this.fundoContext = fundoContext;
            this._Appsettings = _Appsettings;
        }
        public UserEntity Register(UserRegestrationModel userRegestrationModel)
        {
            try
            {
                UserEntity user = new UserEntity();
                user.FirstName = userRegestrationModel.FirstName;
                user.LastName = userRegestrationModel.LastName;
                user.Email = userRegestrationModel.Email;
                user.Password = userRegestrationModel.Password;
                fundoContext.UserTable.Add(user);
                int res = fundoContext.SaveChanges();
                if (res > 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Login(UserLoginModel userLogin)
        {
            try
            {
                var LoginDetails = fundoContext.UserTable.Where(x => x.Email == userLogin.Email && x.Password == userLogin.Password).FirstOrDefault();
                if (LoginDetails != null)
                {
                    var token = GenerateSecurityToken(LoginDetails.Email, LoginDetails.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateSecurityToken(string email, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this._Appsettings[("JWT:key")]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string FogotPassword(string Email)
        {
            try
            {
                var EmailCheck = fundoContext.UserTable.FirstOrDefault(x => x.Email == Email);
                if(EmailCheck != null)
                {
                 
                    string token = GenerateSecurityToken(EmailCheck.Email, EmailCheck.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return "Mail sent";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetLink(string Email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var EmailCheck = fundoContext.UserTable.FirstOrDefault(x => x.Email == Email);
                    EmailCheck.Password = password;

                    fundoContext.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
