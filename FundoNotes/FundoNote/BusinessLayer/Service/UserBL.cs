using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return userRL.Login(userLoginModel);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserEntity Register(UserRegestrationModel userRegestrationModel)
        {
            try
            {
                return userRL.Register(userRegestrationModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
