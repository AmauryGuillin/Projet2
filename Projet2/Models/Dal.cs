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
    /// <summary>
    /// This class implements the IDal interface to provide data persistence operations for the application.
    /// </summary>
    public class Dal : IDal
    {
        /// <summary>
        /// Initializes a new instance of the Dal class by creating a new instance of BddContext.
        /// </summary>
        private BddContext _bddContext;

        public Dal()
        {
            _bddContext = new BddContext();
        }
        /// <summary>
        /// Supprime la base de données existante et recrée une nouvelle base de données.
        /// </summary>
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

        /// <summary>
        /// Authenticate the user with a given username and password.
        /// </summary>
        /// <param name="username">The username of the user to authenticate.</param>
        /// <param name="password">The password of the user to authenticate.</param>
        /// <returns>The Account object corresponding to the authenticated user, or null if authentication failed.</returns>
        public Account Authentificate(string username, string password)
        {
            string motDePasse = EncodeMD5(password);
            Account user = this._bddContext.Account.FirstOrDefault(u => u.Username == username && u.Password == motDePasse);
            return user;
        }

        /// <summary>
        /// Encode a password using the MD5 hash algorithm.
        /// </summary>
        /// <param name="password">The password to encode.</param>
        /// <returns>The string representing the encoded password.</returns>
        public static string EncodeMD5(string password)
        {
            string selectedPassword = "UserChoice" + password + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(selectedPassword)));
        }

       
        /////////////////ACCOUNT

        /// <summary>
        /// This method returns a list that contains all accounts
        /// </summary>
        /// <returns>The list in the database</returns>
        public Account GetAccount(int id)
        {
            return this._bddContext.Account.Find(id);
        }

        /// <summary>
        /// Retrieves a user account from its identifier in the form of a character string.
        /// </summary>
        /// <param name="idStr">The user identifier in the form of a character string.</param>
        /// <returns>The Account object corresponding to the user with the given ID, or null if the ID is not a valid integer.</returns>
        public Account GetAccount(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetAccount(id);
            }
            return null;
        }

        /// <summary>
        /// This method returns a list that contains all accounts
        /// </summary>
        /// <returns>return database account list</returns>
        public List<Account> GetAccounts()
        {
            return _bddContext.Account.ToList();
        }

        /// <summary>
        /// Add a new user account with the provided information.
        /// </summary>
        /// <param name="username">The username of the new account.</param>
        /// <param name="password">The password for the new account.</param>
        /// <param name="contactId">The identifier of the contact card associated with the new account.</param>
        /// <param name="infopersoId">The identifier of the personal information record associated with the new account.</param>
        /// <param name="profileId">The profile ID associated with the new account.</param>
        /// <param name="role">The user role for the new account.</param>
        /// <param name="MessagerieId">The identifier of the mailbox associated with the new account.</param>
        /// <returns>The Account object representing the new user account added to the database.</returns>
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

        /// <summary>
        /// Create a new activity with the provided information.
        /// </summary>
        /// <param name="startDate">The start date of the activity.</param>
        /// <param name="endDate">The end date of the activity.</param>
        /// <param name="description">The description of the activity.</param>
        /// <param name="place">The place where the activity takes place.</param>
        /// <param name="activityType">The activity type.</param>
        /// <param name="eventType">The type of event associated with the activity.</param>
        /// <param name="organizer">The name of the activity organizer.</param>
        /// <param name="filepath">The path to the image associated with the activity.</param>
        /// <param name="accountId">The ID of the user account creating the activity.</param>
        /// <param name="nb">The number of participants expected for the activity.</param>
        /// <returns>The Activity object representing the new activity added to the database.</returns>
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

        /// <summary>
        /// Modify the information of an existing activity in the database.
        /// </summary>
        /// <param name="id">ID of the activity to modify.</param>
        /// <param name="startDate">Activity start date.</param>
        /// <param name="endDate">Activity end date.</param>
        /// <param name="theme">Activity theme.</param>
        /// <param name="description">Activity description.</param>
        /// <param name="Place">Activity location.</param>
        /// <param name="imagePath">Path to the image associated with the activity.</param>
        /// <param name="nb">Number of participants expected for the activity.</param>
        /// <returns>Void</returns>
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

        /// <summary>
        /// Deletes an existing activity in the database.
        /// </summary>
        /// <param name="id">ID of the activity to delete.</param>
        public void RemoveActivity(int id)
        {
            Activity activity = _bddContext.Activities.Find(id);
            if (activity != null)
            {
                _bddContext.Activities.Remove(activity);

                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method returns a list that contains all activities
        /// </summary>
        /// <returns>return database activity list</returns>
        public List<Activity> GetActivities()
        {
            return _bddContext.Activities.ToList();
        }


        /////////////////ADHERENT

        /// <summary>
        /// Add a new member to the database
        /// </summary>
        /// <param name="accountid">The account ID associated with the patron</param>
        /// <param name="benevoleid">The ID of the volunteer associated with the patron</param>
        /// <param name="membership">The member's membership ID</param>
        /// <param name="contributionId">The patron's contribution ID</param>
        /// <param name="docs">The path to the documents associated with the patron</param>
        /// <returns>The patron added</returns>
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
        /// Creates a new Adherent and adds it to the database.
        /// </summary>
        /// <param name="benevoleId">The ID of the corresponding Benevole</param>
        /// <param name="numAdherent">The number of the Adherent</param>
        /// <param name="inscriptiondate">The date of the Adherent's registration</param>
        /// <param name="contribution">The amount contributed by the Adherent</param>
        /// <param name="idDocuments">The file path of the Adherent's identification documents</param>
        /// <param name="teamId">The ID of the team the Adherent belongs to</param>
        /// <param name="adhesionId">The ID of the corresponding Adhesion</param>
        /// <param name="coachingId">The ID of the corresponding Coaching session</param>
        /// <returns>The ID of the created Adherent</returns>
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
        /// <returns>return database adherent list()</returns>
        public List<Adherent> GetAdherents()
        {
            return _bddContext.Adherents.ToList();
        }

        /// <summary>
        /// Modify the access path to a patron's documents.
        /// </summary>
        /// <param name="id">ID of the member to modify.</param>
        /// <param name="docsPath">New path to patron documents.</param>
        /// <returns>The modified member object, or null if the member does not exist.</returns>
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
        /// Deletes an existing adherent in the database.
        /// </summary>
        /// <param name="slot">adherent to delete.</param>
        public void RemoveAdherent(Adherent adherent)
        {
            _bddContext.Adherents.Remove(adherent);
            _bddContext.SaveChanges();
        }


        /////////////ADHESION

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
        /// <returns>return database adhesion list</returns>
        public List<Adhesion> GetAdhesions()
        {
            return _bddContext.Adhesions.ToList();
        }

        /// <summary>
        /// This method removes an Adhesion in the SQL database with an Adhesion
        /// </summary>
        /// <param name="adhesion">adhesion to delete</param>
        public void RemoveAdhesion(Adhesion adhesion)
        {
            _bddContext.Adhesions.Remove(adhesion);
            _bddContext.SaveChanges();
        }


        /////////////////BENEVOLE

        /// <summary>
        /// Creates a new volunteer with the given account ID and initializes the number of volunteering actions to 0.
        /// </summary>
        /// <param name="accountId">The ID of the account associated with the new volunteer.</param>
        /// <returns>The new volunteer created in the database.</returns>
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
        /// <returns>return database benevole list</returns>
        public List<Benevole> GetBenevoles()
        {
            return _bddContext.Benevoles.ToList();
        }

        /// <summary>
        /// This method removes a Benevole int the SQL database with a Benevole
        /// </summary>
        /// <param name="benevole">benevole to delete</param>
        public void RemoveBenevole(Benevole benevole)
        {
            _bddContext.Benevoles.Remove(benevole);
            _bddContext.SaveChanges();
        }


        /////////////////CHAT && CONVERSATIONS

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

        /// <summary>
        /// This method returns a list that contains all messagerieA
        /// </summary>
        /// <returns>return database messagerieA list</returns>
        public List<MessagerieA> GetMessageries()
        {
            return _bddContext.Messageries.ToList();
        }

        /// <summary>
        /// This method returns a list that contains all message
        /// </summary>
        /// <returns>return database message list</returns>
        public List<Message> GetMessages()
        {
            return _bddContext.Messages.ToList();
        }

        /// <summary>
        /// This method returns a list that contains all conversations
        /// </summary>
        /// <returns>return database conversation list</returns>
        public List<Conversation> GetConversations()
        {
            return _bddContext.Conversations.ToList();
        }

        /// <summary>
        /// This method retrieves the conversations started by a specific user.
        /// </summary>
        /// <param name="account1">The id of the user who started the conversations</param>
        /// <returns>A list of Conversation objects representing the conversations started by the user.</returns>
        public List<Conversation> GetUserConversationsStarter(int account1)
        {
           List <Conversation>ListConversationsStarter = new List<Conversation>();

            ListConversationsStarter =GetConversations().Where(r => r.FirstSenderId == account1).ToList();
            return ListConversationsStarter;
        }

        /// <summary>
        /// This method retrieves the conversations in which a specific user has replied.
        /// </summary>
        /// <param name="account1">The id of the user who has replied to the conversations</param>
        /// <returns>A list of Conversation objects representing the conversations in which the user has replied.</returns>
        public List<Conversation> GetUserConversationsReplier(int account1)
        {
            List<Conversation> ListConversationsReplier = new List<Conversation>();
            ListConversationsReplier = GetConversations().Where(r => r.ReceiverId== account1).ToList();
            //LinkedList<Conversation> TotalConversations= new LinkedList<Conversation>();
            return ListConversationsReplier;
        }

        /// <summary>
        /// Create a new conversation between two accounts.
        /// </summary>
        /// <param name="accountIdSender">The ID of the account that initiates the conversation.</param>
        /// <param name="accountReceiver">The ID of the account that receives the conversation.</param>
        /// <returns>The created conversation.</returns>
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

        /// <summary>
        /// Creates and returns the first message of a conversation.
        /// </summary>
        /// <param name="conversationId">The id of the conversation to which the message belongs.</param>
        /// <param name="account1">The id of the sender account.</param>
        /// <param name="account2">The id of the receiver account.</param>
        /// <param name="body">The body of the message.</param>
        /// <returns>The created message.</returns>
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

        /// <summary>
        /// Creates a new message as a reply to an existing conversation.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation to reply to.</param>
        /// <param name="account1">The ID of the user sending the message.</param>
        /// <param name="account2">The ID of the user receiving the message.</param>
        /// <param name="body">The content of the message.</param>
        /// <returns>The new message object.</returns>
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

            GetUserConversationsStarter(account1);
            GetUserConversationsReplier(account1);

            this._bddContext.Messages.Add(NewMessage);
            this._bddContext.SaveChanges();

            return NewMessage;
        }

        ////////////////////////////////////INFOPERSO (CONTACT)

        /// <summary>
        /// Add a new contact to the database.
        /// </summary>
        /// <param name="email">The contact's email address.</param>
        /// <param name="Tel">The contact's phone number.</param>
        /// <returns>The Contact instance that was added to the database.</returns>
        public Contact AddContact(string email, string Tel)

        {
            Contact contact = new Contact() {EmailAdress = email, TelephoneNumber = Tel };

            _bddContext.Contact.Add(contact);

            _bddContext.SaveChanges();

            return contact;
        }

        /// <summary>
        /// This method returns a list that contains all contacts
        /// </summary>
        /// <returns>return database contact list</returns>
        public List<Contact> GetContacts()
        {
            return _bddContext.Contact.ToList();
        }

        /// <summary>
        /// Modify an existing publication in the database with the provided information.
        /// </summary>
        /// <param name="id">The id of the post to modify.</param>
        /// <param name="name">The new name of the post.</param>
        /// <param name="type">The new post type.</param>
        /// <param name="content">The new post content.</param>
        /// <param name="date">The new date of the publication.</param>
        /// <param name="imagePath">The new post image path.</param>
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
        /// Create a new contribution object and add it to the database with the given RIB.
        /// </summary>
        /// <param name="rib">The RIB (Relevé d'Identité Bancaire) of the contribution</param>
        /// <returns>The newly created contribution object</returns>
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
        /// <returns>return database contribution list</returns>
        public List<Contribution> GetContributions()
        {
            return _bddContext.Contributions.ToList();
        }

        /// <summary>
        /// This method removes a contribution in the SQL database with a contribution
        /// </summary>
        /// <param name="contribution">contribution to delete</param>
        public void RemoveContribution(Contribution contribution)
        {
            _bddContext.Contributions.Remove(contribution);
            _bddContext.SaveChanges();
        }

        /////////////////EMPLOYEE

        /// <summary>
        /// This method returns a list that contains all employees
        /// </summary>
        /// <returns>return database employee list</returns>
        public List<Employee> GetEmployees()
        {
            return _bddContext.Employees.ToList();
        }

        /// <summary>
        /// Creates a new profile for an employee with the provided bio and games information.
        /// </summary>
        /// <param name="bio">The employee's bio information.</param>
        /// <param name="games">The games information the employee plays.</param>
        /// <returns>A Profile object with the provided information.</returns>
        public Profile CreateProfileEmployee(string bio, string games)
        {
            Profile profile = new Profile() { Bio= bio, Games = games };
            _bddContext.Profils.Add(profile);
            _bddContext.SaveChanges();
            return profile;
        }

        /// <summary>
        /// Creates a new Employee with the provided account ID, job name, and matricule.
        /// </summary>
        /// <param name="accountId">The account ID associated with the employee.</param>
        /// <param name="jobname">The name of the job the employee has.</param>
        /// <param name="matricule">The unique serial number for the employee.</param>
        /// <returns>An Employee object with the provided information.</returns>
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

        /// <summary>
        /// This method removes a contribution in the SQL database with a employee
        /// </summary>
        /// <param name="employee">employee to delete</param>
        public void RemoveEmployee(Employee employee)
        {
            _bddContext.Employees.Remove(employee);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Retrieve the list of objects (stuff) belonging to a given account
        /// </summary>
        /// <param name="accountid">Account identifier</param>
        /// <returns>List of Stuff type objects belonging to the given account</returns>
        public List<Stuff> GetOwnedStuff(int accountid)
        {   
            List<Stuff> stuffOwned = _bddContext.Stuffs.Where(s => s.AccountOwnerId== accountid).ToList();

            return stuffOwned;
        }

        ///////////////////INFOPERSO

        /// <summary>
        /// Create a new personal information record and add it to the database.
        /// </summary>
        /// <param name="firstname">The first name of the person.</param>
        /// <param name="lastame">The last name of the person.</param>
        /// <param name="dob">The date of birth of the person.</param>
        /// <returns>The personal information object that was just created.</returns>
        public InfoPerso AddInfoPerso(string firstname,string lastame,string dob)
        {
            InfoPerso infoPerso = new InfoPerso() { FirstName = firstname, LastName = lastame,Birthday =dob};

            _bddContext.PersonnalInfo.Add(infoPerso);

            _bddContext.SaveChanges();

            return infoPerso;
        }

        /// <summary>
        /// This method returns a list that contains all info perso
        /// </summary>
        /// <returns>return database info perso list</returns>
        public List<InfoPerso> GetInformations()
        {
            return _bddContext.PersonnalInfo.ToList();
        }


        /////////////////PLANNING

        /// <summary>
        /// Adds a new planning with the specified name to the database and returns the new Planning object.
        /// </summary>
        /// <param name="name">The name of the new planning.</param>
        /// <returns>The new Planning object.</returns>
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

        /// <summary>
        /// This method returns a list that contains all plannings
        /// </summary>
        /// <returns>return database planning list</returns>
        public List<Planning> GetPlannings()
        {
            return _bddContext.Planning.ToList();
        }

        /// <summary>
        /// Adds the specified Slot object to the planning associated with the specified account.
        /// </summary>
        /// <param name="accountId">The ID of the account to add the slot to.</param>
        /// <param name="slot">The Slot object to add to the planning.</param>
        public void AddSlotToPlanning(int accountId, Slot slot)
        {
            Account account=GetAccount(accountId);
           Planning planning= GetPlannings().Where(r => r.Id == account.PlanningId).FirstOrDefault();
            slot.PlanningId = planning.Id;
            this._bddContext.Slots.UpdateRange();
            this._bddContext.SaveChanges();    
        }


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

        /// <summary>
        /// Deletes an existing publication in the database.
        /// </summary>
        /// <param name="id">ID of the publication to delete.</param>
        public void RemovePublication(int id)
        {
            Publication publication = _bddContext.Publications.Find(id);
            if (publication != null)
            {
                _bddContext.Publications.Remove(publication);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method returns a list that contains all publications
        /// </summary>
        /// <returns>return database publication list</returns>
        public List<Publication> GetPublications()
        {
            return _bddContext.Publications.ToList();
        }

        /// <summary>
        /// This method searches for a post in the publication list
        /// </summary>
        /// <param name="id">The identifier of the publication to search for</param>
        /// <returns>return the publication search</returns>
        public Publication GetOnePublication(int id)
        {
            Publication publication = _bddContext.Publications.Find(id);
            return publication;
        }

        /////////////////PROFILE

        /// <summary>
        /// Add a profile with the provided information.
        /// </summary>
        /// <param name="profilImage">The image of the new profile</param>
        /// <param name="Bio">The biographie of the new profile</param>
        /// <param name="games">Games of the new profile</param>
        /// <returns>return the publication created</returns>
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
        /// <returns>return the id profile</returns>
        public int CreateProfile()
        {
            Profile profile = Profile.CreateProfile();
            _bddContext.Profils.Add(profile);
            _bddContext.SaveChanges();
            return profile.Id;
        }

        /// <summary>
        /// This method searches for a profile in the profiles list
        /// </summary>
        /// <param name="id">The identifier of the profile to search for</param>
        /// <returns>return the profile search</returns>
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
        /// <summary>
        /// This method returns a list that contains all profiles
        /// </summary>
        /// <returns>return database profile list</returns>
        public List<Profile> GetProfiles()
        {
            return _bddContext.Profils.ToList();
        }

        /////////////////RESERVATION STUFF

        /// <summary>
        /// This method creates a reservation of a stuff
        /// </summary>
        /// <param name="reservation">the reservation created</param>
        /// <returns>the reservation</returns>
        public ReservationStuff CreateReservationStuff(ReservationStuff reservation)
        {
            _bddContext.ReservationsStuffs.Add(reservation);
            _bddContext.SaveChanges();
            return reservation;
        }

        /// <summary>
        /// This method returns a list that contains all reservations for stuff
        /// </summary>
        /// <returns>return database reservation list</returns>
        public List<ReservationStuff> GetReservations()
        {
            return _bddContext.ReservationsStuffs.ToList();
        }


        /////////////////SLOTS


        /// <summary>
        /// Create a new activity with the provided information.
        /// </summary>
        /// <param name="startDate">The start date of the activity.</param>
        /// <param name="endDate">The end date of the activity.</param>
        /// <param name="description">The description of the activity.</param>
        /// <param name="place">The place where the activity takes place.</param>
        /// <param name="activityType">The activity type.</param>
        /// <param name="eventType">The type of event associated with the activity.</param>
        /// <param name="organizer">The name of the activity organizer.</param>
        /// <param name="filepath">The path to the image associated with the activity.</param>
        /// <param name="accountId">The ID of the user account creating the activity.</param>
        /// <param name="nb">The number of participants expected for the activity.</param>
        /// <returns>The Activity object representing the new activity added to the database.</returns>


        /// <summary>
        /// Create a new slot with the provided information.
        /// </summary>
        /// <param name="startHour">The start date of the slot</param>
        /// <param name="endHour">The end date of the slot</param>
        /// <param name="ActivityId">The ID of the activity of the slot</param>
        /// <param name="PlanningId">The ID of the planning of the slot</param>
        /// <returns></returns>
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

        /// <summary>
        /// Modify the information of an existing slot in the database.
        /// </summary>
        /// <param name="id">The id of the slot that is modified</param>
        /// <param name="startHour">The start date of the slot</param>
        /// <param name="endHour">The end date of the slot</param>
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

        /// <summary>
        /// Deletes an existing slot in the database.
        /// </summary>
        /// <param name="id">ID of the slot to delete.</param>
        public void RemoveSlot(int id)
        {
            Slot slot = _bddContext.Slots.Find(id);

            if (slot != null)
            {
                _bddContext.Slots.Remove(slot);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method returns a list that contains all slots
        /// </summary>
        /// <returns>return database slot list</returns>
        public List<Slot> GetSlots()
        {
            return _bddContext.Slots.ToList();
        }

        /// <summary>
        /// Deletes an existing slot in the database.
        /// </summary>
        /// <param name="slot">slot to delete.</param>
        public void RemoveSlot(Slot slot)
        {
            _bddContext.Slots.Remove(slot);
            _bddContext.SaveChanges();
        }


        /////////////////STUFF

        /// <summary>
        /// Create a new stuff with the provided information.
        /// </summary>
        /// <param name="profilImage">The image of the created stuff</param>
        /// <param name="accountOwnerId">The id of the account owner of the created stuff</param>
        /// <param name="name">The name of the created stuff</param>
        /// <param name="description">The description of the created stuff</param>
        /// <param name="type">The type of the created stuff</param>
        /// <param name="state">The state of the created stuff</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method searches for a stuff in the stuff list
        /// </summary>
        /// <param name="id">The identifier of the stuff to search for</param>
        /// <returns>return the stuff search</returns>
        public Stuff GetOneStuff(int id)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
            return stuff;
        }

        /// <summary>
        /// This method returns a list that contains all stuffs
        /// </summary>
        /// <returns>return database stuff list</returns>
        public List<Stuff> GetStuffs()
        {
            return _bddContext.Stuffs.ToList();
        }


        /// <summary>
        /// This method modifies the reservation of the material, ie the material itself  by modifying the material and therefore deleting the reservation request.
        /// </summary>
        /// <param name="id">The stuff id</param>
        /// <param name="accountBorrowerId">The borrower's account id</param>
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

        /// <summary>
        /// This method modifies the reservation of the material, ie the material itself by accepting it.
        /// </summary>
        /// <param name="id">The stuff id</param>
        public void EditStuffAcceptation(int id)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
            if (stuff != null)
            {
                stuff.Reservation = Reservation.reserve;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method modifies the reservation of the material, that is to say the material itself by refusing it.
        /// </summary>
        /// <param name="id">The stuff id</param>
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

        /// <summary>
        /// Modify an existing publication in the database with the provided information.
        /// </summary>
        /// <param name="id">The stuff id</param>
        /// <param name="name">The stuff name</param>
        /// <param name="description">The stuff description</param>
        /// <param name="type">The stuff type</param>
        /// <param name="state">The stuff state</param>
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

        /// <summary>
        /// Deletes an existing stuff in the database.
        /// </summary>
        /// <param name="id">ID of the stuff to delete.</param>
        public void RemoveStuff(int id)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
        if (stuff != null)
          {
            _bddContext.Stuffs.Remove(stuff);
            _bddContext.SaveChanges();
            }
        }   

        /// <summary>
        /// Deletes an existing slot in the database
        /// </summary>
        /// <param name="stuff">stuff to delete</param>
        public void RemoveStuff(Stuff stuff)
        {
            _bddContext.Stuffs.Remove(stuff);
            _bddContext.SaveChanges();
        }

        ////////////////REMOVE

        /// <summary>
        /// Deletes an existing planning in the database.
        /// </summary>
        /// <param name="planning">planning to delete</param>
        public void RemovePlanning(Planning planning)
        {
            _bddContext.Planning.Remove(planning);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing contact in the database.
        /// </summary>
        /// <param name="contact">contact to delete</param>
        public void RemoveContact(Contact contact)
        {
            _bddContext.Contact.Remove(contact);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing InfoPerso in the database.
        /// </summary>
        /// <param name="infos">infoperso to delete.</param>
        public void RemoveInfos(InfoPerso infos)
        {
            _bddContext.PersonnalInfo.Remove(infos);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing conversation in the database.
        /// </summary>
        /// <param name="conversation">conversation to delete.</param>
        public void RemoveConversation(Conversation conversation)
        {
            _bddContext.Conversations.Remove(conversation);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing Messagerie in the database.
        /// </summary>
        /// <param name="messagerie">messagerie to delete.</param>
        public void RemoveMessagerie(MessagerieA messagerie)
        {
            _bddContext.Messageries.Remove(messagerie);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing profile in the database.
        /// </summary>
        /// <param name="profile">profile to delete.</param>
        public void RemoveProfile(Profile profile)
        {
            _bddContext.Profils.Remove(profile);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing account in the database.
        /// </summary>
        /// <param name="Account">accout to delete.</param>
        public void RemoveAccount(Account account)
        {
            _bddContext.Account.Remove(account);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Deletes an existing activity in the database.
        /// </summary>
        /// <param name="activity">activity to delete.</param>
        public void RemoveActivities(Activity activity)
        {
            _bddContext.Activities.Remove(activity);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Modify an existing profile in the database with the provided information.
        /// </summary>
        /// <param name="id">The id profile</param>
        /// <param name="path">The id imagePath profile</param>
        /// <param name="games">The games profile</param>
        /// <param name="Bio">The Bio profile</param>
        /// <returns></returns>
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

        /// <summary>
        /// Modify an existing contact in the database with the provided information.
        /// </summary>
        /// <param name="id">The id contact</param>
        /// <param name="email">The email contact</param>
        /// <param name="tel">The tel contact</param>
        /// <returns></returns>
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

        /// <summary>
        /// Modify an existing account in the database with the provided information.
        /// </summary>
        /// <param name="id">The id account</param>
        /// <param name="username">The username account</param>
        /// <param name="password">The password account</param>
        /// <returns></returns>
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

    }
}
