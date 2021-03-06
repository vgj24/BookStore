using CommonLayer;
using CommonLayer.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLyer.Interface
{
    public interface IUserRL
    {
        public bool Register(RegisterUser register);
        public string Login(string EmailId, string password);
        public string ForgotPassword(string EmailId);
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
