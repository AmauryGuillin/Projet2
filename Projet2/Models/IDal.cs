using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Projet2.Models
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();

        List<Adherent> GetAdherents();

        List<Contribution>  GetContributions();

        List<Adhesion> GetAdhesions();


        List<Team> GetTeams();

        int CreateAdherent(int benevoleId, int numadherent, DateTime inscriptiondate, Double contibution, string idDocuments, int teamId, int adhesionId, int coachingId);
        void EditAdherent(int id, int benevoleId, int numAdherent, DateTime inscriptiondate, Double contribution, string idDocuments, int teamId, int adhesionId, int coachingId);
        void RemoveAdherent(int id);
        int CreateContribution(int id, bool paymentStatus, PrelevementDate prelevementDate, ContributionType contributionType);
        void EditContribution(int id, bool paymentStatus, PrelevementDate prelevementDate, ContributionType contributionType);
        void RemoveContribution(int id);
        //int CreateAdhesion(int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus);
        void EditAdhesion(int id, int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus);
        void RemoveAdhesion(int id);
        int CreateTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent);
        void EditTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent);
        void RemoveTeam(int id);
        int CreateAccount(int id, string username, string password);
        //int CreateProfile(int id, string imagePath, string Bio, string games);
        int CreateProfile();
        List<Profile> GetProfiles();
        int EditProfile(int id, string imagePath, string Bio, string games);

        int CreateBenevole(int accountId);

        void EditBenevole(int accountId, int id, int nbActionVolunteering);

        void RemoveBenevole(int id);

        int CreateEmployee(string serialNumber, string jobName, DateTime dateOfEmployement, int accountId);
        void EditEmployee(int id, string serialNumber, string jobName, DateTime dateOfEmployement, int accountId);
        void RemoveEmployee(int id);


        Stuff CreateStuff(Stuff stuff);
        void EditStuff(int id, string name, Type type, State state, int profilId, int inventoryId);
        void RemoveStuff(int id);
        List<Stuff> GetStuffs();

        int CreatePublication(string name, PublicationTypes publicationType, string content, DateTime creationdate, string author, int employeId);
        public void CreatePublication(Publication publication);
        public void EditPublication(int id, string name, PublicationTypes publicationType, string content, DateTime creationdate, string author, int employeId);
        public void EditPublication(Publication publication);
        public void RemovePublication(int id);
        public void RemovePublication(Publication publication);
        List<Publication> GetPublications();

        public int CreateActivity(DateTime startDate, DateTime endDate, int slotId);
        public int CreateActivity(Activity activity);
        public void EditActivity(int id, DateTime startDate, DateTime endDate, int slotId);
        public void EditActivity(Activity activity);
        public void RemoveActivity(int id);


        public Account AddAccount(string username, string password, int contactId, int infopersoId, int profileId,Role role);

        int CreateSlot(DateTime date, DateTime startHour, DateTime endHour);
        int CreateSlot(Slot slot);
        void EditSlot(int id, DateTime date, DateTime startHour, DateTime endHour);
        void EditSlot(Slot slot);
        void RemoveSlot(int id);

        List<Slot> GetSlots();


        int CreateAssoActivity(string description, string place, int activityId);
        int CreateAssoActivity(AssociationActivity associationActivity);
        void EditAssoActivity(int id, string description, string place, int activityId);
        void EditAssoActivity(AssociationActivity associationActivity);
        void RemoveAssoActivity(int id);
        public void RemoveAssoActivity(AssociationActivity associationActivity);
        List<AssociationActivity> GetAssoActivity();

        int CreateVolunteeringActivity(string type, string name, DateTime startDate, DateTime endDate, int associationActivity);
        int CreateVolunteeringActivity(VolunteeringActivity volunteeringActivity);
        void EditVolunteeringActivity(int id, string type, string name, DateTime startDate, DateTime endDate, int associationActivity);
        void EditVolunteeringActivity(VolunteeringActivity volunteeringActivity);
        void RemoveVolunteeringActivity(int id);
        void RemoveVolunteeringActivity(VolunteeringActivity volunteeringActivity);
        List<VolunteeringActivity> GetVolunteeringActivities();

        int CreateTournament(string finalScore, int numberOfParticipants, string reward, int gameId, int associationActivityId);
        int CreateTournament(Tournament tournament);
        void EditTournament(int id, string finalScore, int numberOfParticipants, string reward, int gameId, int associationActivityId);
        void EditTournament(Tournament tournament);
        void RemoveTournament(int id);
        void RemoveTournament(Tournament tournament);
        List<Tournament> GetTournaments();

        int CreateEvent(string theme, int numberOfParticipants, int associationActivityId);
        void EditEvent(int id, string theme, int numberOfParticipants, int associationActivityId);
        void EditEvent(Event ev);
        void RemoveEvent(int id);
        void RemoveEvent(Event ev);
        List<Event> GetEvents();




    }




}
