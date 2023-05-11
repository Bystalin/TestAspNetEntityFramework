using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CRUDEntityFramework.Models
{
    public partial class BD : DbContext
    {
        public BD()
            : base("name=BD")
        {
        }

        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<plato> plato { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<categoria>()
                .Property(e => e.categoria1)
                .IsUnicode(false);

            modelBuilder.Entity<categoria>()
                .HasMany(e => e.plato)
                .WithRequired(e => e.categoria)
                .HasForeignKey(e => e.idcategoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<plato>()
                .Property(e => e.precio)
                .HasPrecision(4, 2);
        }
    }
}
