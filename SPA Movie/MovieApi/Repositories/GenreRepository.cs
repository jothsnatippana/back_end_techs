using Microsoft.Extensions.Options;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        

        public GenreRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.DefaultConnection.ToString())
        {
        }
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            const string query = @"
            SELECT Id
	              ,Name
            FROM Foundation.Genres (NOLOCK)";
            return await GetAllAsync(query);
        }

        public Task<Genre> GetGenreByIdAsync(int id)
        {
            const string query = @"
                                SELECT Id
	                                  ,Name
                                FROM Foundation.Genres (NOLOCK)
                                WHERE Id = @Id";
            return GetByIdAsync(query, id);
        }
        public async Task<int> AddGenreAsync(RequestGenre reqgenre)
        {
            string query = @"
                        INSERT INTO Foundation.Genres (Name)
                        VALUES (@Name);
                        SELECT CAST(scope_identity() AS int);
                        ";
            return await AddAsync(query, reqgenre);
        }
        public async Task DeleteGenreAsync(int id)
        {
            string query = @"
                          DELETE FROM Foundation.Genres where Id=@Id;
                             DELETE From Foundation.Genres_Movies where GenreId=@Id";
            await DeleteAsync(query,id);
        }

        public async Task UpdateGenreAsync(int id_update, RequestGenre reqGenre)
        {
            string query = @"UPDATE Foundation.Genres
                             SET Name = @Name
                             WHERE Id = @id";
            await UpdateAsync(query,new { id=id_update, Name=reqGenre.Name });
        }
    }
}
