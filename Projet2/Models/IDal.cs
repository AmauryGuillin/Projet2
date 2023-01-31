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

        int CreateAccount(int id, string username, string password, int profileid);
        //int CreateProfile(int id, string imagePath, string Bio, string games);
        int CreateProfile();
    }

    

    
}
