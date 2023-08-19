using System.Collections.Generic;
namespace MovieApi.Domains.Models
{
    public class Movie
    {
        public Movie(int id, string name, int yearOfRelease, string plot, List<Actor> actors, List<Genre> genres, Producer producedBy, string coverImage)
        {
            Id = id;
            Name = name;
            YearOfRelease = yearOfRelease;
            Plot = plot;
            Actors = actors;
            Genres = genres;
            ProducedBy = producedBy;
            CoverImage = coverImage;
        }

        public Movie()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Genre> Genres { get; set; }
        public Producer ProducedBy { get; set; }
        public string CoverImage { get; set; }
    }
}
