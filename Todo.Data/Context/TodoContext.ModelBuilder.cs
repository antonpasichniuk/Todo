using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Data.Context
{
    public partial class TodoContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TasksList>(builder =>
            {
                builder.HasOne(tl => tl.CreatedBy)
                    .WithMany()
                    .HasForeignKey(tl => tl.CreatedById)
                    .IsRequired();

                builder.Property(tl => tl.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TasksListAccess>(builder =>
            {
                builder.HasKey(listAccess => new { listAccess.UserId, listAccess.TasksListId });

                builder.Property(listAccess => listAccess.AccessRole)
                    .HasConversion<string>();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
