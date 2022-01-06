using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository _addressRepository;
        public AddressManager(IAddressRepository addressRepository)
        {
            this._addressRepository = addressRepository;
        }

        public async Task<int> AddAddress(AddressModel address)
        {
            try
            {
                return await this._addressRepository.AddNew(address);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
