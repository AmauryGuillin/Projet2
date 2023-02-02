﻿using System;
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
        int CreateContribution(int id, bool paymentStatus, double totalCount, PrelevementDate prelevementDate, ContributionType contributionType);
        void EditContribution(int id, bool paymentStatus, double totalCount, PrelevementDate prelevementDate, ContributionType contributionType);
        void RemoveContribution(int id);
        int CreateAdhesion(int id, int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus);
        void EditAdhesion(int id, int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus);
        void RemoveAdhesion(int id);
        int CreateTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent);
        void EditTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent);
        void RemoveTeam(int id);


        int CreateAccount(int id, string username, string password, int profileid);
        //int CreateProfile(int id, string imagePath, string Bio, string games);
        int CreateProfile();
        List<Profile> GetProfiles();
        int EditProfile(int id, string imagePath, string Bio, string games);

        int CreateBenevole(int accountId);

        void EditBenevole(int accountId, int id, int nbActionVolunteering);

        void RemoveBenevole(int id);


        int CreateStuff(string name, string type, State state, int profilId, int inventoryId);
        void EditStuff(int id, string name, string type, State state, int profilId, int inventoryId);
        void RemoveStuff(int id);
        List<Stuff> GetStuff();




        int CreateEmployee(int serialNumber, string jobName, DateTime dateOfEmployement, int accountId);
        void EditEmployee(int id, int serialNumber, string jobName, DateTime dateOfEmployement, int accountId);
        void RemoveEmployee(int id);

    }




}
