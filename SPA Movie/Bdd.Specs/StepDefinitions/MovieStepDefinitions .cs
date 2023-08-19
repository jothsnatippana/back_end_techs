using Bdd.Specs.MockResources;
using MovieApi;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;


namespace Bdd.Specs.StepDefinitions
{
    [Scope(Feature = "movie")]
    [Binding]
    public sealed class MovieStepDefinitions:BaseSteps
    {
        public MovieStepDefinitions(CustomWebApplicationFactory factory)
    : base(factory.WithWebHostBuilder(builder =>
    {
        builder.ConfigureServices(services =>
        {
            // Mock Repo
            services.AddScoped(_ => MovieMock.MovieRepoMock.Object);
        });
    }))
        { }

        [BeforeScenario]
        public static void Mocks()
        {
            MovieMock.MockGetAllMoviesAsync();
            MovieMock.MockGetMovieByIdAsync();
            MovieMock.MockAddMovieAsync();
            MovieMock.MockDeleteMovieAsync();
            MovieMock.MockUpdateMovieAsync();
        }
    }
}