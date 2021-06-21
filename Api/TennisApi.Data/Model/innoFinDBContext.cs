using Microsoft.EntityFrameworkCore;


namespace TennisApi.Data.Model
{
    public partial class innoFinDBContext : DbContext
    {
        public innoFinDBContext()
        {
        }

        public innoFinDBContext(DbContextOptions<innoFinDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<NotaNegociacao> NotaNegociacao { get; set; }
        public virtual DbSet<NotaNegociacaoItem> NotaNegociacaoItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=innoFinDB; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CPF)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.FoneCel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FoneRes)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IDLoginNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.IDLogin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_Login");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.DataAlteracao).HasColumnType("datetime");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NotaNegociacao>(entity =>
            {
                entity.HasKey(e => e.IDNotaNegociacao);

                entity.Property(e => e.IDNotaNegociacao).ValueGeneratedNever();

                entity.Property(e => e.AnoMesReferencia).HasComputedColumnSql("(datepart(year,[DataReferencia])*(100)+datepart(month,[DataReferencia]))");

                entity.Property(e => e.DataAlteracao).HasColumnType("datetime");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.DataReferencia).HasColumnType("datetime");

                entity.Property(e => e.Emolumentos).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Execucao).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IRRF).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Impostos).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.NomeCorretora)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Outros).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TaxaANA).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TaxaCustodia).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TaxaLiquidacao).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TaxaOperacional).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TaxaRegistro).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TaxaTermoOpcao).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TotalBovespa)
                    .HasColumnType("decimal(12, 2)")
                    .HasComputedColumnSql("(([TaxaTermoOpcao]+[TaxaANA])+[Emolumentos])");

                entity.Property(e => e.TotalCBLC)
                    .HasColumnType("decimal(12, 2)")
                    .HasComputedColumnSql("(([ValorLiquidoOperacoes]+[TaxaLiquidacao])+[TaxaRegistro])");

                entity.Property(e => e.TotalCustos)
                    .HasColumnType("decimal(15, 2)")
                    .HasComputedColumnSql("((((([TaxaOperacional]+[Execucao])+[TaxaCustodia])+[Impostos])+[IRRF])+[Outros])");

                entity.Property(e => e.ValorLiquidoOperacoes).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IDLoginNavigation)
                    .WithMany(p => p.NotaNegociacao)
                    .HasForeignKey(d => d.IDLogin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotaNegociacao_Login");
            });

            modelBuilder.Entity<NotaNegociacaoItem>(entity =>
            {
                entity.HasKey(e => e.IDNotaNegociacaoItem);

                entity.Property(e => e.IDNotaNegociacaoItem).ValueGeneratedNever();

                entity.Property(e => e.DataAlteracao).HasColumnType("datetime");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.Negociacao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Prazo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TipoMercado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoNegociacao)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("C ou V (compra ou venda)");

                entity.Property(e => e.TipoOperacao)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("D ou C (débito ou crédito)");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ValorOperacao).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IDNotaNegociacaoNavigation)
                    .WithMany(p => p.NotaNegociacaoItem)
                    .HasForeignKey(d => d.IDNotaNegociacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotaNegociacaoItem_NotaNegociacao");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
