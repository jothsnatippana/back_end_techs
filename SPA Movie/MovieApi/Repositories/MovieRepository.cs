using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Dapper;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using System;
using TechTalk.SpecFlow.CommonModels;
using static Microsoft.Azure.Amqp.Serialization.SerializableType;
using System.Linq;

namespace MovieApi.Repositories
{
    public class MovieRepository: BaseRepository<Movie>, IMovieRepository
    {
      
        private readonly string _connectionString;
   
        public MovieRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.DefaultConnection.ToString()) 
        {
            _connectionString = connectionString.Value.DefaultConnection; 
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            
            const string query = @"
                                   SELECT
                                        M.Id,
                                        M.Name,
                                        M.YearOfRelease,
                                        M.Plot,
                                        M.ProducerId,
                                        M.Poster,
                                        AM.Actors as Actors,
                                        GM.Genres as Genres
                                   FROM Foundation.Movies M (NOLOCK)
                                   INNER JOIN (
                                        SELECT MovieId, STRING_AGG(ActorId, ',') AS Actors
                                        FROM Foundation.Actors_Movies
                                        GROUP BY MovieId) AM ON M.Id=AM.MovieId
                                   INNER JOIN (
                                        SELECT MovieId, STRING_AGG(GenreId, ',') AS Genres
                                        FROM Foundation.Genres_Movies
                                        GROUP BY MovieId) GM ON M.Id=GM.MovieId  ";

              
              dynamic new_movies= await GetLists(query,new { });
              if (new_movies == null)
                return null;
              List<Movie> movies = new List<Movie>();
              foreach(var movie in new_movies)
              {
                var mydata = new { Id=movie.Id, Name=movie.Name, YearOfRelease = movie.YearOfRelease,Plot = movie.Plot, ProducerId = movie.ProducerId,Poster = movie.Poster, Actors = movie.Actors, Genres = movie.Genres};
                movies.Add(await Conversion(mydata));
              }
            return movies;
        }

       public async Task<Movie> Conversion(dynamic movies)
        {
            var actors = new List<Actor>();
            var genres = new List<Genre>();
           

            string actorIds= movies.Actors;
            string genreIds = movies.Genres;
            int producerId = movies.ProducerId;

            var actorIdList = actorIds.Split(',');
            var genreIdList = genreIds.Split(',');

            foreach (var actorId in actorIdList)
            {
                int id = int.Parse(actorId);
                const string queryActor = @"select Id,Name,Bio,Dob,Gender from Foundation.Actors (NOLOCK) where Id=@Id";
                dynamic actorResult = await GetLists(queryActor, new { Id = id });
                var actormodel = new Actor
                (
                    id,
                    actorResult[0].Name,
                    actorResult[0].Bio,
                    actorResult[0].Dob,
                    actorResult[0].Gender
                );
                actors.Add(actormodel);
            }

            foreach (var genreId in genreIdList)
            {
                int id = int.Parse(genreId);
                const string queryGenre = @"select Id,Name from Foundation.Genres (NOLOCK) where Id=@Id";
                dynamic genreResult = await GetLists(queryGenre, new { Id = id });
                var genremodel = new Genre(
                    id,
                    genreResult[0].Name
                );
                genres.Add(genremodel);
            }

            const string queryProducer = @"select Id,Name,Bio,Dob,Gender from Foundation.Producers (NOLOCK) where Id=@Id";
            dynamic producerResult = await GetLists(queryProducer, new { Id = producerId });
            var producermodel = new Producer
            (
                producerId,
                producerResult[0].Name,
                producerResult[0].Bio,
                producerResult[0].Dob,
                producerResult[0].Gender
            );

            var moviemodel = new Movie
            (
                movies.Id,
                movies.Name,
                movies.YearOfRelease,
                movies.Plot,
                actors,
                genres,
                producermodel,
                movies.Poster
            );
            return moviemodel;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            const string query = @"
                                 SELECT
                                    M.Id,
                                    M.Name,
                                    M.YearOfRelease,
                                    M.Plot,
                                    M.ProducerId,
                                    M.Poster,
                                    AM.Actors,
                                    GM.Genres
                                FROM FOUNDATION.Movies M (NOLOCK)
                                INNER JOIN (
                                    SELECT MovieId, STRING_AGG(ActorId, ',') AS Actors
                                    FROM FOUNDATION.Actors_Movies
                                    GROUP BY MovieId) AM ON M.Id=AM.MovieId
                                INNER JOIN (
                                    SELECT MovieId, STRING_AGG(GenreId, ',') AS Genres
                                    FROM FOUNDATION.Genres_Movies
                                    GROUP BY MovieId) GM ON M.Id=GM.MovieId
                                WHERE M.Id = @Id";
           
            dynamic movie = await GetLists(query,new { Id=id});
            if (movie is IEnumerable<object>)
            {
                List<object> newmovie = (List<object>)movie;
                if (!newmovie.Any())
                {
                    return null;
                }
            }      
            var mydata = new { Id = movie[0].Id, Name = movie[0].Name, YearOfRelease = movie[0].YearOfRelease, Plot = movie[0].Plot, ProducerId = movie[0].ProducerId, Poster = movie[0].Poster, Actors = movie[0].Actors, Genres = movie[0].Genres };
            return await Conversion(mydata);
        }

        
        public async Task<int> AddMovieAsync(RequestMovie reqMovie)
        {
            int result;
            const string actorquery = @"
                                 SELECT Id
	                                    ,Name
	                                    ,Bio
	                                    ,Dob
	                                    ,Gender
                                 FROM Foundation.Actors
                                 WHERE Id = @Id";
            const string genrequery = @"
                                SELECT Id
	                                  ,Name
                                FROM Foundation.Genres (NOLOCK)
                                WHERE Id = @Id";
            const string producerquery= @"  
                                 SELECT Id
	                                    ,Name
	                                    ,Bio
	                                    ,Dob
	                                    ,Gender
                                 FROM Foundation.Producers
                                 WHERE Id = @Id";
            foreach (var ids in reqMovie.ActorIds)
            {
                var newactor = await GetByIdAsync(actorquery, ids);
                if (newactor == null)
                    return -1;
            }
            foreach (var ids in reqMovie.GenreIds)
            {
                var newgenre = await GetByIdAsync(genrequery, ids);
                if (newgenre == null)
                    return -1;
            }
            var newproducer = await GetByIdAsync(producerquery, reqMovie.ProducedBy);
            if (newproducer == null)
                return -2;
            List<string> strings = reqMovie.ActorIds.ConvertAll<string>(x => x.ToString());
            var actorids = string.Join(", ", strings);
            //converting list<int> to string(genreids)
            List<string> strings2 = reqMovie.GenreIds.ConvertAll<string>(x => x.ToString());
            var genreids = string.Join(", ", strings2);
           
            DynamicParameters parameters = new();
            parameters.Add("Name", reqMovie.Name);
            parameters.Add("YearOfRelease", reqMovie.YearOfRelease);
            parameters.Add("Plot", reqMovie.Plot);
            parameters.Add("ActorIds", actorids);
            parameters.Add("GenreIds", genreids);
            parameters.Add("producedBy", reqMovie.ProducedBy);
            parameters.Add("coverimage", reqMovie.CoverImage);
            using (var connection = new SqlConnection(_connectionString.ToString()))
            {
                await connection.OpenAsync();
                result = await connection.ExecuteScalarAsync<int>("dbo.usp_InsertMovie",parameters, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task DeleteMovieAsync(int id)
        {
            string query = @"
                            DELETE From Foundation.Actors_Movies where MovieId=@Id;
                            DELETE FROM Foundation.Genres_Movies  where MovieId=@Id;
                            DELETE From Foundation.Review where MovieId=@Id;
                            DELETE FROM Foundation.Movies where Id=@Id;";
            await DeleteAsync(query, id);
        }

       
        public async Task<int> UpdateMovieAsync(int id, RequestMovie reqMovie)
        {
            const string actorquery = @"
                                 SELECT Id
	                                    ,Name
	                                    ,Bio
	                                    ,Dob
	                                    ,Gender
                                 FROM Foundation.Actors
                                 WHERE Id = @Id";
            const string genrequery = @"
                                SELECT Id
	                                  ,Name
                                FROM Foundation.Genres (NOLOCK)
                                WHERE Id = @Id";
            foreach (var ids in reqMovie.ActorIds)
            {
                var newactor = await GetByIdAsync(actorquery, ids);
                if (newactor == null)
                    return -1;
            }
            foreach (var ids in reqMovie.GenreIds)
            {
                var newgenre = await GetByIdAsync(genrequery, ids);
                if (newgenre == null)
                    return -1;
            }
            List<string> strings = reqMovie.ActorIds.ConvertAll<string>(x => x.ToString());
            var actorids = string.Join(", ", strings);
            //converting list<int> to string(genreids)
            List<string> strings2 = reqMovie.GenreIds.ConvertAll<string>(x => x.ToString());
            var genreids = string.Join(", ", strings2);

            DynamicParameters updateparameters = new();
            updateparameters.Add("movieid", id);
            updateparameters.Add("Name", reqMovie.Name);
            updateparameters.Add("YearOfRelease", reqMovie.YearOfRelease);
            updateparameters.Add("Plot", reqMovie.Plot);
            updateparameters.Add("ActorIds", actorids);
            updateparameters.Add("GenreIds", genreids);
            updateparameters.Add("producedBy", reqMovie.ProducedBy);
            updateparameters.Add("coverimage", reqMovie.CoverImage);
            using (var connection = new SqlConnection(_connectionString.ToString()))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync("dbo.usp_UpdateMovie", updateparameters, commandType: CommandType.StoredProcedure);
            }
            return 1;
        }
    }
}
