namespace EMS
{
    public class responseEmployee
    {
        public responseEmployee(dynamic name1, dynamic role1)
        {
            name = name1;
            role= role1;
        }

        public string name { get; set; }
        public string role { get; set; }
    }
}
