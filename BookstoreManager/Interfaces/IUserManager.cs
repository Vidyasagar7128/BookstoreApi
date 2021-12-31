using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IUserManager
    {
        Task<int> AddUser(CreateUserModel createUserModel);
    }
}