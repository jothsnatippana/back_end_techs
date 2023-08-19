using Bdd.Specs.MockResources;
using MovieApi;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Bdd.Specs.StepDefinitions
{
    [Scope(Feature = "Actor")]
    [Binding]
    public sealed class ActorStepDefinitions:BaseSteps
    {
        public ActorStepDefinitions(CustomWebApplicationFactory factory)
           : base(factory.WithWebHostBuilder(builder =>
           {
               builder.ConfigureServices(services =>
               {
                   // Mock Repo
                   services.AddScoped(_=> ActorMock.ActorRepoMock.Object);
               });
           }))
        { }

        [BeforeScenario]
        public static void Mocks()
        {
            ActorMock.MockGetAllActorsAsync();
            ActorMock.MockGetActorByIdAsync();
            ActorMock.MockAddActorAsync();
            ActorMock.MockDeleteActorAsync();
            ActorMock.MockUpdateActorAsync();
        }
    }
}