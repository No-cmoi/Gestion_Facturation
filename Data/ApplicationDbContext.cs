using Microsoft.EntityFrameworkCore;
using Gestion_Facturation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Gestion_Facturation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Devis> Devis { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<LigneDevis> LigneDevis { get; set; }
        public DbSet<LigneFacture> LigneFacture { get; set; }
        public DbSet<Tarif> Tarifs { get; set; }
        public DbSet<Acompte> Acomptes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de l'entité Projet
            modelBuilder.Entity<Projet>(entity =>
            {
                entity.ToTable("Projets");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.NomProjet).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Description).IsRequired().HasColumnType("TEXT");
                entity.Property(p => p.DateDebut).IsRequired();
                entity.Property(p => p.DateFin).IsRequired();

                entity.HasOne(p => p.Client)
                      .WithMany(c => c.Projets)
                      .HasForeignKey(p => p.ClientId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Devis)
                      .WithOne(d => d.Projet)
                      .HasForeignKey(d => d.ProjetId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Factures)
                      .WithOne(f => f.Projet)
                      .HasForeignKey(f => f.ProjetId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuration de l'entité Client
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nom).IsRequired().HasMaxLength(100);
                entity.Property(c => c.AdresseRue).IsRequired().HasMaxLength(255);
                entity.Property(c => c.AdresseCp).IsRequired().HasMaxLength(20);
                entity.Property(c => c.AdresseVille).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(c => c.Email).IsUnique();
                entity.Property(c => c.Telephone).HasMaxLength(20);
                
                entity.HasMany(c => c.Projets)
                      .WithOne(p => p.Client)
                      .HasForeignKey(p => p.ClientId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuration de l'entité Devis
            modelBuilder.Entity<Devis>(entity =>
            {
                entity.ToTable("Devis");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.NumeroDevis).IsRequired().HasMaxLength(20);
                entity.Property(d => d.DateDevis).IsRequired();
                entity.Property(d => d.MontantTotal).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(d => d.Statut).IsRequired().HasConversion<string>();
                entity.HasIndex(d => d.Statut).IsUnique();

                entity.HasOne(d => d.Projet)
                      .WithMany(p => p.Devis)
                      .HasForeignKey(d => d.ProjetId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.LignesDevis)
                      .WithOne(ld => ld.Devis)
                      .HasForeignKey(ld => ld.DevisId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuration de l'entité Facture
            modelBuilder.Entity<Facture>(entity =>
            {
                entity.ToTable("Factures");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.NumeroFacture).IsRequired().HasMaxLength(50);
                entity.Property(f => f.DateFacture).IsRequired();
                entity.Property(f => f.MontantTotal).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(f => f.StatutFacture).IsRequired().HasConversion<string>();
                entity.HasIndex(f => f.StatutFacture).IsUnique();

                entity.HasOne(f => f.Projet)
                      .WithMany(p => p.Factures)
                      .HasForeignKey(f => f.ProjetId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(f => f.LignesFacture)
                      .WithOne(lf => lf.Facture)
                      .HasForeignKey(lf => lf.FactureId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(f => f.Acomptes)
                      .WithOne(a => a.Facture)
                      .HasForeignKey(a => a.FactureId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuration de l'entité LigneDevis
            modelBuilder.Entity<LigneDevis>(entity =>
            {
                entity.ToTable("LignesDevis");
                entity.HasKey(ld => ld.Id);
                entity.Property(ld => ld.Quantité).IsRequired();

                entity.HasOne(ld => ld.Devis)
                      .WithMany(d => d.LignesDevis)
                      .HasForeignKey(ld => ld.DevisId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ld => ld.Tarif)
                      .WithMany(t => t.LignesDevis)
                      .HasForeignKey(ld => ld.TarifId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuration de l'entité LigneFacture
            modelBuilder.Entity<LigneFacture>(entity =>
            {
                entity.ToTable("LignesFacture");
                entity.HasKey(ld => ld.Id);
                entity.Property(ld => ld.Quantité).IsRequired();

                entity.HasOne(ld => ld.Facture)
                      .WithMany(d => d.LignesFacture)
                      .HasForeignKey(ld => ld.FactureId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ld => ld.Tarif)
                      .WithMany(t => t.LignesFacture)
                      .HasForeignKey(ld => ld.TarifId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuration de l'entité Acompte
            modelBuilder.Entity<Acompte>(entity =>
            {
                entity.ToTable("Acomptes");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.DateAcompte).IsRequired();
                entity.Property(a => a.MontantAcompte).HasColumnType("decimal(18, 2)").IsRequired();

                entity.HasOne(a => a.Facture)
                      .WithMany(f => f.Acomptes)
                      .HasForeignKey(a => a.FactureId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuration de l'entité Tarif
            modelBuilder.Entity<Tarif>(entity =>
            {
                entity.ToTable("Tarifs");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Description).IsRequired().HasMaxLength(255);
                entity.Property(t => t.PrixUnitaire).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(t => t.TypeTarif).IsRequired().HasConversion<string>();

                entity.HasMany(t => t.LignesDevis)
                      .WithOne(ld => ld.Tarif)
                      .HasForeignKey(ld => ld.TarifId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(t => t.LignesFacture)
                      .WithOne(lf => lf.Tarif)
                      .HasForeignKey(lf => lf.TarifId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuration des clés pour Identity
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(127);
                entity.Property(e => e.NormalizedName).HasMaxLength(127);
            });

            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(127);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(127);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(127);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(e => e.LoginProvider).HasMaxLength(127);
                entity.Property(e => e.ProviderKey).HasMaxLength(127);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(e => e.LoginProvider).HasMaxLength(127);
                entity.Property(e => e.Name).HasMaxLength(127);
            });
        }
    }
}
