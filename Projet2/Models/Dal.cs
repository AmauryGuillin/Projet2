using System;
using System.Collections.Generic;
using System.Linq;
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
        /// This method creates an "Adhérent" in the SQL database with all attributes
        /// </summary>
        /// <param name="id">Adherent's id</param>
        /// <param name="benevoleId">Benevol's id as a foreign key</param>
        /// <param name="numadherent">Adherent's identification number</param>
        /// <param name="inscriptiondate"> Adherent's subscription date</param>
        /// <param name="cotisation">Adherent's contribution</param>
        /// <param name="idDocuments">Adherent's mandatory documents</param>
        /// <returns>adherent.Id</returns>

        public void CreateAdherent(int id, int benevoleId, int numadherent, DateTime inscriptiondate, Double contibution, string idDocuments, int teamId, int adhesionId, int coachingId)
        {
            Adherent adherent = new Adherent() { Id = id, BenevoleId= benevoleId,
                NumAdherent = numadherent, InscriptionDate = inscriptiondate, Contribution = contibution,
                IDDocuments = idDocuments, TeamId= teamId, AdhesionId= adhesionId, CoachingId= coachingId };

            _bddContext.Adherents.Add(adherent);

            _bddContext.SaveChanges();

        }
        /// <summary>
        /// This method creates an Adherent in the SQL database with an adherent
        /// </summary>
        /// <param name="adherent"></param>
        public void CreateAdherent(Adherent adherent)
        {
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
        /// This method modifies an Adherent in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="benevoleId"></param>
        /// <param name="numAdherent"></param>
        /// <param name="inscriptiondate"></param>
        /// <param name="idDocuments"></param>
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

        /// <summary>
        /// This method modifies an Adherent in the SQL database with an Adherent
        /// </summary>
        /// <param name="adherent"></param>
        public void EditAdherent(Adherent adherent)
        {
            _bddContext.Adherents.Update(adherent);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method creates an Contribution in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="totalCount"></param>
        /// <param name="prelevementDate"></param>
        /// <param name="contributionType"></param>
        public void CreateContribution(int id, bool paymentStatus, double totalCount, PrelevementDate prelevementDate, ContributionType contributionType)
        {
            Contribution contribution = new Contribution() { Id = id, 
                PaymentStatus = paymentStatus, TotalCount = totalCount, 
                PrelevementDate = prelevementDate, ContributionType = contributionType };

            _bddContext.Contributions.Add(contribution);
            _bddContext.SaveChanges();

        }
        /// <summary>
        /// This method creates an contribution in the SQL database with an contribution
        /// </summary>
        /// <param name="contribution"></param>
        public void CreateContribution(Contribution contribution)
        {
            _bddContext.Contributions.Add(contribution);
            _bddContext.SaveChanges();
        }
        /// <summary>
        /// This method returns a list that contains all contribution
        /// </summary>
        /// <returns></returns>
        public List<Contribution> GetContribution()
        {
            return _bddContext.Contributions.ToList();
        }

        /// <summary>
        /// This method creates an Adhesion in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contributionId"></param>
        /// <param name="Echeance"></param>
        public void CreateAdhesion(int id, int contributionId, DateTime Echeance)
        {
            Adhesion adhesion = new Adhesion() { Id = id, 
                ContributionId = contributionId, Echeance = Echeance };

            _bddContext.Adhesions.Add(adhesion);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method creates an Adhesion in the SQL database with an Adhesion
        /// </summary>
        /// <param name="adhesion"></param>
        public void CreateAdhesion(Adhesion adhesion)
        {
            _bddContext.Adhesions.Add(adhesion);
            _bddContext.SaveChanges();
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
        /// This method creates an account
        /// </summary>
        /// <param name="id">account's id</param>
        /// <param name="username">account's username</param>
        /// <param name="password">account's password</param>
        /// <returns>account.Id</returns>

        public int CreateAccount(int id, string username, string password)
        {
            Account account = new Account() { Id=id, Username=username, Password=password };

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

        public Account Authentificate(string username, string password)
        {
            string motDePasse = EncodeMD5(password);
            Account user = this._bddContext.Account.FirstOrDefault(u => u.Username == username && u.Password == motDePasse);
            return user;
        }

        public int AddAccount(string username, string password)
        {
            string wordpass = EncodeMD5(password);
            Account account = new Account() { Username = username, Password = wordpass };
            this._bddContext.Account.Add(account);
            this._bddContext.SaveChanges();
            return account.Id;
        }

        public static string EncodeMD5(string password)
        {
            string selectedPassword = "UserChoice" + password + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(selectedPassword)));
        }



    }
}
