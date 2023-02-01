using System;
using System.Collections.Generic;

namespace Projet2.Models
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();

        List<Adherent> GetAdherents();

        List<Contribution>  GetContributions();

        List<Adhesion> GetAdhesions();

        List<Team> GetTeams();

        void CreateAdherent(int id, int benevoleId, int numadherent, DateTime inscriptiondate, Double contibution, string idDocuments, int teamId, int adhesionId, int coachingId);

        void EditAdherent(int id, int benevoleId, int numAdherent, DateTime inscriptiondate, Double contribution, string idDocuments, int teamId, int adhesionId, int coachingId);

        void CreateContribution(int id, bool paymentStatus, double totalCount, PrelevementDate prelevementDate, ContributionType contributionType);
        void EditContribution(int id, bool paymentStatus, double totalCount, PrelevementDate prelevementDate, ContributionType contributionType);
        void CreateAdhesion(int id, int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus);

        void CreateTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent);
    }

    

    
}
