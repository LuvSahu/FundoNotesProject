using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Register(UserRegestrationModel userRegestrationModel);
        public string Login(UserLoginModel userLoginModel);

        public string FogotPassword(string Email);


    }
}
