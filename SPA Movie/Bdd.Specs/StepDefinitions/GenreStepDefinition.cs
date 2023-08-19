using MovieApi;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;
using Bdd.Specs.MockResources;

namespace Bdd.Specs.StepDefinitions
{
    [Scope(Feature = "genre")]
    [Binding]
    public sealed class GenreStepDefinitions:BaseSteps
    {
        public GenreStepDefinitions(CustomWebApplicationFactory factory)
           : base(factory.WithWebHostBuilder(builder =>
           {
               builder.ConfigureServices(services =>
               {
                   // Mock Repo
                   services.AddScoped(_ => GenreMock.GenreRepoMock.Object);
               });
           }))
        { }
        [BeforeScenario]
        public static void Mocks()
        {
            GenreMock.MockGetAllGenresAsync();
            GenreMock.MockGetGenreByIdAsync();
            GenreMock.MockAddGenreAsync();
            GenreMock.MockDeleteGenreAsync();
            GenreMock.MockUpdateGenreAsync();
        }
    }
}