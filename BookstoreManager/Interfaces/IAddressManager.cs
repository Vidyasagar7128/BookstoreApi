using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IAddressManager
    {
        Task<int> AddAddress(AddressModel address);
        Task<int> UpdateAddress(AddressModel address);
    }
}