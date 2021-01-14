using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Models
{
    public partial class DbProdContext : DbContext
    {
        public DbProdContext()
        {
        }

        public DbProdContext(DbContextOptions<DbProdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblDocumentos> TblDocumentos { get; set; }
        public virtual DbSet<TblEmails> TblEmails { get; set; }
        public virtual DbSet<TblPessoas> TblPessoas { get; set; }
        public virtual DbSet<TblSenhas> TblSenhas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblDocumentos>(entity =>
            {
                entity.HasKey(e => e.NIdDocumento);

                entity.ToTable("tblDocumentos");

                entity.Property(e => e.NIdDocumento).HasColumnName("nIdDocumento");

                entity.Property(e => e.DAlteracao)
                    .HasColumnName("dAlteracao")
                    .HasColumnType("datetime");

                entity.Property(e => e.DFim)
                    .HasColumnName("dFim")
                    .HasColumnType("datetime");

                entity.Property(e => e.DInicio)
                    .HasColumnName("dInicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.NIdPessoa).HasColumnName("nIdPessoa");

                entity.Property(e => e.SDocumento)
                    .IsRequired()
                    .HasColumnName("sDocumento")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.NIdPessoaNavigation)
                    .WithMany(p => p.TblDocumentosNIdPessoaNavigation)
                    .HasForeignKey(d => d.NIdPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDocumentos_tblPessoas");
            });

            modelBuilder.Entity<TblEmails>(entity =>
            {
                entity.HasKey(e => e.NIdEmail);

                entity.ToTable("tblEmails");

                entity.Property(e => e.NIdEmail).HasColumnName("nIdEmail");

                entity.Property(e => e.DAlteracao)
                    .HasColumnName("dAlteracao")
                    .HasColumnType("datetime");

                entity.Property(e => e.DFim)
                    .HasColumnName("dFim")
                    .HasColumnType("datetime");

                entity.Property(e => e.DInicio)
                    .HasColumnName("dInicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.NIdPessoa).HasColumnName("nIdPessoa");

                entity.Property(e => e.NValidado).HasColumnName("nValidado");

                entity.Property(e => e.SEmail)
                    .IsRequired()
                    .HasColumnName("sEmail")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.NIdPessoaNavigation)
                    .WithMany(p => p.TblEmailsNIdPessoaNavigation)
                    .HasForeignKey(d => d.NIdPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblEmails_tblPessoas");
            });

            modelBuilder.Entity<TblPessoas>(entity =>
            {
                entity.HasKey(e => e.NIdPessoa);

                entity.ToTable("tblPessoas");

                entity.Property(e => e.NIdPessoa).HasColumnName("nIdPessoa");

                entity.Property(e => e.DAlteracao)
                    .HasColumnName("dAlteracao")
                    .HasColumnType("datetime");

                entity.Property(e => e.DFim)
                    .HasColumnName("dFim")
                    .HasColumnType("datetime");

                entity.Property(e => e.DInicio)
                    .HasColumnName("dInicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.DNascimento)
                    .HasColumnName("dNascimento")
                    .HasColumnType("date");

                entity.Property(e => e.SNome)
                    .IsRequired()
                    .HasColumnName("sNome")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SNomeApelido)
                    .HasColumnName("sNomeApelido")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSenhas>(entity =>
            {
                entity.HasKey(e => e.NIdSenha);

                entity.ToTable("tblSenhas");

                entity.Property(e => e.NIdSenha).HasColumnName("nIdSenha");

                entity.Property(e => e.DAlteracao)
                    .HasColumnName("dAlteracao")
                    .HasColumnType("datetime");

                entity.Property(e => e.DFim)
                    .HasColumnName("dFim")
                    .HasColumnType("datetime");

                entity.Property(e => e.DInicio)
                    .HasColumnName("dInicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.NIdPessoa).HasColumnName("nIdPessoa");

                entity.Property(e => e.SSenha)
                    .IsRequired()
                    .HasColumnName("sSenha")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.NIdPessoaNavigation)
                    .WithMany(p => p.TblSenhas)
                    .HasForeignKey(d => d.NIdPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSenhas_tblPessoas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
