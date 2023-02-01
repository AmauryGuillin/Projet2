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

        void EditAdherent(int id, int benevoleId, int numAdherent, DateTime dateInscription, string justifIdentite);

        void CreateAdherent(int id, int benevoleId, int numadherent, DateTime inscriptiondate, Double contibution, string idDocuments, int teamId, int adhesionId, int coachingId);

        void CreateContribution(int id, bool paymentStatus, double totalCount, PrelevementDate prelevementDate, ContributionType contributionType);

        void CreateAdhesion(int id, int contributionId, DateTime Echeance);

    }

    

    
}
