using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public interface IActorService
    {

        Task<ResponseActor> GetActorByIdAsync(int id);
        Task<IEnumerable<ResponseActor>> GetAllActorsAsync();
        Task<int> AddActorAsync(RequestActor reqActor);
        Task DeleteActorAsync(int id);
        Task UpdateActorAsync(int id, RequestActor reqActor);
    }
}