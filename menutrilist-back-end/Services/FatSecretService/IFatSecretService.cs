using System.Threading.Tasks;
using Menutrilist.Contracts.V1.Responses;
using Menutrilist.Domain.FatSecret;

namespace Menutrilist.Services
{
    public interface IFatSecretService
    {
        Task<dynamic> GetFood(int id);
        Task<dynamic> SearchFood(string keyword, int maxResult, int pageNumber);
    }
}