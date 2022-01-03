using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUser(CreateUserModel createUser);
        Task<string> LoginUser(string email, string password);
        string JwtToken(string email);
        Task<int> ForgetPassword(ResetPasswordModel passwordModel);
        Task<string> ResetPassword(string email);
    }
}