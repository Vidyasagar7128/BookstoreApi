using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IAuth
    {
        Task<int> ValidateJwtToken(StringValues token);
    }
}