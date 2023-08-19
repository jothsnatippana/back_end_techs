namespace MovieApi.Domains.ResponseModels
{
    public class ResponseGenre
    {
        public ResponseGenre(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}
