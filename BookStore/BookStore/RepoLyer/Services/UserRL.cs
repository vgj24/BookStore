using CommonLayer;
using CommonLayer.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLyer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepoLyer.Services
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public bool Register(RegisterUser register)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_AddUser1", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@FullName", register.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", register.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", register.Password);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", register.PhoneNumber);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string Login(string EmailId, string password)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    RegisterUser register = new RegisterUser();

                    SqlCommand command = new SqlCommand("sp_Login", sqlConnection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", EmailId);
                    command.Parameters.AddWithValue("@Password", password);
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int UserId = 0;
                        while (reader.Read())
                        {
                            
                            register.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                            UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                            register.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                        }
                        string token = GenerateToken(EmailId,UserId);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public string GenerateToken(string Email,int UserId)
        {
            ////header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            ////payload 
            var claims = new[]
            {new Claim("Email",Email),
             new Claim("Id",UserId.ToString())};

            ////signature
            var token = new JwtSecurityToken(this.Configuration["Jwt:Issuer"],
              this.Configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public string GenerateJWTTokenForForgotPass(string Email, long Id)
        //{
        //    // header
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    // payload
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Email,Email),
        //        new Claim("Id",Id.ToString()),
        //    };

        //    // signature
        //    var token = new JwtSecurityToken(
        //        this.Configuration["Jwt:Issuer"],
        //        this.Configuration["Jwt:Issuer"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(60),
        //        signingCredentials: credentials);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        private string GenerateJWTTokenForForgotPass(string Email, long Id)
        {
            ////header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            ////payload 
            var claims = new[]
            {new Claim("Email",Email),
             new Claim("Id",Id.ToString())};

            ////signature
            var token = new JwtSecurityToken(this.Configuration["Jwt:Issuer"],
              this.Configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ForgotPassword(string email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    RegisterUser model = new RegisterUser();
                    SqlCommand command = new SqlCommand("spUserFgtPass", sqlConnection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);

                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                   
                    if (reader.HasRows) 
                    { 
                        int userId = 0;
                        while (reader.Read())
                        {
                            email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                            userId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        }
                        sqlConnection.Close();
                        var token = GenerateJWTTokenForForgotPass(email, userId);
                        new MsMqModel().Sender(token);
                        return token;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStoreDB"]);
                    SqlCommand com = new SqlCommand("sp_ResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", confirmPassword);
                    this.sqlConnection.Open();
                    int i = com.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}


