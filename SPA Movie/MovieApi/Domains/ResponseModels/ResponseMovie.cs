using System.Collections.Generic;
namespace MovieApi.Domains.ResponseModels
{
    public class ResponseMovie
    {
        public ResponseMovie(int id, string name, int yearOfRelease, string plot, List<string> actors, List<string> genres, string producers, string coverImage)
        {
            Id = id;
            Name = name;
            YearOfRelease = yearOfRelease;
            Plot = plot;
            Actors = actors;
            Genres = genres;
            Producers = producers;
            CoverImage = coverImage;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<string> Actors { get; set; }
        public List<string> Genres { get; set; }
        public string Producers { get; set; }
        public string CoverImage { get; set; }
    }
}
