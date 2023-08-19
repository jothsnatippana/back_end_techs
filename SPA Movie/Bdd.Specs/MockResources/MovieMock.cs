using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Moq;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Repositories;
using System.Numerics;

namespace Bdd.Specs.MockResources
{
    public class MovieMock
    {
        //actors
        static Actor actor1 = new Actor(1, "Yash", "YashBio", new DateTime(2000, 1, 1), "Male");
        static Actor actor2 = new Actor(2, "Shrinidhi", "ShrinidhiBio", new DateTime(2000, 1, 1), "Female");
        static Actor actor3 = new Actor(3, "Ramacharan", "RamacharanBio", new DateTime(2000, 1, 1), "Male");
        static Actor actor4 = new Actor(4, "Vinay", "VinayBio", new DateTime(2000, 1, 1), "Male");
      

        //genres
        static Genre genre1 = new Genre(1, "Comedy");
        static Genre genre2 = new Genre(2, "Thriller");
        static Genre genre3 = new Genre(3, "Adventure");
        static Genre genre4 = new Genre(4, "Action");
       
        //producers
        static Producer producer1 = new Producer(1, "Priyanka", "PriyankaBio", new DateTime(2000, 2, 13), "Female");
        static Producer producer2 = new Producer(2, "Shobu", "ShobuBio", new DateTime(2000, 5, 14), "Male");

        public static readonly Mock<IMovieRepository> MovieRepoMock = new Mock<IMovieRepository>();
        static Movie movie1 = new Movie(1, "KGFChap2", 2022, "KGFPlot", new List<Actor> { actor1, actor2 }, new List<Genre> { genre1, genre2 }, producer1, "KGFChap2CoverPage");
        static Movie movie2 = new Movie(2, "Kantara", 2022, "KantaraPlot", new List<Actor> { actor3, actor4 }, new List<Genre> { genre3, genre4 }, producer2, "KantaraCoverPage");
       

       
        private static readonly IEnumerable<Movie> ListOfMovies = new List<Movie>
        {
            movie1,
            movie2
        };

        public static void MockGetAllMoviesAsync()
        {
            MovieRepoMock.Setup(x => x.GetAllMoviesAsync()).ReturnsAsync(ListOfMovies);
        }

        public static void MockGetMovieByIdAsync()
        {
            MovieRepoMock.Setup(x => x.GetMovieByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => { return ListOfMovies.FirstOrDefault(Movie => Movie.Id == id); });
        }

        public static void MockAddMovieAsync()
        {
            MovieRepoMock.Setup(x => x.AddMovieAsync(It.IsAny<RequestMovie>())).ReturnsAsync((RequestMovie moviedata)  =>
            {
                foreach(var actorid in moviedata.ActorIds)
                {
                    if(actorid<=0 || actorid>4)
                           return -1;
                }
                foreach(var genreid in moviedata.GenreIds)
                {
                    if (genreid <= 0 || genreid > 4)
                        return -1;
                }
                if (moviedata.ProducedBy > 2 || moviedata.ProducedBy <= 0)
                    return -2;
                return ListOfMovies.Count() - 1;
            });
        }

        public static void MockDeleteMovieAsync()
        {
            MovieRepoMock.Setup(x => x.DeleteMovieAsync(It.IsAny<int>())).Returns((int id) =>
            {
                return Task.CompletedTask;
            });
        }

        public static void MockUpdateMovieAsync()
        {
            MovieRepoMock.Setup(x => x.UpdateMovieAsync(It.IsAny<int>(), It.IsAny<RequestMovie>())).ReturnsAsync((int id, RequestMovie moviedata) =>
            {
                foreach (var actorid in moviedata.ActorIds)
                {
                    if (actorid <= 0 || actorid > 4)
                        return -1;
                }
                foreach (var genreid in moviedata.GenreIds)
                {
                    if (genreid <= 0 || genreid > 4)
                        return -1;
                }
                if (moviedata.ProducedBy > 2 || moviedata.ProducedBy <= 0)
                    return -2;
                return 1;
            });
        }
    }
}
