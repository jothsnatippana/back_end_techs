using Moq;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Repositories;

namespace Bdd.Specs.MockResources
{
    public class ActorMock
    {
        public static readonly Mock<IActorRepository> ActorRepoMock = new Mock<IActorRepository>();

        private static readonly IEnumerable<Actor> ListOfActors = new List<Actor>
        {
            new Actor(1,"Anushka","AnushkaBio",new DateTime(2001, 12, 2),"female"),
            new Actor(2,"Prabhas","PrabhasBio",new  DateTime(2000, 08, 4),"male")
        };

        public static void MockGetAllActorsAsync()
        {
            ActorRepoMock.Setup(x => x.GetAllActorsAsync()).ReturnsAsync(ListOfActors);
        }

        public static void MockGetActorByIdAsync()
        {
            ActorRepoMock.Setup(x => x.GetActorByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => 
            {
                var result= ListOfActors.FirstOrDefault(actor => actor.Id == id);
                return result;
            });
        }

        public static void MockAddActorAsync()
        {
            ActorRepoMock.Setup(x => x.AddActorAsync(It.IsAny<RequestActor>())).ReturnsAsync((RequestActor req) =>
            {
                return ListOfActors.Count()-1;
            });
        }

        public static void MockDeleteActorAsync()
        {
            ActorRepoMock.Setup(x => x.DeleteActorAsync(It.IsAny<int>())).Returns((int id) =>
            {

                return Task.CompletedTask;
            });
        }

        public static void MockUpdateActorAsync()
        {
            ActorRepoMock.Setup(x => x.UpdateActorAsync(It.IsAny<int>(), It.IsAny<RequestActor>())).Returns((int id,RequestActor actor)=>
            {
                
                return Task.CompletedTask; 
            });
        }
    }
}
