using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public interface IMovieService
    {
        Task<ResponseMovie> GetMovieByIdAsync(int id);
        Task<IEnumerable<ResponseMovie>> GetAllMoviesAsync();
        Task<int> AddMovieAsync(RequestMovie reqMovie);
        Task DeleteMovieAsync(int id);
        Task<int> UpdateMovieAsync(int id, RequestMovie reqMovie);
    }
}