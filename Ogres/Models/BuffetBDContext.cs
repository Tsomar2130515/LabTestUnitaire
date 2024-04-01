using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

#nullable disable

namespace Ogres.Models
{
    public partial class BuffetBDContext : DbContext
    {

        public BuffetBDContext()
        {
        }

        public BuffetBDContext(DbContextOptions<BuffetBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Plat> Plats { get; set; }
        public virtual DbSet<TypePlat> TypePlats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BuffetBDSolution;Trusted_Connection=True;");
            }
        }
    }
}
