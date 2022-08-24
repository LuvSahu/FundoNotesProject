using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Register(UserRegestrationModel userRegestrationModel);
        public string Login(UserLoginModel userLoginModel);
        public string FogotPassword(string Email);

        public bool ResetLink(string Email, string password, string confirmPassword);




    }
}
