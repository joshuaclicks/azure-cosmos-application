using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Models;

namespace Project.DataAccess.Contexts
{
    public partial class EmployerApplicationContext : DbContext
    {
        public EmployerApplicationContext()
        {
        }

        public EmployerApplicationContext(DbContextOptions<EmployerApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; }

        public virtual DbSet<CandidateResponse> CandidateResponses { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Program> Programs { get; set; }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

        public virtual DbSet<QuestionType> QuestionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
            .ToContainer(nameof(Candidate))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            modelBuilder.Entity<CandidateResponse>()
            .ToContainer(nameof(CandidateResponse))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            modelBuilder.Entity<Country>()
            .ToContainer(nameof(Country))
            .HasPartitionKey(c => c.Id);

            modelBuilder.Entity<Program>()
            .ToContainer(nameof(Program))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            modelBuilder.Entity<Question>()
            .ToContainer(nameof(Question))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            modelBuilder.Entity<QuestionOption>()
            .ToContainer(nameof(QuestionOption))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            modelBuilder.Entity<QuestionType>()
            .ToContainer(nameof(QuestionType))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
