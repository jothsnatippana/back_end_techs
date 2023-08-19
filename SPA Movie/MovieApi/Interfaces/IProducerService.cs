using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public interface IProducerService
    {
        Task<ResponseProducer> GetProducerByIdAsync(int id);
        Task<IEnumerable<ResponseProducer>> GetAllProducersAsync();
        Task<int> AddProducerAsync(RequestProducer reqProducer);
        Task DeleteProducerAsync(int id);
        Task UpdateProducerAsync(int id, RequestProducer reqProducer);
    }
}