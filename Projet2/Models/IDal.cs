using System;
using System.Collections.Generic;

namespace Projet2.Models
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();

        List<Adherent> GetAdherent();

        void EditAdherent(int id, int benevoleId, int numAdherent, DateTime dateInscription, string justifIdentite);

        void CreateAdherent(int id, int benevoleId, int numAdherent, DateTime dateInscription, string justifIdentite);

        int CreateEmployee(int serialNumber, string jobName, DateTime dateOfEmployement, int accountId);
        void EditEmployee(int id, int serialNumber, string jobName, DateTime dateOfEmployement, int accountId);
        void RemoveEmployee(int id);

    }

    

    
}
