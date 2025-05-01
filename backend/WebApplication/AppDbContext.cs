using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication.Entities;

namespace WebApplication
{
    public class AppDbContext(IConfiguration configuration) : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration["ConnectionString"]);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>();
            modelBuilder.Entity<FirebaseSubscriber>();

            modelBuilder.Entity<TelegramPost>()
                .HasIndex(x => new { x.ChatId, x.MessageId });

            modelBuilder.Entity<TelegramVote>()
                .HasIndex(x => new { x.TelegramPostId, x.UserId });
        }
    }
}