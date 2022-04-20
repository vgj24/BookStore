using BusinessLyer.Interface;
using CommonLayer.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult UserRegestration(RegisterUser register)
        {
            try
            {
                if (this.userBL.Register(register))
                {
                    return this.Ok(new { Sucess = true, message = "Registration Successful", Response = register });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registraton Fail" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }

        [HttpPost("Login/{Emailid}/{Password}")]
        public IActionResult UserLogin(string Emailid,string Password)
        {
            try
            {
                var loginData = this.userBL.Login(Emailid, Password);
                if(loginData != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful", Response = loginData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Invalid user please enter valid Email and Password" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message });
            }
        }
        [HttpPost("ForgotPassword/{Email}")]

        public IActionResult ForgotPassword(String Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest("Email should not be null or empty");
            }
            try
            {
                var forgotPwd = this.userBL.ForgotPassword(Email);               
                if (forgotPwd !=null)
                {
                    return Ok(new { Success = true, message = "Password reset link sent on mail Successfully" });
                }
                else
                {
                    return Ok(new { Success = false, message = "Password reset link nort sent " });
                }

            }
            catch (Exception e)
            {

                return this.BadRequest(new { Success = false, message = e.Message, msg = e.InnerException });
            }
        }
    
    [Authorize]
    [HttpPut("ResetPassword")]
    public IActionResult ResetPassword(string newPassword, string confirmPassword)
    {
        try
        {
            var email = User.Claims.FirstOrDefault(e => e.Type == "Email").Value.ToString();
            if (this.userBL.ResetPassword(email, newPassword, confirmPassword))
            {
                return this.Ok(new { Success = true, message = " Password Changed Sucessfully " });
            }
            else
            {
                return this.BadRequest(new { Success = false, message = " Password Change Failed ! Try Again " });
            }
        }
        catch (Exception ex)
        {
            return this.BadRequest(new { Success = false, message = ex.Message });
        }
    }}
}
