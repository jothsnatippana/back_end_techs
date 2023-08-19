using System;
using System.Collections.Generic;

namespace MovieApi.Domains.RequestModels
{
    public class RequestActor
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
    }
}
