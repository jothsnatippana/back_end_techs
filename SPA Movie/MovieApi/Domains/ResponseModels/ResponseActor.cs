using System;
namespace MovieApi.Domains.ResponseModels
{
    public class ResponseActor
    {
        public ResponseActor()
        {
        }

        public ResponseActor(int id, string name, string bio, DateTime dOB, string gender)
        {
            Id = id;
            Name = name;
            Bio = bio;
            DOB = dOB;
            Gender = gender;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }

      
    }
}
