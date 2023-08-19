using System;
namespace MovieApi.Domains.Models
{
    public class Actor
    {
        public Actor(int id, string name, string bio, DateTime dOB, string gender)
        {
            this.Id = id;
            this.Name = name;
            this.Bio = bio;
            this.DOB = dOB;
            this.Gender = gender;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
    }
    
}
