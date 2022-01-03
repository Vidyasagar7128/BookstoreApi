using BookstoreModel;
using BookstoreRepository.Interfaces;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
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

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="password">string</param>
        /// <returns>logged In or failed</returns>
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
                            await reader1.CloseAsync();
                            await con.CloseAsync();
                            return "Login Successfully!";
                        }
                        else
                        {
                            await reader1.CloseAsync();
                            await con.CloseAsync();
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
                            await readerPass.CloseAsync();
                            await con.CloseAsync();
                            return "Invalid Email.";
                        }
                        else
                        {
                            await readerPass.CloseAsync();
                            await con.CloseAsync();
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

        /// <summary>
        /// Jwt Token
        /// </summary>
        /// <param name="email">passing email</param>
        /// <returns>Token</returns>
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

        public async Task<int> ForgetPassword(ResetPasswordModel passwordModel)
        {
            try
            {
                var encPassword = EncryptPassword(passwordModel.OldPassword);
                var encNewPassword = EncryptPassword(passwordModel.Password);
                if (passwordModel.Password.Equals(passwordModel.ConfirmPassword))
                {
                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                    {
                        SqlCommand sql = new SqlCommand("update Users set Password='" + encNewPassword + "' where Password = '" + encPassword + "'", con);
                        await con.OpenAsync();
                        int status = await sql.ExecuteNonQueryAsync();
                        await con.CloseAsync();
                        return status;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> ResetPassword(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("select Email from Users where Email ='"+ email +"'", con);
                    await con.OpenAsync();
                    SqlDataReader reader = await sql.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var Email = reader["Email"];
                    }
                    else
                    {
                        await reader.CloseAsync();
                        return "Email doesn't exist!";
                    }
                    await reader.CloseAsync();
                    await con.CloseAsync();
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(this.Configuration.GetSection("EmailId").Value);
                    mail.To.Add(email);
                    mail.Subject = "[Bookstore] Password Reset Link";
                    this.SendMSMailQueue();
                    mail.Body = this.GetMSMailQueue();
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.Credentials = new System.Net.NetworkCredential(this.Configuration.GetSection("EmailId").Value, this.Configuration.GetSection("EPassword").Value);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mail);
                    return "Email sent!";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Email sent by MSMQ
        /// </summary>
        private void SendMSMailQueue()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\Bookstore"))
            {
                messageQueue = new MessageQueue(@".\Private$\Bookstore");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\Bookstore");
            }

            messageQueue.Label = "MsMq";
            //string body = "Do you want to change your Password!";
            messageQueue.Send("Do you want to change your Password!");
        }

        /// <summary>
        /// get mail in queue
        /// </summary>
        /// <returns>received or not</returns>
        private string GetMSMailQueue()
        {
            var queue = new MessageQueue(@".\Private$\Bookstore");
            var received = queue.Receive();
            return received.ToString();
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
