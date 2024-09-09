using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.SqlServer;
using SubjectTracker.Models;

namespace SubjectTracker.Brokers
{
    public interface IStorageBroker
    {
        Task<Subject> InsertSubjectAsync(Subject subject);
    }
    public class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private IConfiguration configuration;

        public StorageBroker(IConfiguration keyConfiguration)
        {
            this.configuration = keyConfiguration;
            this.Database.Migrate();
        }
        public DbSet<Subject> Subjects{ get; set; }

        public async Task<Subject> InsertSubjectAsync(Subject subject)
        {
            StorageBroker storageBroker = new StorageBroker(configuration);
            await storageBroker.Subjects.AddAsync(subject);
            await storageBroker.SaveChangesAsync();

            return subject;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = this.configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
