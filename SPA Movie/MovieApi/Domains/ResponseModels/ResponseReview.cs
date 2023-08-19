namespace MovieApi.Domains.ResponseModels
{
    public class ResponseReview
    {
        public ResponseReview(int id, int movieId, string message)
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
