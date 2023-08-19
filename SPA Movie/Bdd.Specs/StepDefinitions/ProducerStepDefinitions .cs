using Bdd.Specs.MockResources;
using MovieApi;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;


namespace Bdd.Specs.StepDefinitions
{
    [Scope(Feature = "producer")]
    [Binding]
    public sealed class ProducerStepDefinitions:BaseSteps
    {
        public ProducerStepDefinitions(CustomWebApplicationFactory factory)
    : base(factory.WithWebHostBuilder(builder =>
    {
        builder.ConfigureServices(services =>
        {
            // Mock Repo
            services.AddScoped(_ => ProducerMock.ProducerRepoMock.Object);
        });
    }))
        { }

        [BeforeScenario]
        public static void Mocks()
        {
            ProducerMock.MockGetAllProducersAsync();
            ProducerMock.MockGetProducerByIdAsync();
            ProducerMock.MockAddProducerAsync();
            ProducerMock.MockDeleteProducerAsync();
            ProducerMock.MockUpdateProducerAsync();
        }
    }
}