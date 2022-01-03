using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<int> AddUser(CreateUserModel createUserModel)
        {
            try
            {
                return await this._userRepository.AddUser(createUserModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> Login(string email, string password)
        {
            try
            {
                return await this._userRepository.LoginUser(email, password);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> ForgetPass(ResetPasswordModel passwordModel)
        {
            try
            {
                return await this._userRepository.ForgetPassword(passwordModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string JwtToken(string email)
        {
            try
            {
                return this._userRepository.JwtToken(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// for send Email
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>email sent or not</returns>
        public async Task<string> Reset(string email)
        {
            try
            {
                return await this._userRepository.ResetPassword(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
