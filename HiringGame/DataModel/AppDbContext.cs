using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HiringGame.DataModel
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Video> Videos { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new QuestionTypeConfigurations());
            modelBuilder.ApplyConfiguration(new PersonalityAnswerChoiceConfigurations());

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Question)
                .WithMany(t => t.Transactions)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Answer)
                .WithMany(t => t.Transactions)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }


}
