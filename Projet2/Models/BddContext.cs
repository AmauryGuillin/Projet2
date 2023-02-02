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

        public DbSet<Contribution> Contribution { get; set; }

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




        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Adherents.AddRange(
                new Adherent() { Id = 1, BenevoleId = null, NumAdherent = 1, InscriptionDate = new DateTime(2000, 12, 25), IDDocuments = "justification-OUI"},
                new Adherent() { Id = 2, BenevoleId = null, NumAdherent = 2, InscriptionDate = new DateTime(2000, 12, 30), IDDocuments = "justification-NON" });

            this.Benevoles.AddRange(
                new Benevole() { Id = 1, AccountId = null, NbActionVolunteering = 15 },
                new Benevole() { Id = 2, AccountId = null, NbActionVolunteering = 3 }
                );
            this.Account.Add(
                new Account()
                {
                    Id = 1,
                    Username = "TOTO",
                    Password = "111",
                    InfoPersoId = null,
                    ContactId = null,
                    PlanningId = null,
                    SportAssociationId = null,
                    InventoryId = null
                }
                );
            this.SaveChanges();
            using (Dal dal = new Dal())
            {

                //dal.EditAdherent(1, 1, 33, new DateTime(2000, 12, 25), 33.33, "OUI", 1, 1, 1);
                //dal.EditAdhesion(1, 1, DateTime.Now, AdhesionStatus.EnCours);
                //dal.EditContribution(1, true, 33.33, PrelevementDate.VingtCingDuMois, ContributionType.Annuel);
                //dal.EditTeam(2, "Les Tartines de Gruillere", 1, new DateTime(2002, 02, 02), 46);


                //dal.RemoveAdherent(1);
                //dal.RemoveContribution(1);
                //dal.RemoveAdhesion(1);
                //dal.RemoveTeam(1);

                // dal.EditBenevole(1, 1, 45);
                dal.RemoveBenevole(1);
            }

        }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnexionSQL.connexion);
        }


    }


}
