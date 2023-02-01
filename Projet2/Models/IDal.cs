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

        int CreateBenevole(int accountId);

        void EditBenevole(int accountId, int id, int nbActionVolunteering);

        void RemoveBenevole(int id);






    }




}
