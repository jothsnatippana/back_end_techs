namespace MovieApi.Domains.Models
{
    public class Review
    {
        public Review(int id, string message, int movieId)
        {
            Id = id;
            Message = message;
            MovieId = movieId;
        }

        public int Id { get; set; }  
        public string Message { get; set; }
        public int MovieId { get; set; }
    }
}
