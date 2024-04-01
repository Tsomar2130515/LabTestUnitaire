using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cuisine.Modeles;

public partial class BuffetBdsolutionContext : DbContext
{
    public BuffetBdsolutionContext()
    {
    }

    public BuffetBdsolutionContext(DbContextOptions<BuffetBdsolutionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Plat> Plats { get; set; }

    public virtual DbSet<TypePlat> TypePlats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BuffetBDSolution;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plat>(entity =>
        {
            entity.HasIndex(e => e.TypePlatId, "IX_Plats_TypePlatId");

            entity.HasOne(d => d.TypePlat).WithMany(p => p.Plats).HasForeignKey(d => d.TypePlatId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
