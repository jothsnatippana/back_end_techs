using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using MovieApi.Repositories;
using MovieApi.Domains.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public class ActorService:IActorService
    {
        private readonly IActorRepository _actorRepository;
        readonly Validations v;
      
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
            v=new Validations(); 
        }

        public async Task<ResponseActor> GetActorByIdAsync(int id)
        {
            // making a call to repository and fetcing data  
            var actor = await _actorRepository.GetActorByIdAsync(id);
            // typecasting from actor to responseactor
            if(actor==null)
            {
                return null;
            }
            var newactor = new ResponseActor(id,actor.Name,actor.Bio,actor.DOB, actor.Gender); 
            return newactor; 
        }

        public  async Task<IEnumerable<ResponseActor>> GetAllActorsAsync()
        {
            // calling repo and fetching data
            var ActorData = await _actorRepository.GetAllActorsAsync();
            // typecasting from actor to responseactor
            var allActors = ActorData.Select(o => new ResponseActor(o.Id,o.Name, o.Bio, o.DOB, o.Gender)).ToList();
            return allActors;
        }

        public async Task<int> AddActorAsync(RequestActor reqActor)
        {
                return await _actorRepository.AddActorAsync(reqActor);
        }

        public async Task DeleteActorAsync(int id) 
        {
            //deleting actor
            await _actorRepository.DeleteActorAsync(id);
        }

       
        public async Task UpdateActorAsync(int id, RequestActor reqActor)
        {
            await _actorRepository.UpdateActorAsync(id,reqActor);
        }
    }
}
