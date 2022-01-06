using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IAddressManager
    {
        Task<int> AddAddress(AddressModel address);
        Task<int> UpdateAddress(AddressModel address);
        Task<int> DeleteAddress(int userId, int addressId);
    }
}