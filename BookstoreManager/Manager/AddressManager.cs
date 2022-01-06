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

        /// <summary>
        /// Adding address to user
        /// </summary>
        /// <param name="address">passing AddressModel</param>
        /// <returns>added or not</returns>
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

        /// <summary>
        /// Updating address to user
        /// </summary>
        /// <param name="address">passing AddressModel</param>
        /// <returns>Updated or not</returns>
        public async Task<int> UpdateAddress(AddressModel address)
        {
            try
            {
                return await this._addressRepository.Update(address);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// delete address
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="addressId">passing addressId</param>
        /// <returns>deleted or not</returns>
        public async Task<int> DeleteAddress(int userId, int addressId)
        {
            try
            {
                return await this._addressRepository.Delete(userId, addressId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// List of address
        /// </summary>
        /// <returns>List of address</returns>
        public async Task<IEnumerable<AddressModel>> ListAddress(int userId)
        {
            try
            {
                return await this._addressRepository.ShowAddress(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
