using Microsoft.EntityFrameworkCore;
using Projet2.Models.Informations;
using Projet2.Models.Messagerie;
using System;
using System.Security.AccessControl;
using System.Xml.Linq;
using  Projet2.Models.UserMessagerie;
using System.Security.Policy;

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
        public DbSet<MessagerieA> Messageries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Contribution> Contributions { get; set; }//ajout d'un 's' à la fin
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Admin> Admins { get; set; }
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

        public DbSet<Team> Teams { get; set; }
        public DbSet<Coaching> Training { get; set; } 
        public DbSet<Stuff> Stuffs { get; set; }

        public DbSet<ReservationStuff> ReservationsStuffs { get; set; }


        public DbSet<Publication> Publications { get; set; }
        public DbSet<VolunteeringActivity> VolunteeringActivities { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Event> Events { get; set; }




        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            ///BENEVOLE
            this.Benevoles.Add(new Benevole() { Id=1,AccountId=1,NbActionVolunteering=2});
            this.Account.Add(new Models.Account()
            {
                Id=1,
                Username="Benevole",
                Password= "AC-35-7E-4A-EA-CC-F4-8C-70-AB-7D-59-12-69-02-FA",// 123
                InfoPersoId=1,
                ContactId=1,
                PlanningId=1,
                ProfileId=1,
                MessagerieId=1,
                role= Role.Benevole
            });
            this.Profils.Add(new Profile()
            {
                Id=1,
                ImagePath= "/images/angrycat1.jpg",
                Bio="Je suis un benevole",
                Games="Candy Crush",

            });
            this.Contact.Add(new Informations.Contact()
            {
                Id=1,
                EmailAdress="benevole@gmail.com",
                TelephoneNumber="0505050505"
            });
            this.PersonnalInfo.Add(new InfoPerso()
            {
                Id=1,
                LastName="Vole",
                FirstName="Bene",
                Birthday="02-11-2002"
            });
            this.Messageries.Add(new MessagerieA()
            {
                Id=1,
                NbConversations=0,
            });
            this.Planning.Add(new Models.Planning
            {
                Id = 1,
                Name = "Le planning de Bene Vole",
                nbSlots = 0
            }) ;
            this.Stuffs.Add(new Stuff()
            {
                Id=1,
                Name="poudre de licorne",
                ImagePath= "/images/angrycat1.jpg",
                Description= "La poudre de licorne est un produit fictif qui est souvent associé à la magie et à l'imagination. On dit que la poudre de licorne a des propriétés magiques uniques, telles que la capacité de guérir les blessures, d'accorder des souhaits et de renforcer les pouvoirs magiques. Cependant, il est important de noter que la poudre de licorne n'existe pas dans la réalité et que toutes les allégations à son sujet sont purement fictives.",
                Type=Type.AccessoireBureau,
                State=State.Neuf,
                Reservation=Reservation.libre,
                AccountOwnerId=1,


            });
            this.Publications.Add(new Publication()
            {
                Id=1,
                Name="Un truc chiant.....",
                PublicationType=PublicationTypes.FAQ,
                Content= "puceau moi ? serieusement ^^ haha on me l avait pas sortie celle la depuis loooongtemps  demande a mes potes si je suis puceau tu vas voir les reponses que tu vas te prendre XD rien que la semaine passee j ai niquer donc chuuuuut ferme la puceau de merde car oui toi tu m as tout l air d un bon puceau de merde car souvent vous etes frustrer de ne pas BAISER  ses agreable de se faire un missionnaire ou un amazone avec une meuf hein? tu peux pas repondre car tu ne sais pas ce que c ou alors tu le sais mais tu as du taper dans ta barre de recherche \"missionnaire sexe\" ou \"amazone sexe\" pour comprendre ce que c etait mdddrrr !! c est grave quoiquil en soit.... pour revenir a moi, je pense que je suis le mec le moins puceau de ma bande de 11 meilleurs amis pas psk j ai eu le plus de rapport intime mais psk j ai eu les plus jolie femme que mes amis ses pas moi qui le dit, ses eux qui commente sous mes photos insta \"trop belle la fille que tu as coucher avec hier en boite notamment!\" donc apres si tu veux que sa parte plus loi sa peut partir vraiment loi j habite dans la banlieue de niort sa te parle steven sanchez ? ses juste un cousin donc OKLM hahaha on verra si tu parles encore le puceau de merde mdddrrr pk insulter qd on est soi meme puceau tu me feras toujour marrer!! ",
                AccountId=1,
            });
            // this.Activities.Add();

            //ADHERENT

                
            this.SaveChanges();

        ///////////
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnexionSQL.connexion);
        }
///////////////////////////////////////////////
    }



}
