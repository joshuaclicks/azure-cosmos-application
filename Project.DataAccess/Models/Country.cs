using Project.DataAccess.Interfaces;

namespace Project.DataAccess.Models
{
    public class Country : IIdentityCollection
    {
        public string Name { get; set; } = null!;
        public string ShortCode { get; set; } = null!;
        public string DialingCode { get; set; } = null!;
        public string FlagUrl { get; set; } = null!;
        public string Id { get; set; } = null!;
    }
}
