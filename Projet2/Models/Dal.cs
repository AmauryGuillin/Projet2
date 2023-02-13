﻿using MySql.Data.MySqlClient;
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

        public int CreateAccount(int id, string username, string password)/// Dans le create Account, Profile Id= createProfile();
        {

            Account account = new Account() { Id = id, Username = username, Password = password, ProfileId = CreateProfile(), InventoryId =  CreateInventory()};


            _bddContext.Account.Add(account);
            _bddContext.SaveChanges();

            return account.Id;

        }
        /// <summary>
        /// This method returns a list that contains all accounts
        /// </summary>
        /// <returns></returns>

        public List<Account> GetListAccount()
        {
            return _bddContext.Account.ToList();
        }

        
        public  int[] AccountIds()
        {
            //int[] accountIds = new int[] {GetListAccount().Count };
            List<int> list = new List<int>();
           foreach(Account Account in this._bddContext.Account){
               int accountId = Account.Id;
                list.Add(accountId);
            }
            int[]accountIds=list.ToArray();
           return accountIds;
        }

        //public int[] Accountids()
        //{
        //    int size = GetListAccount().Count;
        //    int[] accountIds = new int[size];
        //}

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

        public Role GetAccountRole(int id)
        {
            Account account= this._bddContext.Account.Find(id);
            Role role=account.role;

            return role;
        }
        public Role GetAccountRole(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetAccountRole(id);
            }
            return 0;
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



        /////////////////ASSOCIATION ACTIVITIES

        public int CreateAssoActivity(string description, string place, int activityId)
        {
            AssociationActivity associationActivity = new AssociationActivity()
            {
                Description = description,
                Place = place,
                //ActivityId = activityId,
            };

            _bddContext.AssociationActivities.Add(associationActivity);
            _bddContext.SaveChanges();
            return associationActivity.Id;
        }

        public int CreateAssoActivity(AssociationActivity associationActivity)
        {
            _bddContext.AssociationActivities.Add(associationActivity);
            _bddContext.SaveChanges();
            return associationActivity.Id;
        }

        public void EditAssoActivity(int id, string description, string place, int activityId)
        {
            AssociationActivity associationActivity = _bddContext.AssociationActivities.Find(id);
            if (associationActivity != null)
            {
                associationActivity.Description = description;
                associationActivity.Place = place;
                //associationActivity.ActivityId = activityId;
                _bddContext.SaveChanges();
            }
        }

        public void EditAssoActivity(AssociationActivity associationActivity)
        {
            _bddContext.AssociationActivities.Update(associationActivity);
            _bddContext.SaveChanges();
        }

        public void RemoveAssoActivity(int id)
        {
            AssociationActivity associationActivity = _bddContext.AssociationActivities.Find(id);

            if (associationActivity != null)
            {
                _bddContext.AssociationActivities.Remove(associationActivity);
                _bddContext.SaveChanges();
            }
        }
        public void RemoveAssoActivity(AssociationActivity associationActivity)
        {

            _bddContext.AssociationActivities.Remove(associationActivity);
            _bddContext.SaveChanges();

        }

        public List<AssociationActivity> GetAssoActivity()
        {
            return _bddContext.AssociationActivities.ToList();
        }


        /////////////////ACTIVITY

        public int CreateActivity(DateTime startDate, DateTime endDate)
        {
            Activity activity = new Activity() { StartDate = startDate, EndDate = endDate };
            _bddContext.Activities.Add(activity);
            _bddContext.SaveChanges();

            return activity.Id;
        }

        public Activity CreateNewActivity(DateTime startDate, DateTime endDate, string place, string description,ActivityType activityType,EventType eventType,string organizer,string filepath,int accountId)
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
                PublisherId=accountId
            };
            _bddContext.Activities.Add(activity);
            _bddContext.SaveChanges();

            return activity;
        }

        public int CreateActivity(Activity activity)
        {
            _bddContext.Activities.Add(activity);
            _bddContext.SaveChanges();
            return activity.Id;
        }

        public void EditActivity(int id, DateTime startDate, DateTime endDate, string theme,string description,string Place, string imagePath)
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
               
                _bddContext.SaveChanges();
            }

        }
        public void EditEvent(int id, string theme, int numberOfParticipants, int ActivityId)
        {
            Event ev = _bddContext.Events.Find(id);
            if (ev != null)
            {
                ev.Theme = theme;
                ev.NumberOfParticipants = numberOfParticipants;
                ev.ActivityId = ActivityId;
                _bddContext.SaveChanges();
            }
        }
        public void EditActivity(Activity activity)
        {
            _bddContext.Activities.Update(activity);
            _bddContext.SaveChanges();
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
                TeamId = teamId,
                AdhesionId = adhesionId,
               
            };

            _bddContext.Adherents.Add(adherent);

            _bddContext.SaveChanges();
            return adherent.Id;

        }
        public Adherent CreateNewAdherent(int accountid)
        {
            Adherent adherent = new Adherent()
            {
                AccountId = accountid,
                Benevole = new Benevole() { AccountId= accountid},
                Adhesion = new Adhesion() { Contribution = new Contribution() },
            };
            
            _bddContext.Adherents.Add(adherent);

            _bddContext.SaveChanges();
            return adherent;

        }
        /// <summary>
        /// This method creates an Adherent in the SQL database with an adherent
        /// </summary>
        /// <param name="adherent"></param>
        public void CreateAdherent(Adherent adherent)
        {
            _bddContext.Adherents.Add(adherent);
            _bddContext.SaveChanges();
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
        public void EditAdherent(int id, int benevoleId, int numAdherent, DateTime inscriptiondate, Double contribution, string idDocuments, int teamId, int adhesionId, int coachingId)
        {
            Adherent adherent = _bddContext.Adherents.Find(id);
            if (adherent != null)
            {
                adherent.BenevoleId = benevoleId;
                adherent.NumAdherent = numAdherent;
                adherent.InscriptionDate = inscriptiondate;
                adherent.Contribution = contribution;
                adherent.DocPath = idDocuments;
                adherent.TeamId = teamId;
                adherent.AdhesionId = adhesionId;
                
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method modifies an Adherent in the SQL database with an Adherent
        /// </summary>
        /// <param name="adherent"></param>
        public void EditAdherent(Adherent adherent)
        {
            _bddContext.Adherents.Update(adherent);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method removes an Adherent in the SQL database with the Adherent id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAdherent(int id)
        {
            Adherent adherent = _bddContext.Adherents.Find(id);
            if (adherent != null)
            {
                _bddContext.Adherents.Remove(adherent);
                _bddContext.SaveChanges();
            }
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
        public int CreateAdhesion(int id,int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus)
        {
            Adhesion adhesion = new Adhesion()
            {
                Id = id,
                ContributionId = contributionId,
                Echeance = Echeance,
                AdhesionStatus = adhesionStatus
            };

            _bddContext.Adhesions.Add(adhesion);
            _bddContext.SaveChanges();
            return adhesion.Id;
        }
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
        /// This method creates an Adhesion in the SQL database with an Adhesion
        /// </summary>
        /// <param name="adhesion"></param>
        public void CreateAdhesion(Adhesion adhesion)
        {
            _bddContext.Adhesions.Add(adhesion);
            _bddContext.SaveChanges();
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
        /// This method modifies an Adhesion in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contributionId"></param>
        /// <param name="Echeance"></param>
        /// <param name="adhesionStatus"></param>
        public void EditAdhesion(int id, int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus)
        {
            Adhesion adhesion = _bddContext.Adhesions.Find(id);
            if (adhesion != null)
            {
                adhesion.ContributionId = contributionId;
                adhesion.Echeance = Echeance;
                adhesion.AdhesionStatus = adhesionStatus;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method modifies an Adhesion in the SQL database with an Adhesion
        /// </summary>
        /// <param name="adhesion"></param>
        public void EditAdhesion(Adhesion adhesion)
        {
            _bddContext.Adhesions.Update(adhesion);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method removes an Adhesion in the SQL database with the Adhesion id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAdhesion(int id)
        {
            Adhesion adhesion = _bddContext.Adhesions.Find(id);
            if (adhesion != null)
            {
                _bddContext.Adhesions.Remove(adhesion);
                _bddContext.SaveChanges();
            }
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

        public int CreateBenevole(int accountId)

        {
            int nbActionVolunteering = 0;
            Benevole benevole = new Benevole() { AccountId = accountId, NbActionVolunteering = nbActionVolunteering };

            _bddContext.Benevoles.Add(benevole);

            _bddContext.SaveChanges();

            return benevole.Id;
        }

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
        /// This method creates a Benevole in the SQL database with a benevole
        /// </summary>
        /// <param name="benevole"></param>

        public void CreateBenevole(Benevole benevole)
        {
            _bddContext.Benevoles.Add(benevole);
            _bddContext.SaveChanges();
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
        /// This method modifies a Benevole in the SQL database
        /// </summary>
        /// <param name="accountId"></param>
        public void EditBenevole(int accountId, int id, int nbActionVolunteering)
        {
            Benevole benevole = _bddContext.Benevoles.Find(id);
            if (benevole != null)
            {
                benevole.AccountId = accountId;
                benevole.NbActionVolunteering = nbActionVolunteering;
                _bddContext.SaveChanges();
            }
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

        public void RemoveBenevole(int id)
        {
            Benevole benevole = _bddContext.Benevoles.Find(id);
            if (benevole != null)
            {
                _bddContext.Benevoles.Remove(benevole);
                _bddContext.SaveChanges();
            }
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

        public Conversation GetThisConversation(int convId)
        {
            return _bddContext.Conversations.Find(convId);
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
                MessageTimeStamp = new DateTime(),
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
                MessageTimeStamp = new DateTime(),
                isRead= false,
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
        public int EditContact(int id, string email, string tel)
        {
            Contact contact = this._bddContext.Contact.Find(id);
            if (contact != null)
            {
                contact.EmailAdress = email;
                contact.TelephoneNumber = tel;
                _bddContext.SaveChanges();
            }
            return contact.Id;
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
        public Contact GetContact(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetContact(id);
            }
            return null;
        }


        public List<Contact> GetContacts()
        {
            return _bddContext.Contact.ToList();
        }


        public void EditPublication(Publication publication)
        {
            _bddContext.Publications.Update(publication);
            _bddContext.SaveChanges();
        }

        public void EditPublication(int id, string name, PublicationTypes type, string content, string date)
        {
            Publication publi = _bddContext.Publications.Find(id);
            if (publi != null)
            {
                publi.Name = name;
                publi.PublicationType = type;
                publi.Content = content;
                publi.Date = date;
                _bddContext.SaveChanges();
            }
        }

        /////////////////CONTRIBUTION

        ///
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
        /// This method creates an Contribution in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="totalCount"></param>
        /// <param name="prelevementDate"></param>
        /// <param name="contributionType"></param>
        /// <returns>contribution.Id</returns>
        public int CreateContribution(int id, bool paymentStatus, PrelevementDate prelevementDate, ContributionType contributionType)
        {
            Contribution contribution = new Contribution()
            {
                Id = id,
                PaymentStatus = paymentStatus,
               
                PrelevementDate = prelevementDate,
                ContributionType = contributionType
            };

            _bddContext.Contributions.Add(contribution);
            _bddContext.SaveChanges();
            return contribution.Id;

        }
        /// <summary>
        /// This method creates an contribution in the SQL database with a contribution
        /// </summary>
        /// <param name="contribution"></param>
        public void CreateContribution(Contribution contribution)
        {
            _bddContext.Contributions.Add(contribution);
            _bddContext.SaveChanges();
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
        /// This method modifies a Contribution in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="totalCount"></param>
        /// <param name="prelevementDate"></param>
        /// <param name="contributionType"></param>
        public void EditContribution(int id, bool paymentStatus,  PrelevementDate prelevementDate, ContributionType contributionType)
        {
            Contribution contribution = _bddContext.Contributions.Find(id);
            if (contribution != null)
            {
                contribution.PaymentStatus = paymentStatus;
              
                contribution.PrelevementDate = prelevementDate;
                contribution.ContributionType = contributionType;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method modifies a contribution in the SQL database with a contribution
        /// </summary>
        /// <param name="contribution"></param>
        public void EditContribution(Contribution contribution)
        {
            _bddContext.Contributions.Update(contribution);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method removes a contribution in the SQL database with the contribution Id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveContribution(int id)
        {
            Contribution contribution = _bddContext.Contributions.Find(id);
            if (contribution != null)
            {
                _bddContext.Contributions.Remove(contribution);
                _bddContext.SaveChanges();
            }
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
        public int CreateEmployee(string serialNumber, string jobName, string dateOfEmployement, int accountId)

        {
            Employee employee = new Employee() { SerialNumber = serialNumber, JobName = jobName, DateOfEmployement = dateOfEmployement, AccountId = accountId };
            _bddContext.Employees.Add(employee);
            _bddContext.SaveChanges();

            return employee.Id;
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

        public void EditEmployee(int id, string serialNumber, string jobName, string dateOfEmployement, int accountId)
        {
            Employee employee = _bddContext.Employees.Find(id);
            if (employee != null)
            {
                employee.SerialNumber = serialNumber;
                employee.JobName = jobName;
                employee.DateOfEmployement = dateOfEmployement;
                employee.AccountId = accountId;
                _bddContext.SaveChanges();
            }
        }
        public void EditEmployee(Employee employee)
        {
            _bddContext.Employees.Update(employee);
            _bddContext.SaveChanges();
        }

        public void RemoveEmployee(int id)
        {
            Employee employee = _bddContext.Employees.Find(id);
            if (employee != null)
            {
                _bddContext.Employees.Remove(employee);
                _bddContext.SaveChanges();
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            _bddContext.Employees.Remove(employee);
            _bddContext.SaveChanges();
        }

        /////////////////EVENT

        public int CreateEvent(string theme, int numberOfParticipants, int ActivityId)
        {
            Event ev = new Event() { Theme = theme, NumberOfParticipants = numberOfParticipants, ActivityId = ActivityId };
            _bddContext.Events.Add(ev);
            _bddContext.SaveChanges();
            return ev.Id;
        }

        public int CreateEvent(Event ev)
        {
            _bddContext.Events.Add(ev);
            _bddContext.SaveChanges();
            return ev.Id;
        }

        //public void EditEvent(int id, string theme, int numberOfParticipants, int ActivityId)
        //{
        //    Event ev = _bddContext.Events.Find(id);
        //    if (ev != null)
        //    {
        //        ev.Theme = theme;
        //        ev.NumberOfParticipants = numberOfParticipants;
        //        ev.ActivityId = ActivityId;
        //        _bddContext.SaveChanges();
        //    }
        //}

        public void EditEvent(Event ev)
        {
            _bddContext.Events.Update(ev);
            _bddContext.SaveChanges();
        }

        //public void RemoveEvent(int id)
        //{
        //    Event ev = _bddContext.Events.Find(id);
        //    if (ev != null)
        //    {
        //        _bddContext.Events.Remove(ev);
        //        _bddContext.SaveChanges();
        //    }
        //}

        public void RemoveEvent(int id)
        {
            Activity ev=GetEvents().Where(e => e.Id == id).FirstOrDefault();

            _bddContext.Activities.Remove(ev);
            _bddContext.SaveChanges();
        }

        public List<Activity> GetEvents()
        {
          List <Activity>Events=  GetActivities().Where(r=>r.activityType==ActivityType.Evenement).ToList();
        return Events;
        }

        /////////////////FORUM

        /////////////////GAMES

        /////////////////INVENTORY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stuffs"></param>
        /// <returns></returns>
        public int EditInventory(int id, List<Stuff> stuffs)
        {
            Inventory inventory = this._bddContext.Inventory.Find(id);
            if (inventory != null)
            {
                inventory.Stuffs = stuffs;
               
                _bddContext.SaveChanges();
            }
            return inventory.Id;
        }
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
        public Inventory GetInventory(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetInventory(id);
            }
            return null;
        }
        public List<Inventory> GetInventories()
        {
            return _bddContext.Inventory.ToList();
        }
        //public List<Stuff> GetBorrowerStuff(int accountid)
        //{

        //    int inventoryid = _bddContext.Account.Where(a => a.Id == accountid).FirstOrDefault().InventoryId.Value;
        //    List<Stuff> stuffContent = _bddContext.Stuffs.Where(s => s.InventoryBorrowerId == inventoryid).ToList();
            
        //    return stuffContent;
        //}
        public List<Stuff> GetBorrowerStuff(int accountid)
        {

            int inventoryid = _bddContext.Account.Where(a => a.Id == accountid).FirstOrDefault().InventoryId.Value;
            List<Stuff> stuffContent = _bddContext.Stuffs.Where(s => s.AccountBorrowerId == accountid).ToList();

            return stuffContent;
        }

        public List<Stuff> GetOwnedStuff(int accountid)
        {
            
            List<Stuff> stuffOwned = _bddContext.Stuffs.Where(s => s.AccountOwnerId== accountid).ToList();

            return stuffOwned;


            //Inventory inventory= new Inventory( );
            //List<Stuff> inventoryContent = new List<Stuff> ( );
            //foreach (Stuff stuffs in _bddContext.Stuffs) { 
            // GetStuffs().Where(r => r.InventoryBorrowerId == inventory.Id).FirstOrDefault();
            //    inventoryContent.Add( stuffs );
            //}
            //return inventoryContent;


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
        public int EditInfos(int id, string firstname, string lastname, string dob)
        {
            InfoPerso infos = this._bddContext.PersonnalInfo.Find(id);
            if (infos != null)
            {
                infos.LastName = lastname;
                infos.FirstName = firstname;
                infos.Birthday = dob;
                _bddContext.SaveChanges();
            }
            return infos.Id;
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
        public InfoPerso GetInfos(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetInfos(id);
            }
            return null;
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

        //public int CreatePublication(string name, PublicationTypes publicationType, string content, DateTime creationdate, string author, int employeId)
        //{
        //    Publication publication = new Publication() { Name = name, PublicationType = publicationType, Date = creationdate, Author = author, EmployeeId = employeId };
        //    _bddContext.Publications.Add(publication);
        //    _bddContext.SaveChanges();

        //    return publication.Id;

        //}

        //public Publication CreatePublication(Publication publication)
        //{
        //    _bddContext.Publications.Add(publication);
        //    _bddContext.SaveChanges();
        //    return publication;
        //}

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

        public void EditCreatePublication(int id, int autorId)
        {
            Publication publi = _bddContext.Publications.Find(id);
            if (publi != null)
            {
                publi.AccountId = autorId;
                _bddContext.SaveChanges();
            }
            
        }


        //public void EditPublication(int id, string name, PublicationTypes publicationType, string content, DateTime creationdate, string author, int employeId)
        //{
        //    Publication publication = _bddContext.Publications.Find(id);

        //    if (publication != null)
        //    {
        //        publication.Name = name;
        //        publication.PublicationType = publicationType;
        //        publication.Date = creationdate;
        //        publication.Author = author;
        //        publication.EmployeeId = employeId;
        //        _bddContext.SaveChanges();
        //    }
        //}

        public void RemovePublication(int id)

        {
            Publication publication = _bddContext.Publications.Find(id);
            if (publication != null)
            {
                _bddContext.Publications.Remove(publication);
                _bddContext.SaveChanges();
            }
        }



        //public void RemovePublication(Publication publication)
        //{
        //    _bddContext.Publications.Remove(publication);
        //    _bddContext.SaveChanges();
        //}

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
        public int EditProfile(int id, string imagePath, string Bio, string games)
        {
            Profile profile = this._bddContext.Profils.Find(id);
            if (profile != null)
            {
                profile.ImagePath = imagePath;
                profile.Bio = Bio;
                profile.Games = games;
                _bddContext.SaveChanges();
            }
            return profile.Id;
        }
        public int EditProfile(Profile profile)
        {
            _bddContext.Profils.Update(profile);
            _bddContext.SaveChanges();
            return profile.Id;
        }

        /////////////////RESERVATION STUFF

        //public void CreateReservationStuff(DateTime start, DateTime end)
        //{
        //    ReservationStuff reservation = new ReservationStuff()
        //    {
        //        StartDate = start,
        //        EndDate = end,
        //    };

        //    _bddContext.reservationsStuffs.Add(reservation);
        //    _bddContext.SaveChanges();
        //}

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



        public void RemoveReservationStuff(int id)
        {
            ReservationStuff reservation = _bddContext.ReservationsStuffs.Find(id);

            if (reservation != null)
            {
                _bddContext.ReservationsStuffs.Remove(reservation);
                _bddContext.SaveChanges();
            }
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

        public int CreateSlot(Slot slot)
        {
            _bddContext.Slots.Add(slot);
            _bddContext.SaveChanges();
            return slot.Id;
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

        public void EditSlot(Slot slot)
        {
            _bddContext.Slots.Update(slot);
            _bddContext.SaveChanges();
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

        public Slot AddActivityToSlot(Activity activity,Account account)
        {
            Slot activitySlot = new Slot()
            {
                Date = activity.StartDate,
                StartHour = activity.StartDate,
                EndHour = activity.EndDate,
                ActivityId = activity.Id,
                PlanningId =account.PlanningId,
            };
            return activitySlot;

        }
 public void RemoveSlot(Slot slot)
        {
            _bddContext.Slots.Remove(slot);
            _bddContext.SaveChanges();
        }

        /////////////////STUFF



        //public Stuff CreateStuff(Stuff stuff)
        //{
        //    _bddContext.Stuffs.Add(stuff);
        //    _bddContext.SaveChanges();
        //    return stuff;
        //}
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

        public void EditStuffCreate(int id, int accountOwnerId,string filepath)
        {
            Stuff stuff = _bddContext.Stuffs.Find(id);
            if (stuff != null)
            {
                stuff.Reservation = Reservation.libre;
                stuff.AccountOwnerId = accountOwnerId;
                stuff.ImagePath= filepath;
                _bddContext.SaveChanges();
            }
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

        public string GetOwnerStuff(int AccountOwnerId, string owner)
        {
            Account account = _bddContext.Account.Find(AccountOwnerId);
            if (account != null)
            {
                owner = account.Username;
            }
        return owner;
        }





        //public Stuff CreateStuff(string name, string imagePath, Type type, State state)
        //{
        //    Stuff stuff = new Stuff()
        //    {
        //        Name = name,
        //        Type = type,
        //        ImagePath= imagePath,
        //        State = state
        //    };
        //    return stuff;
        //}







        public void RemoveStuff(Stuff stuff)
        {
            _bddContext.Stuffs.Remove(stuff);
            _bddContext.SaveChanges();
        }

        /////////////////TEAM


        /// <summary>
        /// This method creates a team in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="gameId"></param>
        /// <param name="creationDate"></param>
        /// <param name="NbAdherent"></param>
        /// <returns>team.Id</returns>
        public int CreateTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent)
        {
            Team team = new Team()
            {
                Id = id,
                Name = name,
                GameId = gameId,
                CreationDate = creationDate,
                NbAdherent = NbAdherent
            };

            _bddContext.Teams.Add(team);
            _bddContext.SaveChanges();
            return team.Id;

        }
        /// <summary>
        /// This method creates a team in the SQL database with a team
        /// </summary>
        /// <param name="team"></param>
        public void CreateTeam(Team team)
        {
            _bddContext.Teams.Add(team);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// his method returns a list that contains all teams
        /// </summary>
        /// <returns></returns>
        public List<Team> GetTeams()
        {
            return _bddContext.Teams.ToList();
        }

        /// <summary>
        /// This method modifies a team in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="gameId"></param>
        /// <param name="creationDate"></param>
        /// <param name="NbAdherent"></param>
        public void EditTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent)
        {
            Team team = _bddContext.Teams.Find(id);
            if (team != null)
            {
                team.Name = name;
                team.GameId = gameId;
                team.CreationDate = creationDate;
                team.NbAdherent = NbAdherent;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method modifies a team in the SQL database with a Team
        /// </summary>
        /// <param name="team"></param>
        public void EditTeam(Team team)
        {
            _bddContext.Teams.Update(team);
            _bddContext.SaveChanges();
        }


        /// <summary>
        /// This method removes a team in the SQL database with the Team id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveTeam(int id)
        {
            Team team = _bddContext.Teams.Find(id);
            if (team != null)
            {
                _bddContext.Teams.Remove(team);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method removes a team in the SQL database with a team
        /// </summary>
        /// <param name="team"></param>
        public void RemoveTeam(Team team)
        {
            _bddContext.Teams.Remove(team);
            _bddContext.SaveChanges();
        }


        /////////////////TOURNAMENT


        internal void EditProfilePIC(string imagePath,int id)
        {
            Profile profilToUpdate = this._bddContext.Profils.Find(id);
            if (profilToUpdate != null)
            {
                profilToUpdate.ImagePath= imagePath;
                this._bddContext.SaveChanges();
            }
            
        }





        /////////////////VOLUNTEERING ACTIVITY

        
        public int CreateTournament(string finalScore, int numberOfParticipants, string reward, int gameId, int associationActivityId)
        {
            Tournament tournament = new Tournament() 
            {
                FinalScore= finalScore,
                NumberOfParticipants= numberOfParticipants,
                Reward= reward,
                GameId= gameId,
                AssociationActivityId= associationActivityId
            };
            _bddContext.Tournaments.Add(tournament);
            _bddContext.SaveChanges();
            return tournament.Id;
        }

        public int CreateTournament(Tournament tournament)
        {
            _bddContext.Tournaments.Add(tournament);
            _bddContext.SaveChanges();
            return tournament.Id;
        }

        public void EditTournament(int id, string finalScore, int numberOfParticipants, string reward, int gameId, int associationActivityId)
        {
            Tournament tournament = _bddContext.Tournaments.Find(id);
            if (tournament != null)
            {
                tournament.FinalScore = finalScore;
                tournament.NumberOfParticipants = numberOfParticipants;
                tournament.Reward = reward;
                tournament.GameId = gameId;
                tournament.AssociationActivityId = associationActivityId;
                _bddContext.Tournaments.Update(tournament);
                _bddContext.SaveChanges();
            }
        }

        public void EditTournament(Tournament tournament)
        {
            _bddContext.Tournaments.Update(tournament);
            _bddContext.SaveChanges();
            
        }

        public void RemoveTournament(int id)
        {
            Tournament tournament = _bddContext.Tournaments.Find(id);
            if (tournament != null)
            {
                _bddContext.Tournaments.Remove(tournament);
                _bddContext.SaveChanges();
            }
        }

        public void RemoveTournament(Tournament tournament)
        {
            _bddContext.Tournaments.Remove(tournament);
            _bddContext.SaveChanges();
        }

        public List<Tournament> GetTournaments()
        {
            return _bddContext.Tournaments.ToList();
        }


        /////////////////VOLUNTEERING ACTIVITY

        public int CreateVolunteeringActivity(string type, string name, DateTime startDate, DateTime endDate, int associationActivity)
        {
            VolunteeringActivity volunteeringActivity = new VolunteeringActivity() 
            {
                Type= type,
                Name= name,
                StartDate= startDate,
                EndDate= endDate,
                AssociationActivityId = associationActivity,  
            };
            _bddContext.VolunteeringActivities.Add(volunteeringActivity);
            _bddContext.SaveChanges();
            return volunteeringActivity.Id;
        }


        public int CreateVolunteeringActivity(VolunteeringActivity volunteeringActivity)
        {
            _bddContext.VolunteeringActivities.Add(volunteeringActivity);
            _bddContext.SaveChanges();
            return volunteeringActivity.Id;
        }


        public void EditVolunteeringActivity(int id, string type, string name, DateTime startDate, DateTime endDate, int associationActivity)
        {
            VolunteeringActivity volunteeringActivity = _bddContext.VolunteeringActivities.Find(id);
            if (volunteeringActivity != null)
            {
                volunteeringActivity.Type = type;
                volunteeringActivity.Name = name;
                volunteeringActivity.StartDate = startDate;
                volunteeringActivity.EndDate = endDate;
                volunteeringActivity.AssociationActivityId = associationActivity;
                _bddContext.SaveChanges();
            }
        }

        public void EditVolunteeringActivity(VolunteeringActivity volunteeringActivity)
        {
            _bddContext.VolunteeringActivities.Update(volunteeringActivity);
            _bddContext.SaveChanges();
        }

        public void RemoveVolunteeringActivity(int id)
        {
            VolunteeringActivity volunteeringActivity = _bddContext.VolunteeringActivities.Find(id);
            if (volunteeringActivity != null )
            {
                _bddContext.VolunteeringActivities.Remove(volunteeringActivity);
                _bddContext.SaveChanges();
            }
        }


        public void RemoveVolunteeringActivity(VolunteeringActivity volunteeringActivity)
        {
            _bddContext.VolunteeringActivities.Remove(volunteeringActivity);
            _bddContext.SaveChanges();
        }

        public List<VolunteeringActivity> GetVolunteeringActivities() 
        {
            return _bddContext.VolunteeringActivities.ToList();
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

        public void EditProfileS(int id, string path,string games,string Bio )
        {
            Profile profile = _bddContext.Profils.Find(id);
            if (profile != null)
            {
                profile.ImagePath = path;
                profile.Games = games;
                profile.Bio = Bio;
                
                _bddContext.SaveChanges();
            }
        }

        public void EditContacts(int id, string email,string tel)
        {
            Contact contact = _bddContext.Contact.Find(id);
            if (contact != null)
            {
                contact.EmailAdress = email;
                contact.TelephoneNumber = tel;
                

                _bddContext.SaveChanges();
            }

        }

        public void EditInfos(int id, string name, PublicationTypes type, string content, string date)
        {

        }



        //////////////////TND

    }
}
