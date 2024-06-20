namespace Project.DataAccess.DataObjects
{
    public class CountryDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ShortCode { get; set; } = null!;
        public string DialingCode { get; set; } = null!;
    }
}
