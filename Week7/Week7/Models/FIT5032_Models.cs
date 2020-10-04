namespace Week7.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FIT5032_Models : DbContext
    {
        public FIT5032_Models()
            : base("name=FIT5032_Models")
        {
        }

        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Units> Units { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>()
                .HasMany(e => e.Units)
                .WithRequired(e => e.Students)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);
        }
    }
}
