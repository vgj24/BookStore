using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
   public class UserRL :IUserRL
   {
        private SqlConnection sqlConnection;
        public IConfiguration configuration;
        public UserRL(IConfiguration configuration )
        {
            this.configuration = configuration;
        }
    }
}
