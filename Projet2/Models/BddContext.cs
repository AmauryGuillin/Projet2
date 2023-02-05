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
        public DbSet<Activity> Activities { get; set; }   
        public DbSet<Adhesion> Adhesions { get; set; }// ajout d'un 's' à la fin
        public DbSet<Adherent> Adherents { get; set; } //ajout d'un 's' à la fin
        public DbSet<AssociationActivity> AssociationActivities { get; set; }
        public DbSet<Benevole> Benevoles { get; set; } //ajout d'un 's' à la fin
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Contribution> Contributions { get; set; }//ajout d'un 's' à la fin
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<InfoPerso> PersonnalInfo { get; set; }
        public DbSet<Planning> Planning { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Profile> Profils { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<SportAssociation> SportAssociation { get; set; }
        public DbSet<Stuff> Stuff { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coaching> Training { get; set; } 
        public DbSet<Stuff> Stuffs { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<VolunteeringActivity> VolunteeringActivities { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }




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

            this.Games.AddRange(
                new Game()
                {
                    Id = 1,
                    GameType = "console",
                    GameList = GameList.Mk
                },
                
                new Game()
                {
                    Id = 2,
                    GameType = "PC",
                    GameList = GameList.Lol
                },

                new Game()
                {
                    Id = 3,
                    GameType = "PC/Console",
                    GameList = GameList.Stf
                }
                );


            this.Stuffs.AddRange(
                new Stuff()
                {
                    Id = 1,
                    Name = "ordinateur",
                    Type = "informatique",
                    State = State.Neuf,
                    ProfileId = null,
                    InventoryId = null
                },
                new Stuff()
                {
                    Id = 2,
                    Name = "casque PS4",
                    Type = "Accessoire PS4",
                    State = State.Acceptable,
                    ProfileId = null,
                    InventoryId = null
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
                    PublicationType = PublicationTypes.ArticleInformatif,
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


            this.Activities.AddRange(
                new Activity()
                {
                    Id = 1,
                    StartDate= DateTime.Now,
                    EndDate= DateTime.Now,
                    SlotID= 1,
                },

                new Activity()
                {
                    Id = 2,
                    StartDate = new DateTime(1998,02,10),
                    EndDate= DateTime.Now,
                    SlotID= 3,
                },

                new Activity()
                {
                    Id = 3,
                    StartDate= DateTime.Now,
                    EndDate= DateTime.Now,
                    SlotID= 2,
                },

                new Activity()
                {
                    Id = 4,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    SlotID = 5,
                },

                new Activity()
                {
                    Id = 5,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    SlotID = 4,
                },

                new Activity()
                {
                    Id = 6,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    SlotID = 8,
                },

                new Activity()
                {
                    Id = 7,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    SlotID = 7,
                },

                new Activity()
                {
                    Id = 8,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    SlotID = 6,
                },

                new Activity()
                {
                    Id = 9,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    SlotID = 9,
                },

                new Activity()
                {
                    Id = 10,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    SlotID = 10,
                });

            this.Slots.AddRange(
                new Slot()
                {
                    Id = 1,
                    Date= DateTime.Now,
                    StartHour= DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 2,
                    Date = DateTime.Now,
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 3,
                    Date = DateTime.Now,
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 4,
                    Date = DateTime.Now,
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 5,
                    Date = new DateTime(1998, 02, 10),
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 6,
                    Date = DateTime.Now,
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 7,
                    Date = DateTime.Now,
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 8,
                    Date = DateTime.Now,
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 9,
                    Date = DateTime.Now,
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                },
                new Slot()
                {
                    Id = 10,
                    Date = DateTime.Now,
                    StartHour = DateTime.Now,
                    EndHour = DateTime.Now
                }
                );

            this.AssociationActivities.AddRange(
                new AssociationActivity()
                {
                    Id = 1,
                    Description="Description 1",
                    Place="Paris",
                    ActivityId= null,
                },

                new AssociationActivity()
                {
                    Id = 2,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = null,
                },

                new AssociationActivity()
                {
                    Id = 3,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = null,
                },

                new AssociationActivity()
                {
                    Id = 4,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = 1,
                },

                new AssociationActivity()
                {
                    Id = 5,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = null,
                },

                new AssociationActivity()
                {
                    Id = 6,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = 2,
                },

                new AssociationActivity()
                {
                    Id = 7,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = null,
                },

                new AssociationActivity()
                {
                    Id = 8,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = null,
                },

                new AssociationActivity()
                {
                    Id = 9,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = 3,
                },

                new AssociationActivity()
                {
                    Id = 10,
                    Description = "Description 1",
                    Place = "Paris",
                    ActivityId = null,
                }

                );

            this.VolunteeringActivities.AddRange(
                new VolunteeringActivity()
                {
                    Id = 1,
                    Name="Name 1",
                    Type="Type 1",
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now,
                    AssociationActivityId=1,
                },

                new VolunteeringActivity()
                {
                    Id = 2,
                    Name = "Name 2",
                    Type = "Type 2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId = 2,
                },

                new VolunteeringActivity()
                {
                    Id = 3,
                    Name = "Name 3",
                    Type = "Type 3",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId = 3,
                },

                new VolunteeringActivity()
                {
                    Id = 4,
                    Name = "Name 4",
                    Type = "Type 4",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId = 4,
                },

                new VolunteeringActivity()
                {
                    Id = 5,
                    Name = "Name 5",
                    Type = "Type 5",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId= 5,
                },

                new VolunteeringActivity()
                {
                    Id = 6,
                    Name = "Name 6",
                    Type = "Type 6",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId = 6,
                },

                new VolunteeringActivity()
                {
                    Id = 7,
                    Name = "Name 7",
                    Type = "Type 7",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId = 7,
                },

                new VolunteeringActivity()
                {
                    Id = 8,
                    Name = "Name 8",
                    Type = "Type 8",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId = 8,
                },

                new VolunteeringActivity()
                {
                    Id = 9,
                    Name = "Name 9",
                    Type = "Type 9",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId = 9,
                },

                new VolunteeringActivity()
                {
                    Id = 10,
                    Name = "Name 10",
                    Type = "Type 10",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AssociationActivityId = 10,
                });

            this.Tournaments.AddRange(
                new Tournament()
                {
                    Id = 1,
                    FinalScore= "1 - 3",
                    Reward="100€",
                    GameId= 1,
                    AssociationActivityId= 1,
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
                //dal.RemoveBenevole(1);

                //dal.CreatePublication("Test creation publi 1", PublicationTypes.Infographie, "contenu de la publi", DateTime.Now, "Auteur 1", 2);
                //dal.EditPublication(5, "Test creation publi 1 MODIF", PublicationTypes.Infographie, "contenu de la publi MODIF", DateTime.Now, "Auteur 1", 2);
                //dal.RemovePublication(5);

                //dal.CreateSlot(DateTime.Now, DateTime.Now, DateTime.Now);
                //dal.EditSlot(11, new DateTime(1998,02,10), DateTime.Now, DateTime.Now);
                //dal.RemoveSlot(11);

                //dal.CreateAssoActivity("Description test", "Chez moi ! :)", 9);
                //dal.EditAssoActivity(11, "Description test MOFID", "Chez moi ! :) MODIF", 5);
                //dal.RemoveAssoActivity(11);

                //dal.CreateVolunteeringActivity("Type 11", "Name 11", DateTime.Now, DateTime.Now, 11);
                //dal.EditVolunteeringActivity(11, "Type 11 MODIF", "Name 11 MODIF", DateTime.Now, DateTime.Now, 11);
                //dal.RemoveVolunteeringActivity(11);
            }

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnexionSQL.connexion);
        }


    }


}
