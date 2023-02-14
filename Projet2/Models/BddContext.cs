using Microsoft.EntityFrameworkCore;
using Projet2.Models.Informations;
using Projet2.Models.Messagerie;
using System;
using System.Security.AccessControl;
using System.Xml.Linq;
using  Projet2.Models.UserMessagerie;
using System.Security.Policy;
using Microsoft.Extensions.Configuration;


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
                Name= "Passionnés de jeux vidéo, comment devenir pro-gamer ?",
                PublicationType=PublicationTypes.FAQ,
                Date= "2023-02-13",
                ImagePath= "/images/Gamer-ok.png",
                Content = "Un métier qui nécessite rigueur et détermination\r\n \r\n Il serait illusoire de penser qu’on peut devenir pro-gamer sans travailler dur pour y parvenir. Au même titre que pour les carrières sportives de haut niveau, il est indispensable de fournir beaucoup de rigueur et de détermination pour arriver à ses fins.\r\n \r\n Pour commencer, il est impératif de choisir un jeu spécifique et de passer du temps à s’entraîner dessus (entre 35 et 50 heures hebdomadaires). L’objectif de cet entraînement est de parfaitement maîtriser le jeu et son univers. Ces heures de jeu n’incarnent donc plus un loisir quelconque, mais un travail à temps plein, exigeant une totale concentration de la part du joueur.\r\n \r\n La charge mentale liée à cet entraînement intensif est très élevée. C’est pourquoi le joueur souhaitant devenir pro-gamer doit également veiller à conserver une bonne hygiène de vie (régime alimentaire équilibré, pratique d’activité physique, etc.).\r\n \r\n Contrairement aux idées reçues, le pro-gamer doit également posséder une certaine aisance relationnelle car il est quotidiennement confronté à ses coéquipiers dans le cadre de ses entraînements. Selon le type de jeu dans lequel il souhaite se professionnaliser, le futur pro-gamer doit parfois choisir à quel rôle il va se consacrer au sein sa team (midlaner, bruiser, etc…).\r\n \r\n  \r\n \r\n Quelles sont les qualités d’un pro-gamer ?\r\n \r\n Le pro-gamer doit impérativement être passionné par le jeu dans lequel il est spécialisé ainsi que par l’univers du jeu vidéo en général. Il possède l’esprit de compétition, mais sait aussi se montrer fair-play en cas de défaite. Il doit également posséder un réel esprit d’équipe et savoir s’adapter rapidement aux changements. En effet, certaines modalités des jeux en ligne sont parfois amenées à être modifiées par les équipes de développeurs, ce qui contraint le pro-gamer à revoir certaines de ses stratégies et à redoubler ses entraînements.\r\n \r\n Le fait d’avoir suivi un cursus d’études spécialisé dans l’univers du jeu vidéo incarne un avantage non-négligeable pour devenir pro-gamer. A ce titre, de plus en plus d’établissement ouvrent des formations exclusivement consacrées à l’univers du jeu vidéo et à ses professions : c’est par exemple\r\n \r\n le cas de l’ICAN, qui propose un parcours exclusivement consacré au Game Design. Accessible dès le niveau bac, ce parcours peut être suivi sur un cycle court (le cycle Bachelor, de niveau bac+3), ou un cycle long (le cycle Mastère, de niveau bac +5). Cette formation inclut l’enseignement de nombreuses disciplines fondamentales qui permettent à l’étudiant d’acquérir toutes les connaissances requises pour faire carrière dans l’univers du jeu vidéo : culture anglaise du jeu vidéo, histoire du jeu vidéo, intelligence artificielle….\r\n \r\n Il est difficile de définir le salaire moyen d’un pro-gamer car il varie énormément en fonction de ses différents sponsors, de sa popularité et de son niveau de jeu. Les professionnels les plus populaires au monde jouissent toutefois d’une rémunération colossale : on peut par exemple citer le cas du spécialiste de Fortnite, Nickmercs, qui a gagné plus de 6 millions de dollars en 2019.\r\n \r\n  \r\n \r\n Dans un contexte où le e-sport se démocratise à toute vitesse, il est aujourd’hui parfaitement possible d’envisager de devenir pro-gamer. Très exigeant, ce type de carrière nécessite toutefois un entraînement intensif et une bonne connaissance de l’univers du gaming.",
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
                Name = "Costume emblématique de l'Agent 47",
                PublicationType = PublicationTypes.ArticleInformatif,
                Date = "2023-01-01",
                ImagePath = "/images/agent_47_hitman_costume_by_misteralterego-d5opjei.webp",
                Content = "Le costume emblématique de 47 est un costume disponible dans la trilogie du Monde de l'Assassinat et sert de costume par défaut pour 47.\r\n \r\n DESCRIPTION\r\n Un costume sur-mesure moderne porté avec une chemise blanche avec un col en pointe et une cravate bordeaux.\r\n Acquisition\r\n Costume par défaut automatiquement présent dans l'inventaire.\r\n \r\n ACQUISITION PARTICULIERE\r\n HITMAN™ III\r\n Déguisement de départ sur une destination : il s'agit du costume automatiquement mis lorsqu'on choisit le lieu de départ « Reflet » ou lorsqu'on la lance pour la première fois.\r\n \r\n CARACTERISTIQUE\r\n Déguisemen : Permet de se camoufler dans certains endroits.\r\n \r\n ANECDOTE\r\n Ce costume porte l'adjectif emblématique car c'est le costume qui était souvent utilisé dans les opus précédents notamment Hitman : Tueur à gages, Hitman 2: Silent Assassin et Hitman: Contracts, c'est-à-dire un costume noir, une chemise blanche et une cravate rouge.\r\n On peut voir si les illustrations de ces trois jeux que la cravate est rayée mais apparaît sans cette caractéristique, suggérant qu'il y avait à cette époque des limitations techniques.\r\n Le costume a été légèrement réajusté dans la trilogie.\r\n Dans HITMAN™ 2, la cravate a été éclairée et rendue plus fine.\r\n Dans HITMAN™ III, la cravate a été rendue sensiblement plus sombre, le nœud a été refait et le col ainsi que la cravate ont été rendus plus larges. Le noir du costume a lui aussi été éclairci.\r\n \r\n \r\n ",
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
                Date= "2023 - 12 - 31",
                ImagePath= "/images/ffvii_sephiroth_zack_by_falconfliesalone.webp",
                Content = " Competition Final Fantasy Crisis Core: Zack VS Sephiroth = 1-0 ... ",
                AccountId = 3,
            });;

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
                ImagePath = "/images/ProfileAdmin.png",
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
                Date= "2022-12-31",
                ImagePath= "/images/3195ac2fcaaa1922b1e1f4d4c7472e4b.webp",
                Content = " Et n’oubliez jamais que même si votre barre de vie est proche de zéro, rien n’est jamais perdu, le mode rage peut encore vous permettre de renverser la situation !",
                AccountId = 4,
            });


            //////////////////ADD ACTIVITIES
            ///






            /////////////////////////////
            this.SaveChanges();

        ///////////
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // optionsBuilder.UseMySql(ConnexionSQL.connexion);
			
			if (System.Diagnostics.Debugger.IsAttached)
            {
				optionsBuilder.UseMySql("server=localhost;user id=root;password=" + System.Environment.GetEnvironmentVariable("dbPassword")
            +";database=projet2groupe2");           
			}
            else
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            }

        }
///////////////////////////////////////////////
    }



}
