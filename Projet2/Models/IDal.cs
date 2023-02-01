using System;
using System.Collections.Generic;

namespace Projet2.Models
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();

        List<Adherent> GetAdherent();

        void EditAdherent(int id, int benevoleId, int numAdherent, DateTime dateInscription, string justifIdentite);

        void CreateAdherent(int id, int benevoleId, int numadherent, DateTime inscriptiondate, Double contibution, string idDocuments, int teamId, int adhesionId, int coachingId);

        void CreateContribution();
    }

    

    
}
