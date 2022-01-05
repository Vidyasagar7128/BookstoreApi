using BookstoreRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreRepository.Repository
{
    public class Auth : IAuth
    {
        public Auth(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        public async Task<int> ValidateJwtToken(StringValues token)
        {
            try
            {
                var tk = token.ToString().Split(" ");
                var stk = tk[1];
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken tokens = handler.ReadJwtToken(tk[1]);
                var newToken = tokens as JwtSecurityToken;
                var paylods = newToken.Payload;
                var email = paylods.Claims.First(c => c.Type == "unique_name").Value;
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("select UserId from Users where Email='" + email + "'", con);
                    await con.OpenAsync();
                    SqlDataReader reader = await sql.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        int id = (int) reader["UserId"];
                        await con.CloseAsync();
                        return id;
                    }
                    else
                    {
                        await con.CloseAsync();
                        throw new Exception("Something went wrong");
                    }
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
