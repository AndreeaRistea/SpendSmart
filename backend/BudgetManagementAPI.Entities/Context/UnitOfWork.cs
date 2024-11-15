using BudgetManagementAPI.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Entities.Context
{
    public class UnitOfWork : DbContext
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Budget> Budgets { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<Lesson> Lessons { get; set; }

        public virtual DbSet<LevelLesson> LevelLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

           
            modelBuilder.UseSerialColumns();
        }
    }
}
