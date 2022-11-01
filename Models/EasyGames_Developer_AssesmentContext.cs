using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Creolin_Gopal_Easy_Games_Developer_Test.Models
{
    public partial class EasyGames_Developer_AssesmentContext : DbContext
    {
        public EasyGames_Developer_AssesmentContext()
        {
        }

        public EasyGames_Developer_AssesmentContext(DbContextOptions<EasyGames_Developer_AssesmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<TransactionTbl> TransactionTbls { get; set; } = null!;
        public virtual DbSet<TransactionType> TransactionTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=CREOLIN;Initial Catalog=EasyGames_Developer_Assesment;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId)
                    .ValueGeneratedNever()
                    .HasColumnName("ClientID");

                entity.Property(e => e.ClientBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ClientName).HasMaxLength(50);

                entity.Property(e => e.ClientSurname).HasMaxLength(50);
            });

            modelBuilder.Entity<TransactionTbl>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__55433A4BCB7BE6AB");

                entity.ToTable("TransactionTbl");

                entity.Property(e => e.TransactionId)
                    .ValueGeneratedNever()
                    .HasColumnName("TransactionID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.TransactionAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransactionComment).HasMaxLength(100);

                entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TransactionTbls)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Clien__2F10007B");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.TransactionTbls)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Trans__2E1BDC42");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("TransactionType");

                entity.Property(e => e.TransactionTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("TransactionTypeID");

                entity.Property(e => e.TransactionTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
