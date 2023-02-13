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
                Username="Wario",
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
                ImagePath= "/images/ProfileBenevole.png",
                Bio="Je suis un benevole",
                Games="Mario",

            });
            this.Contact.Add(new Informations.Contact()
            {
                Id=1,
                EmailAdress="wario@gmail.com",
                TelephoneNumber="0505050505"
            });
            this.PersonnalInfo.Add(new InfoPerso()
            {
                Id=1,
                LastName= "Nintendo",
                FirstName="Wario",
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



            //ADHERENT
            this.Contributions.Add(new Contribution() { Id=1,
            ContributionType=ContributionType.Trimestriel,
            PrelevementDate=PrelevementDate.CinqDuMois,
            RIB="FR1234567891011",
            PaymentStatus=true,
            
            });
            this.Adhesions.Add(new Adhesion()
            {  
                Id=1,
                AdhesionStatus=AdhesionStatus.Verfie,
                ContributionId=1,
                
            });
            this.Adherents.Add(new Adherent() { Id=1,AccountId=2,BenevoleId=2,AdhesionId=1,NumAdherent=640509040147,Contribution=350,DocPath="/AdherentsDocuments/DocIdExemple"});
            this.Benevoles.Add(new Benevole() { Id = 2, AccountId = 2, NbActionVolunteering = 12 });
            this.Account.Add(new Models.Account()
            {
                Id = 2,
                Username = "Agent47",
                Password = "AC-35-7E-4A-EA-CC-F4-8C-70-AB-7D-59-12-69-02-FA",// 123
                InfoPersoId = 2,
                ContactId = 2,
                PlanningId = 2,
                ProfileId = 2,
                MessagerieId = 2,
                role = Role.Adherent
            });
            this.Profils.Add(new Profile()
            {
                Id = 2,
                ImagePath = "/images/ProfilAdherent.jpg",
                Bio = "Je suis un adherent",
                Games = "Hitman",

            });
            this.Contact.Add(new Informations.Contact()
            {
                Id = 2,
                EmailAdress = "agent47@gmail.com",
                TelephoneNumber = "0505050505"
            });
            this.PersonnalInfo.Add(new InfoPerso()
            {
                Id = 2,
                LastName = "Rieper",
                FirstName = "Tobias",
                Birthday = "05-09-1964"
            });
            this.Messageries.Add(new MessagerieA()
            {
                Id = 2,
                NbConversations = 0,
            });
            this.Planning.Add(new Models.Planning
            {
                Id = 2,
                Name = "Le planning de Bene Vole",
                nbSlots = 0
            });
            this.Stuffs.Add(new Stuff()
            {
                Id = 2,
                Name = "Deguisement agent secret",
                ImagePath = "/images/CostumeItem.png",
                Description = "Un costume sur-mesure moderne porté avec une chemise blanche avec un col en pointe et une cravate bordeaux.",
                Type = Type.AccessoireBureau,
                State = State.Neuf,
                Reservation = Reservation.libre,
                AccountOwnerId = 2,


            });
            this.Publications.Add(new Publication()
            {
                Id = 2,
                Name = "Infos",
                PublicationType = PublicationTypes.ArticleInformatif,
                Content = "Ce costume porte l'adjectif emblématique car c'est le costume qui était souvent utilisé dans les opus précédents notamment Hitman : Tueur à gages, Hitman 2: Silent Assassin et Hitman: Contracts, c'est-à-dire un costume noir, une chemise blanche et une cravate rouge.\r\nOn peut voir si les illustrations de ces trois jeux que la cravate est rayée mais apparaît sans cette caractéristique, suggérant qu'il y avait à cette époque des limitations techniques. ",
                AccountId = 2,
            });

            ///////////////////////////EMPLOYEE

            this.Employees.Add(new Employee() { AccountId=3,JobName="Coach Experimenté",SerialNumber="EMP2394"});
            this.Account.Add(new Models.Account()
            {
                Id = 3,
                Username = "Sephiroth",
                Password = "AC-35-7E-4A-EA-CC-F4-8C-70-AB-7D-59-12-69-02-FA",// 123
                InfoPersoId = 3,
                ContactId = 3,
                PlanningId = 3,
                ProfileId = 3,
                MessagerieId = 3,
                role = Role.Salarie
            });
            this.Profils.Add(new Profile()
            {
                Id = 3,
                ImagePath = "/images/ProfilSalarie.jpg",
                Bio = "J'ai toujours su, dès l'enfance... Je n'étais pas comme les autres. J'ai toujours su que j'étais différent. Mais je... Mais je n'aurais jamais imaginé cela. Suis-je... un être humain ? ",
                Games = "Final Fantasy VII",

            });
            this.Contact.Add(new Informations.Contact()
            {
                Id = 3,
                EmailAdress = "Sephiroth@gmail.com",
                TelephoneNumber = "0505050505"
            });
            this.PersonnalInfo.Add(new InfoPerso()
            {
                Id = 3,
                LastName = "Sephiroth",
                FirstName = "Safer",
                Birthday = "05-04-2001"
            });
            this.Messageries.Add(new MessagerieA()
            {
                Id = 3,
                NbConversations = 0,
            });
            this.Planning.Add(new Models.Planning
            {
                Id = 3,
                Name = "Le planning du meillur antagoniste",
                nbSlots = 0
            });
            this.Stuffs.Add(new Stuff()
            {
                Id = 3,
                Name = "Magnifique Katana Masamune",
                ImagePath = "/images/SwordItem.png",
                Description = "Une lame en acier inoxydable 440 stainless steel,Fourreau en bois noir, Longueur du manche:39cm - Longueur de la lame:99cm - Longueur total:140cm",
                Type = Type.PeripheriquePC,
                State = State.Neuf,
                Reservation = Reservation.libre,
                AccountOwnerId = 3,


            });
            this.Publications.Add(new Publication()
            {
                Id = 3,
                Name = "FFVII",
                PublicationType = PublicationTypes.Resultat,
                Content = " Competition Final Fantasy Crisis Core: Zack VS Sephiroth = 1-0 ... ",
                AccountId = 3,
            });

            ///////////////////////ADMIN
            this.Admins.Add(new Admin() { AccountId = 4 });
            this.Account.Add(new Models.Account()
            {
                Id = 4,
                Username = "Heihachi",
                Password = "AC-35-7E-4A-EA-CC-F4-8C-70-AB-7D-59-12-69-02-FA",// 123
                InfoPersoId = 4,
                ContactId = 4,
                PlanningId = 4,
                ProfileId = 4,
                MessagerieId = 4,
                role = Role.Admin
            });
            this.Profils.Add(new Profile()
            {
                Id = 4,
                ImagePath = "/images/ProfileAdmin",
                Bio = " Chef de la Mishima Zaibatsu , j'ai crée les tournois de King of Iron Rist. Voyons comment vous allez vous en tirer face à mon poing d'acier !",
                Games = "Final Fantasy VII",

            });
            this.Contact.Add(new Informations.Contact()
            {
                Id = 4,
                EmailAdress = "Heihachi@gmail.com",
                TelephoneNumber = "0505050505"
            });
            this.PersonnalInfo.Add(new InfoPerso()
            {
                Id = 4,
                LastName = "Michima",
                FirstName = "Heihachi",
                Birthday = "30-04-1948"
            });
            this.Messageries.Add(new MessagerieA()
            {
                Id = 4,
                NbConversations = 0,
            });
            this.Planning.Add(new Models.Planning
            {
                Id = 4,
                Name = "Le planning du boss",
                nbSlots = 0
            });
            this.Stuffs.Add(new Stuff()
            {
                Id = 4,
                Name = " keikogi de Karaté",
                ImagePath = "/images/FitItem.png",
                Description = "Magnifique Keikogi entièrement noir. Dans le dos du vêtement, on retrouve une tête de tigre ",
                Type = Type.PeripheriqueConsole,
                State = State.Acceptable,
                Reservation = Reservation.libre,
                AccountOwnerId = 4,


            });
            this.Publications.Add(new Publication()
            {
                Id = 4,
                Name = "Techniques de combat.",
                PublicationType = PublicationTypes.Infographie,
                Content = " Et n’oubliez jamais que même si votre barre de vie est proche de zéro, rien n’est jamais perdu, le mode rage peut encore vous permettre de renverser la situation !",
                AccountId = 4,
            });





            /////////////////////////////
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
