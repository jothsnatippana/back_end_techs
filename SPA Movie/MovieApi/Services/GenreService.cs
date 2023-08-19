using Microsoft.Extensions.Options;
using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using MovieApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _GenreRepository;
        readonly Validations v;
      
        public GenreService(IGenreRepository  genreRepository)
        {
            _GenreRepository = genreRepository;
            v = new Validations();
        }

        public async Task<IEnumerable<ResponseGenre>> GetAllGenresAsync()
        {
            var GenreData = await _GenreRepository.GetAllGenresAsync(); //getting all actors data from repo
            var result = GenreData.Select(o => new ResponseGenre(o.Id,o.Name));//typecasting all genres from genre to responsegenre 
            return result;
        }

        public async Task<ResponseGenre> GetGenreByIdAsync(int id)
        {
            var Genres =await _GenreRepository.GetGenreByIdAsync(id); //fetching data from repo
            if (Genres == null)
                return null;
            ResponseGenre genre= new(Genres.Id,Genres.Name); //typecasting from Genre to Responsegenre
            return genre;
        }

        public async Task<int> AddGenreAsync(RequestGenre reqGenre)
        {
            return await _GenreRepository.AddGenreAsync(reqGenre);
        }
        public async Task DeleteGenreAsync(int id)
        {
           await  _GenreRepository.DeleteGenreAsync(id);
        }
        public async Task UpdateGenreAsync(int id, RequestGenre reqGenre)
        {
            await _GenreRepository.UpdateGenreAsync(id, reqGenre);
        }
    }
}
