using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class DrosasPruebaPacienteContext : DbContext
{
    public DrosasPruebaPacienteContext()
    {
    }

    public DrosasPruebaPacienteContext(DbContextOptions<DrosasPruebaPacienteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<TipoSangre> TipoSangres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= DRosasPruebaPaciente; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__C93DB49BE1ACCA8A");

            entity.ToTable("Paciente");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Diagnostico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaNaciemiento).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdTipoSangreNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdTipoSangre)
                .HasConstraintName("FK__Paciente__IdTipo__1273C1CD");
        });

        modelBuilder.Entity<TipoSangre>(entity =>
        {
            entity.HasKey(e => e.IdTipoSangre).HasName("PK__TipoSang__3FA617D9154AECED");

            entity.ToTable("TipoSangre");

            entity.Property(e => e.IdTipoSangre).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
