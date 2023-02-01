using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


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


        /// <summary>
        /// This method creates an "Adhérent" in the SQL database
        /// </summary>
        /// <param name="id">Adherent's id</param>
        /// <param name="benevoleId">Benevol's id as a foreign key</param>
        /// <param name="numadherent">Adherent's identification number</param>
        /// <param name="inscriptiondate"> Adherent's subscription date</param>
        /// <param name="cotisation">Adherent's contribution</param>
        /// <param name="idDocuments">Adherent's mandatory documents</param>
        /// <returns>adherent.Id</returns>

        public void CreateAdherent(int id, int benevoleId, int numadherent, DateTime inscriptiondate, string idDocuments)
        {
            Adherent adherent = new Adherent() { Id = id, BenevoleId= benevoleId,
                NumAdherent = numadherent, InscriptionDate = inscriptiondate,
                IDDocuments = idDocuments };

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
        /// This method creates a "Benevole" in the SQL database
        /// </summary>
        /// <param name="id"> Id of the "Benevole"</param>
        /// <param name="compteId">Benevole's account id</param>
        /// <param name="nbActionVolunteering">Number of volunteering actions</param>
        /// <returns>benevole.Id</returns>

        public void CreateBenevole(int id, int compteId, int nbActionVolunteering)
        {
            Benevole benevole = new Benevole() { Id = id, AccountId=compteId ,NbActionVolunteering = nbActionVolunteering };

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
        /// This method creates an account and the profile associated
        /// </summary>
        /// <param name="id">account's id</param>
        /// <param name="username">account's username</param>
        /// <param name="password">account's password</param>
        /// <param name="password">account's password</param>
        /// /// <param name="profileid">account's associated profile Id </param>
        /// <returns>account.Id</returns>

        public int CreateAccount(int id, string username, string password, int profileid)/// Dans le create Account, Profile Id= createProfile();
        {
            Account account = new Account() { Id = id, Username = username, Password = password , ProfileId = CreateProfile() };

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


        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public List<Adherent> GetAdherent()
        {
            return _bddContext.Adherents.ToList();
        }

        public void EditAdherent(int id, int benevoleId, int numAdherent, DateTime inscriptiondate, string idDocuments)
        {
            Adherent adherent = _bddContext.Adherents.Find(id);
            if (adherent != null)
            {
                adherent.BenevoleId = benevoleId;
                adherent.NumAdherent = numAdherent;
                adherent.InscriptionDate = inscriptiondate;
                adherent.IDDocuments = idDocuments;
                _bddContext.SaveChanges();
            }
        }

        public void EditAdherent(Adherent adherent)
        {
            _bddContext.Adherents.Update(adherent);
            _bddContext.SaveChanges();
        }

        public void CreateAdherent(Adherent adherent)
        {
            _bddContext.Adherents.Add(adherent);
            _bddContext.SaveChanges();
        }

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

        public List<Account> GetAccounts()
        {
            return _bddContext.Account.ToList();
        }
        public Account Authentificate(string username, string password)
        {
            string motDePasse = EncodeMD5(password);
            Account user = this._bddContext.Account.FirstOrDefault(u => u.Username == username && u.Password == motDePasse);
            return user;
        }
        /// <summary>
        /// This method adds a user's account in the database while encoding the user's password
        /// </summary>
        /// <returns></returns>
        public Account AddAccount(string username, string password)
        {
            string wordpass = EncodeMD5(password);
            //idProfile= CreateProfile();
            Account account = new Account() { Username = username, Password = wordpass, Profile = new Profile() };
            this._bddContext.Account.Add(account);
            this._bddContext.SaveChanges();
            return account;
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


        /// <summary>
        /// This method creates an empty profile returns a profile id 
        /// </summary>
        /// <returns></returns>
        public int CreateProfile()
        {
            Profile profile = Profile.CreateProfile();
            _bddContext.Profils.Add(profile);
            _bddContext.SaveChanges();
            return profile.Id;
        }

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
                profile.ProfilImage = imagePath;
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


        //////////////////TND
    }
}
