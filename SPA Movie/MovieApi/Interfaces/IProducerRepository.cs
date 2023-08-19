using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public interface IProducerRepository
    {
        Task<IEnumerable<Producer>> GetAllProducersAsync();
        Task<Producer> GetProducerByIdAsync(int id);
        Task<int> AddProducerAsync(RequestProducer reqProducer);
        Task DeleteProducerAsync(int id);
        Task UpdateProducerAsync(int id, RequestProducer reqProducer);
    }
}