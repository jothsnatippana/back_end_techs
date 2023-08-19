using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task<int> AddGenreAsync(RequestGenre reqGenre);
        Task DeleteGenreAsync(int id);
        Task UpdateGenreAsync(int id, RequestGenre reqGenre);
    }
}