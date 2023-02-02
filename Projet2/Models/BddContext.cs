using Microsoft.EntityFrameworkCore;
using Projet2.Models.Informations;
using System;
using System.Security.AccessControl;
using System.Xml.Linq;

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
        
   
        public DbSet<Contribution> Contributions { get; set; }//ajout d'un 's' à la fin
        public DbSet<SportAssociation> SportAssociation { get; set; }
      
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
        public DbSet<Game> Games { get; set; }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<Publication> Publications { get; set; }





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
                });

            this.Teams.AddRange(
                new Team()
                {
                    Id = 1,
                    Name = "Les BG du 44",
                    GameId = null,
                    CreationDate = new DateTime(2001, 01, 01),
                    NbAdherent = 5
                },
                new Team()
                {
                    Id = 2,
                    Name = "Les Tartines de Fromages",
                    GameId = null,
                    CreationDate = new DateTime(2002, 02, 02),
                    NbAdherent = 15
                });

            this.Training.Add(
                new Coaching() 
                { 
                    Id = 1, 
                    Level = Level.Galadriel 
                });

            this.Games.Add(
                new Models.Game()
                {
                    Id = 1,
                    GameType = "console",
                    GameList = GameList.Mk
                });

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
                });

            this.Employees.AddRange(
                new Employee()
                {
                    Id = 1,
                    JobName = "Administrateur",
                    DateOfEmployement= DateTime.Now,
                    SerialNumber = "AAG0001",
                    AccountId = null,
                },

                new Employee()
                {
                    Id = 2,
                    JobName = "Coach",
                    DateOfEmployement= DateTime.Now,
                    SerialNumber = "CAC0001",
                    AccountId = null,
                },

                new Employee()
                {
                    Id= 3,
                    JobName = "Gestionnaire",
                    DateOfEmployement = DateTime.Now,
                    SerialNumber = "GMB0001",
                    AccountId = null,
                },
                new Employee()
                {
                    Id = 4,
                    JobName = "Administrateur",
                    DateOfEmployement = DateTime.Now,
                    SerialNumber = "AAEM0002",
                    AccountId = null,
                });

            this.Publications.AddRange(
                new Publication()
                {
                    Id = 1,
                    Name = "Règles à respectées sur le Forum",
                    PublicationType = PublicationTypes.ArticlesInformatifs,
                    Content = "contenu de la publi",
                    Date= DateTime.Now,
                    Author="Amaury",
                    EmployeeId = 1,
                },

                new Publication()
                {
                    Id = 2,
                    Name = "Video de la remises des prix du tournois du 15/02/2023",
                    PublicationType = PublicationTypes.Video,
                    Content = "contenu de la publi",
                    Date = DateTime.Now,
                    Author="Abigael",
                    EmployeeId = 2,
                },

                new Publication()
                {
                    Id = 3,
                    Name = "Information pour les Bénévoles",
                    PublicationType= PublicationTypes.Newsletter,
                    Content = "contenu de la publi",
                    Date = DateTime.Now,
                    Author="Asmma",
                    EmployeeId = 4,
                },

                new Publication()
                {
                    Id = 4,
                    Name = "Les questions les plus posées",
                    PublicationType= PublicationTypes.FAQ,
                    Content = "contenu de la publi",
                    Date = DateTime.Now,
                    Author="Michelle",
                    EmployeeId = 3,
                });
                
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
                //dal.RemoveBenevole(1);

                //dal.CreatePublication("Test creation publi 1", PublicationTypes.Infographie, "contenu de la publi", DateTime.Now, "Auteur 1", 2);
                //dal.EditPublication(5, "Test creation publi 1 MODIF", PublicationTypes.Infographie, "contenu de la publi MODIF", DateTime.Now, "Auteur 1", 2);
                //dal.RemovePublication(5);
            }

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnexionSQL.connexion);
        }


    }


}
