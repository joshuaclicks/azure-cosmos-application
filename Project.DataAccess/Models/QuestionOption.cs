using Project.DataAccess.Interfaces;

namespace Project.DataAccess.Models
{
    public class QuestionOption : IIdentityCollection
    {
        public string Id { get; set; } = null!;
        public string Option { get; set; } = null!;

    }
}
