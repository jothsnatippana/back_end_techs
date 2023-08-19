using System.Collections.Generic;
using MovieApi.Domains.Models;
namespace MovieApi.Domains.RequestModels
{
    public class RequestMovie
    {
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<int> ActorIds { get; set; }
        public List<int> GenreIds { get; set; }
        public int ProducedBy { get; set; }
        public string CoverImage { get; set; }
    }
}
