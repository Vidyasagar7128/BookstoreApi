using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUser(CreateUserModel createUser);
    }
}