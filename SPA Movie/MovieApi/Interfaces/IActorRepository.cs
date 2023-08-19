using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetAllActorsAsync();
        Task<Actor> GetActorByIdAsync(int id);
        Task<int> AddActorAsync(RequestActor reqactor);
        Task DeleteActorAsync(int id);
        Task UpdateActorAsync(int id,RequestActor reqactor);
       
    }
}