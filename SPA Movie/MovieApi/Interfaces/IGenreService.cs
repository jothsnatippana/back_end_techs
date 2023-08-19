using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public interface IGenreService
    {
        Task<ResponseGenre> GetGenreByIdAsync(int id);
        Task<IEnumerable<ResponseGenre>> GetAllGenresAsync();
        Task<int> AddGenreAsync(RequestGenre reqGenre);
        Task DeleteGenreAsync(int id);
        Task UpdateGenreAsync(int id, RequestGenre reqGenre);
    }
}