using Microsoft.EntityFrameworkCore;
using Projet2.Models.Informations;
using System;

namespace Projet2.Models
{
    public class BddContext : DbContext
    {

        public DbSet<Account> Account { get; set; } //ajout d'un 's' à la fin

        public DbSet<InfoPerso> PersonnalInfo { get; set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<Planning> Planning { get; set; }

        public DbSet<Slot> Slot { get; set; }

        public DbSet<Activity> Activity { get; set; }

        public DbSet<ActiviteAssociation> AssociationActivity { get; set; }

        public DbSet<Benevole> Benevoles { get; set; } //ajout d'un 's' à la fin

        public DbSet<Adherent> Adherents { get; set; } //ajout d'un 's' à la fin

        public DbSet<Adhesion> Adhesion { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<Contribution> Contributions { get; set; }//ajout d'un 's' à la fin

        public DbSet<SportAssociation> SportAssociation { get; set; }

        public DbSet<Games> Game { get; set; }

        public DbSet<Profile> Profils { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Stuff> Stuff { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Chat> Chat { get; set; }

        public DbSet<Forum> Forums { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Coaching> Training { get; set; }

        public DbSet<Adhesion> Adhesions { get; set; }// ajout d'un 's' à la fin

        public DbSet<Team> Teams { get; set; }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Adherents.AddRange(
                new Adherent() { Id = 1, BenevoleId = null, NumAdherent = 1, InscriptionDate = new DateTime(2000, 12, 25), Contribution=400.80, IDDocuments = "justification-OUI", TeamId = null, AdhesionId = null, CoachingId = null },
                new Adherent() { Id = 2, BenevoleId = null, NumAdherent = 2, InscriptionDate = new DateTime(2000, 12, 30), Contribution=250.25, IDDocuments = "justification-NON", TeamId= null,AdhesionId= null,CoachingId= null });

            this.Contributions.AddRange(
                new Contribution()
                {
                    Id = 1,
                    PaymentStatus = true,
                    TotalCount = 600.25,
                    PrelevementDate = PrelevementDate.CinqDuMois,
                    ContributionType = ContributionType.Mensuel
                },
                new Contribution()
                {
                    Id = 2,
                    PaymentStatus = false,
                    TotalCount = 800.33,
                    PrelevementDate = PrelevementDate.VingtCingDuMois,
                    ContributionType = ContributionType.Annuel
                });
            this.Adhesions.AddRange(
                new Adhesion()
                {
                    Id = 1,
                    ContributionId = null,
                    Echeance = DateTime.Now,
                    AdhesionStatus = AdhesionStatus.Verfie
                },
                new Adhesion()
                {
                    Id = 2,
                    ContributionId = null,
                    Echeance = DateTime.Now,
                    AdhesionStatus = AdhesionStatus.EnCours
                }
                );
            

            this.SaveChanges();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnexionSQL.connexion);
        }


    }


}
