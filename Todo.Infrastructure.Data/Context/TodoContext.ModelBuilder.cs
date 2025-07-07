using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Data.Context
{
    public partial class TodoContext
    {
        private const string Schema = "todo";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

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
