using Moq;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdd.Specs.MockResources
{
    public class GenreMock
    {
        public static readonly Mock<IGenreRepository> GenreRepoMock = new Mock<IGenreRepository>();

        private static readonly IEnumerable<Genre> ListOfGenres = new List<Genre>
        {
            new Genre(1,"Crime"),
            new Genre(2,"Action")
        };

        public static void MockGetAllGenresAsync()
        {
            GenreRepoMock.Setup(x => x.GetAllGenresAsync()).ReturnsAsync(ListOfGenres);
        }

        public static void MockGetGenreByIdAsync()
        {
            GenreRepoMock.Setup(x => x.GetGenreByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => { return ListOfGenres.FirstOrDefault(Genre => Genre.Id == id); });
        }

        public static void MockAddGenreAsync()
        {
            GenreRepoMock.Setup(x => x.AddGenreAsync(It.IsAny<RequestGenre>())).ReturnsAsync((RequestGenre Genre) =>
            {
                return ListOfGenres.Count() - 1;
            });
        }

        public static void MockDeleteGenreAsync()
        {
            GenreRepoMock.Setup(x => x.DeleteGenreAsync(It.IsAny<int>())).Returns((int id) =>
            {
                return Task.CompletedTask;
            });
        }

        public static void MockUpdateGenreAsync()
        {
            GenreRepoMock.Setup(x => x.UpdateGenreAsync(It.IsAny<int>(), It.IsAny<RequestGenre>())).Returns((int id, RequestGenre Genre) =>
            {
                return Task.CompletedTask;
            });
        }
    }
}
