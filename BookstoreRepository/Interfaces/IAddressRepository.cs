using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IAddressRepository
    {
        Task<int> AddNew(AddressModel address);
    }
}