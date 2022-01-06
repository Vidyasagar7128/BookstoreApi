using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IAddressManager
    {
        Task<int> AddAddress(AddressModel address);
        Task<int> UpdateAddress(AddressModel address);
        Task<int> DeleteAddress(int userId, int addressId);
        Task<IEnumerable<AddressModel>> ListAddress(int userId);
    }
}