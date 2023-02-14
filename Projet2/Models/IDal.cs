using Projet2.Models.Informations;
using Projet2.Models.Messagerie;
using Projet2.Models.UserMessagerie;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Projet2.Models
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();
        public void Dispose();
        public Account Authentificate(string username, string password);
        public Account GetAccount(int id);
        public Account GetAccount(string idStr);
        public List<Account> GetAccounts();
        public Account AddAccount(string username, string password, int contactId, int infopersoId, int profileId, Role role, int MessagerieId);
        public Activity CreateNewActivity(DateTime startDate, DateTime endDate, string description, string place, ActivityType activityType, EventType eventType, string organizer, string filepath, int accountId, int nb);
        public void EditActivity(int id, DateTime startDate, DateTime endDate, string theme, string description, string Place, string imagePath, int nb);
        public void RemoveActivity(int id);
        public List<Activity> GetActivities();
        public Adherent AddAdherent(int accountid, int benevoleid, int adhesion, int contributionId, string docs);
        public int CreateAdherent(int benevoleId, int numAdherent, DateTime inscriptiondate, Double contribution, string idDocuments, int teamId, int adhesionId, int coachingId);
        public List<Adherent> GetAdherents();
        public Adherent EditAdherent(int id, string docsPath);
        public void RemoveAdherent(Adherent adherent);
        public Adhesion CreateNewAdhesion(int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus);
        public List<Adhesion> GetAdhesions();
        public void RemoveAdhesion(Adhesion adhesion);
        public Benevole CreateNewBenevole(int accountId);
        public List<Benevole> GetBenevoles();
        public void RemoveBenevole(Benevole benevole);
        public MessagerieA AddMessagerie();
        public List<MessagerieA> GetMessageries();
        public List<Message> GetMessages();
        public List<Conversation> GetConversations();
        public List<Conversation> GetUserConversationsStarter(int account1);
        public List<Conversation> GetUserConversationsReplier(int account1);
        public Conversation CreateConversation(int accountIdSender, int accountReceiver);
        public Message FirstMessage(int conversationId, int account1, int account2, string body);
        public Message MessageReply(int conversationId, int account1, int account2, string body);
        public Contact AddContact(string email, string Tel);
        public List<Contact> GetContacts();
        public void EditPublication(int id, string name, PublicationTypes type, string content, string date, string imagePath);
        public Contribution CreateNewContribution(string rib);
        public List<Contribution> GetContributions();
        public void RemoveContribution(Contribution contribution);
        public List<Employee> GetEmployees();
        public Profile CreateProfileEmployee(string bio, string games);
        public Employee CreateEmployee(int accountId, string jobname, string matricule);
        public void RemoveEmployee(Employee employee);
        public List<Stuff> GetOwnedStuff(int accountid);
        public InfoPerso AddInfoPerso(string firstname, string lastame, string dob);
        public List<InfoPerso> GetInformations();
        public Planning AddPlanning(string name);
        public List<Planning> GetPlannings();
        public void AddSlotToPlanning(int accountId, Slot slot);
        public Publication CreatePublication(string profilImage, int authorid, string body, string name, PublicationTypes publicationTypes, string date);
        public void RemovePublication(int id);
        public List<Publication> GetPublications();
        public Publication GetOnePublication(int id);
        public Profile AddProfile(string profilImage, string Bio, string games);
        public int CreateProfile();
        public Profile GetProfile(int id);
        public Profile GetProfile(string idStr);
        public List<Profile> GetProfiles();
        public ReservationStuff CreateReservationStuff(ReservationStuff reservation);
        public List<ReservationStuff> GetReservations();
        public Slot CreateSlot(DateTime startHour, DateTime endHour, int ActivityId, int PlanningId);
        public void EditSlot(int id, DateTime startHour, DateTime endHour);
        public void RemoveSlot(int id);
        public List<Slot> GetSlots();
        public void RemoveSlot(Slot slot);
        public Stuff CreateStuff(string profilImage, int accountOwnerId, string name, string description, Type type, State state);
        public Stuff GetOneStuff(int id);
        public List<Stuff> GetStuffs();
        public void EditStuffReservation(int id, int accountBorrowerId);
        public void EditStuffAcceptation(int id);
        public void EditStuffCancelation(int id);
        public void EditStuff(int id, string name, string description, Type type, State state);
        public void RemoveStuff(int id);
        public void RemoveStuff(Stuff stuff);
        public void RemovePlanning(Planning planning);
        public void RemoveContact(Contact contact);
        public void RemoveInfos(InfoPerso infos);
        public void RemoveConversation(Conversation conversation);
        public void RemoveMessagerie(MessagerieA messagerie);
        public void RemoveProfile(Profile profile);
        public void RemoveAccount(Account account);
        public void RemoveActivities(Activity activity);
        public Profile EditProfileS(int id, string path, string games, string Bio);
        public Contact EditContacts(int id, string email, string tel);
        public Account EditAccount(int id, string username, string password);
    }
}




