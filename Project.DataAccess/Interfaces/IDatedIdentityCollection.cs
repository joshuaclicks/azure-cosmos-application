namespace Project.DataAccess.Interfaces
{
    public interface IDatedIdentityCollection : IIdentityCollection
    {
        public DateTime DateCreated { get; set; }
    }
}
