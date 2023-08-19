using System.Collections.Generic;

namespace EMS
{
    public class requestteam
    {
        public requestteam(string name, List<int> members)
        {
            this.name = name;
            this.members = members;
        }

        public string name { get; set; }
        public List<int> members { get; set; }
    }
}
