using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVC.Models
{
    public partial class TareasContext : DbContext
    {
        public TareasContext()
        {
        }

        public TareasContext(DbContextOptions<TareasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-81L6KLR; Database=Tareas;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Estado");

                entity.Property(e => e.Estado1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("empresa");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .HasColumnName("estado")
                    .IsFixedLength();

                entity.Property(e => e.FechaDeFinalizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaDeFinalizacion");

                entity.Property(e => e.FechaDeInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaDeInicio");

                entity.Property(e => e.FechaLimite)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaLimite");

                entity.Property(e => e.IdPadre)
                    .HasMaxLength(10)
                    .HasColumnName("idPadre")
                    .IsFixedLength();

                entity.Property(e => e.Observacion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.Property(e => e.Recursos)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("recursos");

                entity.Property(e => e.Responsables)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("responsables");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
