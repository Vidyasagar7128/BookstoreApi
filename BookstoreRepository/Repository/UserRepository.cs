using BookstoreModel;
using BookstoreRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Adding user to database
        /// </summary>
        /// <param name="createUser">CreateUserModel</param>
        /// <returns>status</returns>
        public async Task<int> AddUser(CreateUserModel createUser)
        {
            try
            {
                var encPassword = EncryptPassword(createUser.Password);
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("CreateUserSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@Name", createUser.Name);
                    sql.Parameters.AddWithValue("@Email", createUser.Email);
                    sql.Parameters.AddWithValue("@Mobile", createUser.Mobile);
                    sql.Parameters.AddWithValue("@Password", encPassword);
                    await con.OpenAsync();
                    int status = await sql.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    return status;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
        public async Task<string> LoginUser(string email, string password)
        {
            try
            {
                var encPassword = EncryptPassword(password);
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("select UserId, Name, Email, Mobile from Users where Email ='" + email + "'", con);
                    await con.OpenAsync();
                    SqlDataReader reader = await sql.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        await reader.CloseAsync();
                        SqlCommand sql1 = new SqlCommand("select UserId, Name, Email, Mobile from Users where Password ='" + encPassword + "'", con);
                        SqlDataReader reader1 = await sql1.ExecuteReaderAsync();
                        if (await reader1.ReadAsync())
                        {
                            var UserId = reader1["UserId"];
                            var Name = reader1["Name"];
                            var Email = reader1["Email"];
                            var Mobile = reader1["Mobile"];

                            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                            IDatabase database = connectionMultiplexer.GetDatabase();
                            database.StringSet(key: "name", Name.ToString());
                            database.StringSet(key: "email", Email.ToString());
                            database.StringSet(key: "mobile", Mobile.ToString());
                            await con.CloseAsync();
                            return "Login Successfully!";
                        }
                        else
                        {
                            return "Invalid Password.";
                        }
                    }
                    else
                    {
                        await reader.CloseAsync();
                        SqlCommand sqlPass = new SqlCommand("select UserId, Name, Email, Mobile from Users where Password ='" + encPassword + "'", con);
                        SqlDataReader readerPass = await sqlPass.ExecuteReaderAsync();
                        if (await readerPass.ReadAsync())
                        {
                            return "Invalid Email.";
                        }
                        else
                        {
                            return "Invalid Email & Password.";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string JwtToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                      new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        /// <summary>
        /// Password Encryption
        /// </summary>
        /// <param name="password">Password string</param>
        /// <returns>Encrypted Password</returns>
        private string EncryptPassword(string password)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("BookstoreWithaspdotnetcore"));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(result, 0, result.Length).ToString();
                }
            }
        }
    }
}
