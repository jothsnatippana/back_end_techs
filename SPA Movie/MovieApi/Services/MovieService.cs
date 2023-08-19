using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using MovieApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _MovieRepository;
        readonly Validations v;
        public MovieService(IMovieRepository movieRepository)
        {
            _MovieRepository = movieRepository;
            v=new Validations();    
        }

        public async Task<IEnumerable<ResponseMovie>> GetAllMoviesAsync()
        {
            var MovieData = await _MovieRepository.GetAllMoviesAsync();
            List<ResponseMovie> movies = new();
            foreach (var movie in MovieData)
            {
                List<string> newactors = movie.Actors.Select(r => r.Name).ToList();
                List<string> newGenres = movie.Genres.Select(r => r.Name).ToList();
                string newproducer = movie.ProducedBy.Name;
                movies.Add(new ResponseMovie(movie.Id, movie.Name,
                                  movie.YearOfRelease,
                                      movie.Plot,
                                      newactors,
                                      newGenres,
                                      newproducer,
                                      movie.CoverImage));

            }
            return movies;
        }

        public async Task<ResponseMovie> GetMovieByIdAsync(int id)
        {
            var movies=await _MovieRepository.GetMovieByIdAsync(id);
            if (movies == null) 
                return null;
            List<string> newactors = movies.Actors.Select(r => r.Name).ToList();
            List<string> newGenres = movies.Genres.Select(r => r.Name).ToList();
            string newproducer = movies.ProducedBy.Name;
            return new ResponseMovie(movies.Id, movies.Name,
                                      movies.YearOfRelease,
                                      movies.Plot,
                                      newactors,
                                      newGenres,
                                      newproducer,
                                      movies.CoverImage);
        }

        public async Task<int> AddMovieAsync(RequestMovie reqMovie)
        {   
            var id=await _MovieRepository.AddMovieAsync(reqMovie);
            return id;
        }

        public async Task DeleteMovieAsync(int id)
        { 
         await _MovieRepository.DeleteMovieAsync(id);
        }

       
        public async Task<int> UpdateMovieAsync(int id, RequestMovie reqMovie)
        {
            return await _MovieRepository.UpdateMovieAsync(id, reqMovie);
        }
    }
}
