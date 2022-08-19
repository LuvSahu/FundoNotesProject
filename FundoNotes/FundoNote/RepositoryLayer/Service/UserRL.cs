using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundoContext fundoContext;

        public UserRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }   

        public UserEntity Register (UserRegestrationModel userRegestrationModel)
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
                if(res>0)
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

        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                var LoginDetails = fundoContext.UserTable.Where(x => x.Email == userLoginModel.Email && x.Password == userLoginModel.Password).FirstOrDefault();
                if(LoginDetails!=null)
                {
                    return "Login is Sucessfull";
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
    }
}
