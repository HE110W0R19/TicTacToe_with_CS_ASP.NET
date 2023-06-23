namespace FirstWebApp.Models
{
    public class User
    {
        public string Name { get; set; }
        public bool IsMakeMove { get; set; }

        public User(string name)
        {
            Name = name;
            IsMakeMove = false;
        }
    }
}
