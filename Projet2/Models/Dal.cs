using MySql.Data.MySqlClient;
using Projet2.Models.Informations;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using Projet2.Models.Messagerie;

using Projet2.Models.UserMessagerie;
using Projet2.Models.Messagerie;
using System.Security.Principal;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Projet2.Models
{
    public class Dal : IDal
    {

        private BddContext _bddContext;

        public Dal()
        {
            _bddContext = new BddContext();
        }
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }


        /////////////////LOGIN
        public Account Authentificate(string username, string password)
        {
            string motDePasse = EncodeMD5(password);
            Account user = this._bddContext.Account.FirstOrDefault(u => u.Username == username && u.Password == motDePasse);
            return user;
        }

        /// <summary>
        /// This method encodes users passwords
        /// </summary>
        /// <returns></returns>
        public static string EncodeMD5(string password)
        {
            string selectedPassword = "UserChoice" + password + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(selectedPassword)));
        }

        public static string DecodeMD5(string password)
        {
            string selectedPassword = "UserChoice" + password + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(selectedPassword)));
        }
        /////////////////ACCOUNT
        ///
        /// <summary>
        /// This method creates an account and the profile associated
        /// </summary>
        /// <param name="id">account's id</param>
        /// <param name="username">account's username</param>
        /// <param name="password">account's password</param>
        /// <param name="password">account's password</param>
        /// /// <param name="profileid">account's associated profile Id </param>
        /// <returns>account.Id</returns>

        //public int CreateAccount(int id, string username, string password)/// Dans le create Account, Profile Id= createProfile();
        //{

        //    Account account = new Account() { Id = id, Username = username, Password = password, ProfileId = CreateProfile(), InventoryId =  CreateInventory()};


        //    _bddContext.Account.Add(account);
        //    _bddContext.SaveChanges();

        //    return account.Id;

        //}
        /// <summary>
        /// This method returns a list that contains all accounts
        /// </summary>
        /// <returns></returns>

       
        public Account GetAccount(int id)
        {
            return this._bddContext.Account.Find(id);
        }

        public Account GetAccount(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetAccount(id);
            }
            return null;
        }

       
       
        public List<Account> GetAccounts()
        {
            return _bddContext.Account.ToList();
        }

        /// <summary>
        /// This method adds a user's account in the database while encoding the user's password
        /// </summary>
        /// <returns></returns>
        public Account AddAccount(string username, string password,int contactId,int infopersoId,int profileId,Role role,int MessagerieId)
        {
            string wordpass = EncodeMD5(password);
            //idProfile= CreateProfile();
            Account account = new Account() {
                Username = username,
                Password = wordpass,
                ProfileId = profileId,
                ContactId = contactId,
                InfoPersoId = infopersoId,
                Inventory = new Inventory(),
                Planning = AddPlanning(username),
                role = role,
                MessagerieId = MessagerieId
               
            };
            this._bddContext.Account.Add(account);
            this._bddContext.SaveChanges();
            return account;
        }



      

        /////////////////ACTIVITY

        public Activity CreateNewActivity(DateTime startDate, DateTime endDate, string description, string place, ActivityType activityType,EventType eventType,string organizer,string filepath,int accountId, int nb)
        {
            Activity activity = new Activity() { 
                StartDate = startDate, 
                EndDate = endDate,
                Description=description,
                Place=place, 
                activityType=activityType,
                ActivityEventType=eventType,
                Organizer=organizer,
                ImagePath=filepath,
                PublisherId=accountId,
                NumberOfParticipants=nb,
            };
            _bddContext.Activities.Add(activity);
            _bddContext.SaveChanges();

            return activity;
        }

     

        public void EditActivity(int id, DateTime startDate, DateTime endDate, string theme,string description,string Place, string imagePath, int nb)
        {
            Activity activity = _bddContext.Activities.Find(id);
            if (activity != null)
            {
                activity.StartDate = startDate;
                activity.EndDate = endDate;
                activity.Theme = theme;
                
                activity.Description = description;
                activity.Place = Place;
                activity.ImagePath = imagePath;
                activity.NumberOfParticipants = nb;
                activity.ImagePath = imagePath;


                _bddContext.SaveChanges();
            }

        }
       
        public void RemoveActivity(int id)
        {
            Activity activity = _bddContext.Activities.Find(id);
            if (activity != null)
            {
                _bddContext.Activities.Remove(activity);

                _bddContext.SaveChanges();
            }
        }

        public List<Activity> GetActivities()
        {
            return _bddContext.Activities.ToList();
        }

        /////////////////ADHERENT

        public Adherent AddAdherent(int accountid,int benevoleid,int adhesion,int contributionId,string docs)
        {
            
            Adherent adherent = new Adherent()
            {
                AccountId = accountid,
                BenevoleId = benevoleid,
                AdhesionId= adhesion,
                Contribution = contributionId,
                DocPath = docs
            };
            this._bddContext.Adherents.Add(adherent);
            this._bddContext.SaveChanges();
            return adherent;
        }

        /// <summary>
        /// This method creates an Adherent in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="benevoleId"></param>
        /// <param name="numAdherent"></param>
        /// <param name="inscriptiondate"></param>
        /// <param name="contribution"></param>
        /// <param name="idDocuments"></param>
        /// <param name="teamId"></param>
        /// <param name="adhesionId"></param>
        /// <param name="coachingId"></param>
        /// <returns>adherent.Id</returns>
        public int CreateAdherent( int benevoleId, int numAdherent, DateTime inscriptiondate, Double contribution, string idDocuments, int teamId, int adhesionId, int coachingId)
        {
            Adherent adherent = new Adherent()
            {
                BenevoleId = benevoleId,
                NumAdherent = numAdherent,
                InscriptionDate = inscriptiondate,
                Contribution = contribution,
                DocPath = idDocuments,
                AdhesionId = adhesionId,
               
            };

            _bddContext.Adherents.Add(adherent);

            _bddContext.SaveChanges();
            return adherent.Id;

        }
       
       

        /// <summary>
        /// This method returns a list that contains all adherents
        /// </summary>
        /// <returns>_bddContext.Adherents.ToList()</returns>

        public List<Adherent> GetAdherents()
        {
            return _bddContext.Adherents.ToList();
        }

        /// <summary>
        /// This method modifies an Adherent in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="benevoleId"></param>
        /// <param name="numAdherent"></param>
        /// <param name="inscriptiondate"></param>
        /// <param name="idDocuments"></param>
        public Adherent EditAdherent(int id,string docsPath)
        {
            Adherent adherent = _bddContext.Adherents.Find(id);
            if (adherent != null)
            {
                adherent.DocPath = docsPath;
                
                _bddContext.SaveChanges();
                return adherent;
            }
            return null;
        }

        

        

        /// <summary>
        /// This method removes an Adherent in the SQL database with an Adherent
        /// </summary>
        /// <param name="adherent"></param>
        public void RemoveAdherent(Adherent adherent)
        {
            _bddContext.Adherents.Remove(adherent);
            _bddContext.SaveChanges();
        }


        /////////////ADHESION
        ///


        /// <summary>
        /// This method creates an Adhesion in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contributionId"></param>
        /// <param name="Echeance"></param>
        /// <param name="adhesionStatus"></param>
        /// <returns>adhesion.Id</returns>
       
        public Adhesion CreateNewAdhesion(int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus)
        {
            Adhesion adhesion = new Adhesion()
            {
             ContributionId= contributionId,
             Echeance= DateTime.Now,
             AdhesionStatus= AdhesionStatus.EnCours };
            _bddContext.Adhesions.Add(adhesion);
            _bddContext.SaveChanges();
            return adhesion;
        }

       

        /// <summary>
        /// This method returns a list that contains all adhesions
        /// </summary>
        /// <returns></returns>
        public List<Adhesion> GetAdhesions()
        {
            return _bddContext.Adhesions.ToList();
        }

        

       

        

        /// <summary>
        /// This method removes an Adhesion in the SQL database with an Adhesion
        /// </summary>
        /// <param name="adhesion"></param>
        public void RemoveAdhesion(Adhesion adhesion)
        {
            _bddContext.Adhesions.Remove(adhesion);
            _bddContext.SaveChanges();
        }

        /////////////////BENEVOLE


        /// <summary>
        /// This method creates a Benevole in the SQL database with a benevole
        /// </summary>
        /// <param name="benevole"></param>

        public Benevole CreateNewBenevole(int accountId)

        {
            int nbActionVolunteering = 0;
            Benevole benevole = new Benevole() { 
                AccountId = accountId, 
                NbActionVolunteering = nbActionVolunteering 
            };

            _bddContext.Benevoles.Add(benevole);

            _bddContext.SaveChanges();

            return benevole;
        }

       

        /// <summary>
        /// This method returns a list that contains all benevoles
        /// </summary>
        /// <returns></returns>

        public List<Benevole> GetBenevoles()
        {
            return _bddContext.Benevoles.ToList();
        }


        


        /// <summary>
        /// This method removes a Benevole int the SQL database with a Benevole
        /// </summary>
        /// <param name="benevole"></param>
        public void RemoveBenevole(Benevole benevole)
        {
            _bddContext.Benevoles.Remove(benevole);
            _bddContext.SaveChanges();
        }


        /////////////////CHAT && CONVERSATIONS
        ///

        public MessagerieA AddMessagerie()
        {
           MessagerieA messagerie = new MessagerieA()
            {
                NbConversations = 0
            };
            this._bddContext.Messageries.Add(messagerie);
            this._bddContext.SaveChanges();
            return messagerie;
        }

        public List<MessagerieA> GetMessageries()
        {
            return _bddContext.Messageries.ToList();
        }

        public List<Message> GetMessages()
        {
            return _bddContext.Messages.ToList();
        }

        public List<Conversation> GetConversations()
        {
            return _bddContext.Conversations.ToList();
        }

       

        public List<Conversation> GetUserConversationsStarter(int account1)
        {
           List <Conversation>ListConversationsStarter = new List<Conversation>();

            ListConversationsStarter =GetConversations().Where(r => r.FirstSenderId == account1).ToList();
            return ListConversationsStarter;
        }

        public List<Conversation> GetUserConversationsReplier(int account1)
        {
            List<Conversation> ListConversationsReplier = new List<Conversation>();
            ListConversationsReplier = GetConversations().Where(r => r.ReceiverId== account1).ToList();
            //LinkedList<Conversation> TotalConversations= new LinkedList<Conversation>();
            return ListConversationsReplier;
        }
        
        public Conversation CreateConversation(int accountIdSender, int accountReceiver)
        {
            Conversation conversation = new Conversation()
            {
                FirstSenderId = accountIdSender,
                ReceiverId = accountReceiver,
                SenderAccount= GetAccount(accountIdSender),
                ReceiverAccount=GetAccount(accountReceiver)
              
               
            };
            this._bddContext.Conversations.Add(conversation);
            this._bddContext.SaveChanges();
            return conversation;
        }

        public Message FirstMessage(int conversationId, int account1, int account2, string body)
        {
            Message firstMessage = new Message()
            {
                Body = body,
                ConversationId = conversationId,
                SenderId = account1,
                ReceiverId = account2,
                MessageTimeStamp = "15/02/2023",
                isRead = false,
                
            };
            this._bddContext.Messages.Add(firstMessage);
            this._bddContext.SaveChanges();
            return firstMessage;
        }

        public Message MessageReply(int conversationId,int account1, int account2, string body)
        {
            Message NewMessage = new Message()
            { 
                Body = body,
                ConversationId = conversationId,
                SenderId = account1,
                ReceiverId = account2,
                MessageTimeStamp = "15/02/2023",
                isRead = false,
            };
            //List<Message> conversation = new List<Message>();
            GetUserConversationsStarter(account1);
            GetUserConversationsReplier(account1);
            //if (GetUserConversationsStarter(account1).Contains(GetThisConversation(conversationId))) { 

            //}
            this._bddContext.Messages.Add(NewMessage);
            this._bddContext.SaveChanges();

            return NewMessage;
        }


        /////////////////COACHING

        //////////////COMMENT

        ////////////////////////////////////INFOPERSO (CONTACT)

        public Contact AddContact(string email, string Tel)

        {
            Contact contact = new Contact() {EmailAdress = email, TelephoneNumber = Tel };

            _bddContext.Contact.Add(contact);

            _bddContext.SaveChanges();

            return contact;
        }
        
        public int EditContact(Contact contact)
        {
            _bddContext.Contact.Update(contact);
            _bddContext.SaveChanges();
            return contact.Id;
        }
        public Contact GetContact(int id)
        {
            return this._bddContext.Contact.Find(id);
        }
       

        public List<Contact> GetContacts()
        {
            return _bddContext.Contact.ToList();
        }


       

        public void EditPublication(int id, string name, PublicationTypes type, string content, string date, string imagePath)
        {
            Publication publi = _bddContext.Publications.Find(id);
            if (publi != null)
            {
                publi.Name = name;
                publi.PublicationType = type;
                publi.Content = content;
                publi.Date = date;
                publi.ImagePath = imagePath;
                _bddContext.SaveChanges();
            }
        }

        /////////////////CONTRIBUTION

        /// <summary>
        /// This method creates an Contribution in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="totalCount"></param>
        /// <param name="prelevementDate"></param>
        /// <param name="contributionType"></param>
        /// <returns>contribution.Id</returns>
        public Contribution CreateNewContribution(string rib)
        {
            Contribution contribution = new Contribution()
            {
                RIB= rib,
            };
            _bddContext.Contributions.Add(contribution);
            _bddContext.SaveChanges();
            return contribution;


        }
    

        
       
        /// <summary>
        /// This method returns a list that contains all contributions
        /// </summary>
        /// <returns></returns>
        public List<Contribution> GetContributions()
        {
            return _bddContext.Contributions.ToList();
        }

        
        
        /// <summary>
        /// This method removes a contribution in the SQL database with a contribution
        /// </summary>
        /// <param name="contribution"></param>
        public void RemoveContribution(Contribution contribution)
        {
            _bddContext.Contributions.Remove(contribution);
            _bddContext.SaveChanges();
        }

        /////////////////CONTRIBUTION



        /////////////////EMPLOYEE
        ///


        public List<Employee> GetEmployees()
        {
            return _bddContext.Employees.ToList();
        }
       
        public Profile CreateProfileEmployee(string bio, string games)
        {
            Profile profile = new Profile() { Bio= bio, Games = games };
            _bddContext.Profils.Add(profile);
            _bddContext.SaveChanges();
            return profile;
        }

        public Employee CreateEmployee(int accountId, string jobname, string matricule)
        {
            Employee employee = new Employee()
            {
                AccountId= accountId,
                DateOfEmployement= DateTime.Now.ToString("dd/mm/yyyy"),
                JobName=jobname,
                SerialNumber=matricule
            };

            _bddContext.Employees.Add(employee);
            _bddContext.SaveChanges();
            return employee;
        }

       

        public void RemoveEmployee(Employee employee)
        {
            _bddContext.Employees.Remove(employee);
            _bddContext.SaveChanges();
        }

        /////////////////EVENT


        /////////////////INVENTORY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stuffs"></param>
        /// <returns></returns>
       
        public int CreateInventory()
        {

            Inventory inventory = Inventory.CreateInventory();
            _bddContext.Inventory.Add(inventory);
            _bddContext.SaveChanges();
            return inventory.Id;
        }
        public int EditInventory(Inventory inventory)
        {
            _bddContext.Inventory.Update(inventory);
            _bddContext.SaveChanges();
            return inventory.Id;
        }
        public Inventory GetInventory(int id)
        {
            return this._bddContext.Inventory.Find(id);
        }
       
        public List<Inventory> GetInventories()
        {
            return _bddContext.Inventory.ToList();
        }
        
        

        public List<Stuff> GetOwnedStuff(int accountid)
        {
            
            List<Stuff> stuffOwned = _bddContext.Stuffs.Where(s => s.AccountOwnerId== accountid).ToList();

            return stuffOwned;


        }



        ///////////////////INFOPERSO
        ///
        
        public InfoPerso AddInfoPerso(string firstname,string lastame,string dob)
        {
            InfoPerso infoPerso = new InfoPerso() { FirstName = firstname, LastName = lastame,Birthday =dob};

            _bddContext.PersonnalInfo.Add(infoPerso);

            _bddContext.SaveChanges();

            return infoPerso;
        }
        
        public int EditInfos(InfoPerso infos)
        {
            _bddContext.PersonnalInfo.Update(infos);
            _bddContext.SaveChanges();
            return infos.Id;
        }
        public InfoPerso GetInfos(int id)
        {
            return this._bddContext.PersonnalInfo.Find(id);
        }
        
        public List<InfoPerso> GetInformations()
        {
            return _bddContext.PersonnalInfo.ToList();
        }


        /////////////////MESSAGE

        /////////////////PLANNING
        ///
        public Planning AddPlanning(string name)
        {
            Planning planning = new Planning()
            {
                Name= name,
            };
            this._bddContext.Planning.Add(planning);
            this._bddContext.SaveChanges();
            return planning;
        }

        public List<Planning> GetPlannings()
        {
            return _bddContext.Planning.ToList();
        }

        public void AddSlotToPlanning(int accountId, Slot slot)
        {
            Account account=GetAccount(accountId);
           Planning planning= GetPlannings().Where(r => r.Id == account.PlanningId).FirstOrDefault();
            slot.PlanningId = planning.Id;
            this._bddContext.Slots.UpdateRange();
            this._bddContext.SaveChanges();
           
        }


        /////////////////POST
        ///


        /////////////////PUBLICATION

        /// <summary>
        /// This methods is used to create a publication into the SQL database.
        /// </summary>
        /// <param name="name">Publication's name</param>
        /// <param name="publicationType">Publication's type (chosed by the enumeration associated)</param>
        /// <param name="content">Do I have to precise this ? :D </param>
        /// <param name="creationdate">Publication's creation date</param>
        /// <param name="author">Publication's author</param>
        /// <param name="employeId">Employee who created the publication</param>
        /// <returns></returns>


        public Publication CreatePublication(string profilImage, int authorid,string body,string name,PublicationTypes publicationTypes, string date)
        {
            Publication publi = new Publication()
            {
                ImagePath = profilImage,
                AccountId= authorid,
                Content=body,
                Date= date,
                Name= name,
                PublicationType= publicationTypes

                
            };

            this._bddContext.Publications.Add(publi);
            this._bddContext.SaveChanges();
            return publi;
        }

     


        public void RemovePublication(int id)

        {
            Publication publication = _bddContext.Publications.Find(id);
            if (publication != null)
            {
                _bddContext.Publications.Remove(publication);
                _bddContext.SaveChanges();
            }
        }




        public List<Publication> GetPublications()
        {
            return _bddContext.Publications.ToList();
        }
        public Publication GetOnePublication(int id)
        {
            Publication publication = _bddContext.Publications.Find(id);
            return publication;
        }


        /////////////////PROFILE

    
        public Profile AddProfile(string profilImage, string Bio, string games)
        {
            Profile profile = new Profile()
            {
                ImagePath = profilImage,
                Bio = Bio,
                Games = games
            };
            this._bddContext.Profils.Add(profile);
            this._bddContext.SaveChanges();
            return profile;
        }


        /// <summary>
        /// This method creates an empty profile returns a profile id 
        /// </summary>
        /// <returns></returns>
        /// 

        public int CreateProfile()
        {
            Profile profile = Profile.CreateProfile();
            _bddContext.Profils.Add(profile);
            _bddContext.SaveChanges();
            return profile.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Profile GetProfile(int id)
        {
            return this._bddContext.Profils.Find(id);

        }
        public Profile GetProfile(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetProfile(id);
            }
            return null;
        }
        public List<Profile> GetProfiles()
        {
            return _bddContext.Profils.ToList();
        }
        
        public int EditProfile(Profile profile)
        {
            _bddContext.Profils.Update(profile);
            _bddContext.SaveChanges();
            return profile.Id;
        }

        /////////////////RESERVATION STUFF

        public ReservationStuff CreateReservationStuff(ReservationStuff reservation)
        {
            _bddContext.ReservationsStuffs.Add(reservation);
            _bddContext.SaveChanges();
            return reservation;
        }

        public List<ReservationStuff> GetReservations()
        {
            return _bddContext.ReservationsStuffs.ToList();
        }





        /////////////////SLOTS

        public Slot CreateSlot( DateTime startHour, DateTime endHour,int ActivityId,int PlanningId)
        {
            Slot slot = new Slot()
            {
                
                StartHour = startHour,
                EndHour = endHour,
                ActivityId = ActivityId,
                PlanningId = PlanningId

            };

            _bddContext.Slots.Add(slot);
            _bddContext.SaveChanges();
            return slot;
        }

        public void EditSlot(int id, DateTime startHour, DateTime endHour)
        {
            Slot slot = _bddContext.Slots.Find(id);
            if (slot != null)
            {
                slot.Id = id;
                
                slot.StartHour = startHour;
                slot.EndHour = endHour;
                _bddContext.SaveChanges();
            }
        }

      

        public void RemoveSlot(int id)
        {
            Slot slot = _bddContext.Slots.Find(id);

            if (slot != null)
            {
                _bddContext.Slots.Remove(slot);
                _bddContext.SaveChanges();
            }
        }

        public List<Slot> GetSlots()
        {
            return _bddContext.Slots.ToList();
        }

      
 public void RemoveSlot(Slot slot)
        {
            _bddContext.Slots.Remove(slot);
            _bddContext.SaveChanges();
        }

        /////////////////STUFF



        public Stuff CreateStuff(string profilImage, int accountOwnerId, string name, string description, Type type, State state)
        {
            Stuff stuff = new Stuff()
            {
                ImagePath = profilImage,
                Reservation = Reservation.libre,
                AccountOwnerId = accountOwnerId,
                Name=name,
                Description=description,
                Type = type,
                    State = state
            };
       
            this._bddContext.Stuffs.Add(stuff);
            this._bddContext.SaveChanges();
            return stuff;
        }
        public Stuff GetOneStuff(int id)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
            return stuff;
        }

        public List<Stuff> GetStuffs()
        {
            return _bddContext.Stuffs.ToList();
        }

      

        public void EditStuffReservation(int id, int accountBorrowerId)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
            if (stuff != null)
            {
                stuff.Reservation = Reservation.enAttente;
                stuff.AccountBorrowerId = accountBorrowerId;
                _bddContext.SaveChanges();
            }
        }

        public void EditStuffAcceptation(int id)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
            if (stuff != null)
            {
                stuff.Reservation = Reservation.reserve;
                _bddContext.SaveChanges();
            }
        }

        public void EditStuffCancelation(int id)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
            if (stuff != null)
            {
                stuff.AccountBorrowerId = null;
                stuff.Reservation = Reservation.libre;
                _bddContext.SaveChanges();
            }
        }




        public void EditStuff(int id, string name, string description,Type type, State state)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
            if (stuff != null)
            {
                stuff.Name= name;
                stuff.Description= description;
                stuff.Type= type;
                stuff.State= state;
                _bddContext.SaveChanges();
            }
        }


        public void RemoveStuff(int id)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
        if (stuff != null)
          {
            _bddContext.Stuffs.Remove(stuff);
            _bddContext.SaveChanges();
            }
        }   


        public void RemoveStuff(Stuff stuff)
        {
            _bddContext.Stuffs.Remove(stuff);
            _bddContext.SaveChanges();
        }

     
        ////////////////REMOVE
        ///

        public void RemovePlanning(Planning planning)
        {
            _bddContext.Planning.Remove(planning);
            _bddContext.SaveChanges();
        }

        public void RemoveContact(Contact contact)
        {
            _bddContext.Contact.Remove(contact);
            _bddContext.SaveChanges();
        }

        public void RemoveInfos(InfoPerso infos)
        {
            _bddContext.PersonnalInfo.Remove(infos);
            _bddContext.SaveChanges();
        }
        public void RemoveConversation(Conversation conversation)
        {
            _bddContext.Conversations.Remove(conversation);
            _bddContext.SaveChanges();
        }

        public void RemoveMessages (List<Message> messages,int accountId)
        {
            messages = _bddContext.Messages.Where(r => r.SenderId == accountId).ToList();
            _bddContext.Messages.RemoveRange(messages);
            _bddContext.SaveChanges();
        }
        public void RemoveMessagerie(MessagerieA messagerie)
        {
            _bddContext.Messageries.Remove(messagerie);
            _bddContext.SaveChanges();
        }
        public void RemoveProfile(Profile profile)
        {
            _bddContext.Profils.Remove(profile);
            _bddContext.SaveChanges();
        }
        public void RemoveAccount(Account Account)
        {
            _bddContext.Account.Remove(Account);
            _bddContext.SaveChanges();
        }


        public void RemoveActivities(Activity activity)
        {
            _bddContext.Activities.Remove(activity);
            _bddContext.SaveChanges();
        }

        public Profile EditProfileS(int id,string path,string games,string Bio )
        {
            Profile profile = _bddContext.Profils.Find(id);
            if (profile != null)
            {
                profile.ImagePath = path;
                profile.Games = games;
                profile.Bio = Bio;
                
                _bddContext.SaveChanges();
                return profile;
            }
            return null;
        }

        public Contact EditContacts( int id, string email,string tel)
        {
            Contact contact = _bddContext.Contact.Find(id);
            if (contact != null)
            {
                contact.EmailAdress = email;
                contact.TelephoneNumber = tel;
                

                _bddContext.SaveChanges();
                return contact;
            }
            return null;
        }

        public Account EditAccount(int id,string username, string password)
        {
            Account account = _bddContext.Account.Find(id);
            if (account != null)
            {
                account.Username = username;
                    account.Password=password; ;
                _bddContext.SaveChanges();
                return account;
            
            }
            return null;
        }



        //////////////////TND

    }
}
