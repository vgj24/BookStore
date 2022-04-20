using BusinessLyer.Interface;
using CommonLayer;
using CommonLayer.Models.User;
using RepoLyer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLyer.Service
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public bool Register(RegisterUser register)
        {
            try
            {
                return this.userRL.Register(register);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Login(string EmailId, string password)
        {
            try
            {
                return this.userRL.Login(EmailId, password);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgotPassword(string EmailId)
        {
            try
            {
                return this.userRL.ForgotPassword(EmailId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email,newPassword,confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
