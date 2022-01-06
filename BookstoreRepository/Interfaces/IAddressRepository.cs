using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IAddressRepository
    {
        Task<int> AddNew(AddressModel address);
        Task<int> Update(AddressModel address);
        Task<int> Delete(int userId, int addressId);
        Task<IEnumerable<AddressModel>> ShowAddress(int userId);
    }
}