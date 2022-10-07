using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SAExpiations.Models;

namespace SAExpiations.Data
{
    public partial class ExpiationsContext : DbContext
    {
        public ExpiationsContext()
        {
        }

        public ExpiationsContext(DbContextOptions<ExpiationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Expiation> Expiations { get; set; } = null!;
        public virtual DbSet<ExpiationCategory> ExpiationCategories { get; set; } = null!;
        public virtual DbSet<ExpiationOffence> ExpiationOffences { get; set; } = null!;
        public virtual DbSet<LocalServiceArea> LocalServiceAreas { get; set; } = null!;
        public virtual DbSet<OffenceStatus> OffenceStatus { get; set; } = null!;
        public virtual DbSet<PhotoRejectionReason> PhotoRejectionReasons { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expiation>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.ExpiationOffenceCode, "IX_Expiations_Code");

                entity.HasIndex(e => e.IncidentStartDate, "IX_Expiations_Date");

                entity.HasIndex(e => e.DriverLicenseState, "IX_Expiations_State");

                entity.Property(e => e.BloodAlcoholContentExp).HasColumnType("decimal(4, 3)");

                entity.Property(e => e.DriverLicenseState)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiationOffenceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IncidentStartDate).HasColumnType("datetime");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.LocalServiceAreaCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NoticeStatusDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NoticeTypeDesc)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OffenceStatusCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationState)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SpeedCameraCategory)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.WithdrawnReasonDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.DriverLicenseStateNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DriverLicenseState)
                    .HasConstraintName("FK_ANew_DriverState");

                entity.HasOne(d => d.ExpiationOffenceCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ExpiationOffenceCode)
                    .HasConstraintName("FK_ANew_ExpiationOffences");

                entity.HasOne(d => d.LocalServiceAreaCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.LocalServiceAreaCode)
                    .HasConstraintName("FK_ANew_LocalServiceAreas");

                entity.HasOne(d => d.OffenceStatusCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.OffenceStatusCode)
                    .HasConstraintName("FK_ANew_OffenceStatus");

                entity.HasOne(d => d.PhotoRejectionCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.PhotoRejectionCode)
                    .HasConstraintName("FK_ANew_PhotoRejectionReasons");

                entity.HasOne(d => d.RegistrationStateNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.RegistrationState)
                    .HasConstraintName("FK_ANew_RegoState");
            });

            modelBuilder.Entity<ExpiationCategory>(entity =>
            {
                entity.HasKey(e => e.ExpiationOffenceCode)
                    .HasName("PK__Expiatio__49B91CC00D61BE50");

                entity.Property(e => e.ExpiationOffenceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExpiationOffenceCodeNavigation)
                    .WithOne(p => p.ExpiationCategory)
                    .HasForeignKey<ExpiationCategory>(d => d.ExpiationOffenceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpiationCategories_ExpiationOffences");
            });

            modelBuilder.Entity<ExpiationOffence>(entity =>
            {
                entity.HasKey(e => e.ExpiationOffenceCode);

                entity.Property(e => e.ExpiationOffenceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiationOffenceDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LocalServiceArea>(entity =>
            {
                entity.HasKey(e => e.LocalServiceAreaCode);

                entity.Property(e => e.LocalServiceAreaCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("localServiceAreaCode");

                entity.Property(e => e.LocalServiceArea1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("localServiceArea");
            });

            modelBuilder.Entity<OffenceStatus>(entity =>
            {
                entity.HasKey(e => e.OffenceStatusCode);

                entity.ToTable("OffenceStatus");

                entity.Property(e => e.OffenceStatusCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OffenceStatusDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PhotoRejectionReason>(entity =>
            {
                entity.HasKey(e => e.PhotoRejectionCode);

                entity.Property(e => e.PhotoRejectionCode).ValueGeneratedNever();

                entity.Property(e => e.PhotoRejecetionReason)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.State1);

                entity.Property(e => e.State1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("State");

                entity.Property(e => e.StateDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
