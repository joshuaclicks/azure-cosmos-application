using System.ComponentModel.DataAnnotations;

namespace Project.DataAccess.Interfaces
{
    public interface IIdentityCollection
    {
        [Key]
        public string Id { get; set; }
    }
}
