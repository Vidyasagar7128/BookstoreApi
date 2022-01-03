using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IUserManager
    {
        Task<int> AddUser(CreateUserModel createUserModel);
        Task<string> Login(string email, string password);
        string JwtToken(string email);
        Task<int> ForgetPass(ResetPasswordModel passwordModel);
        Task<string> Reset(string email);
    }
}