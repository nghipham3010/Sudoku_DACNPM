using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Sudoku_Game.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<LoginInformation> LoginInformations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginInformation>()
                .Property(e => e.TK)
                .IsUnicode(false);

            modelBuilder.Entity<LoginInformation>()
                .Property(e => e.MK)
                .IsUnicode(false);
        }
    }
}
