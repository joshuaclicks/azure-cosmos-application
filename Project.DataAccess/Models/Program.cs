using Project.DataAccess.Interfaces;

namespace Project.DataAccess.Models
{
    public class Program : IDatedIdentityCollection
    {
        public DateTime DateCreated { get; set; }
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
