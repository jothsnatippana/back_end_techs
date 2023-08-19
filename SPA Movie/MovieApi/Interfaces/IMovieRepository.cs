using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie> >GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<int> AddMovieAsync(RequestMovie reqMovie);
        Task DeleteMovieAsync(int id);
        Task<int> UpdateMovieAsync(int id, RequestMovie reqMovie);
    }
}