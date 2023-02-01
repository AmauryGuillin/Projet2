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
        /// This method creates an Adherent in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="benevoleId"></param>
        /// <param name="numAdherent"></param>
        /// <param name="inscriptiondate"></param>
        /// <param name="contribution"></param>
        /// <param name="idDocuments"></param>
        /// <param name="teamId"></param>
        /// <param name="adhesionId"></param>
        /// <param name="coachingId"></param>
        /// <returns>adherent.Id</returns>
        public int CreateAdherent(int benevoleId, int numAdherent, DateTime inscriptiondate, Double contribution, string idDocuments, int teamId, int adhesionId, int coachingId)
        {
            Adherent adherent = new Adherent()
            {
                BenevoleId = benevoleId,
                NumAdherent = numAdherent,
                InscriptionDate = inscriptiondate,
                Contribution = contribution,
                IDDocuments = idDocuments,
                TeamId = teamId,
                AdhesionId = adhesionId,
                CoachingId = coachingId
            };

            _bddContext.Adherents.Add(adherent);

            _bddContext.SaveChanges();
            return adherent.Id;

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
        public void EditAdherent(int id, int benevoleId, int numAdherent, DateTime inscriptiondate, Double contribution, string idDocuments, int teamId, int adhesionId, int coachingId)
        {
            Adherent adherent = _bddContext.Adherents.Find(id);
            if (adherent != null)
            {
                adherent.BenevoleId = benevoleId;
                adherent.NumAdherent = numAdherent;
                adherent.InscriptionDate = inscriptiondate;
                adherent.Contribution = contribution;
                adherent.IDDocuments = idDocuments;
                adherent.TeamId = teamId;
                adherent.AdhesionId = adhesionId;
                adherent.CoachingId = coachingId;
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
        /// This method removes an Adherent in the SQL database with the Adherent id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAdherent(int id)
        {
            Adherent adherent = _bddContext.Adherents.Find(id);
            if (adherent != null)
            {
                _bddContext.Adherents.Remove(adherent);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method removes an Adherent in the SQL database with an Adherent
        /// </summary>
        /// <param name="adherent"></param>
        public void RemoveAdherent(Adherent adherent)
        {
            _bddContext.Adherents.Remove(adherent);
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
        /// <returns>contribution.Id</returns>
        public int CreateContribution(int id, bool paymentStatus, double totalCount, PrelevementDate prelevementDate, ContributionType contributionType)
        {
            Contribution contribution = new Contribution()
            {
                Id = id,
                PaymentStatus = paymentStatus,
                TotalCount = totalCount,
                PrelevementDate = prelevementDate,
                ContributionType = contributionType
            };

            _bddContext.Contributions.Add(contribution);
            _bddContext.SaveChanges();
            return contribution.Id;

        }
        /// <summary>
        /// This method creates an contribution in the SQL database with a contribution
        /// </summary>
        /// <param name="contribution"></param>
        public void CreateContribution(Contribution contribution)
        {
            _bddContext.Contributions.Add(contribution);
            _bddContext.SaveChanges();
        }
        /// <summary>
        /// This method returns a list that contains all contributions
        /// </summary>
        /// <returns></returns>
        public List<Contribution> GetContributions()
        {
            return _bddContext.Contributions.ToList();
        }

        /// <summary>
        /// This method modifies a Contribution in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentStatus"></param>
        /// <param name="totalCount"></param>
        /// <param name="prelevementDate"></param>
        /// <param name="contributionType"></param>
        public void EditContribution(int id, bool paymentStatus, double totalCount, PrelevementDate prelevementDate, ContributionType contributionType)
        {
            Contribution contribution = _bddContext.Contributions.Find(id);
            if (contribution != null)
            {
                contribution.PaymentStatus = paymentStatus;
                contribution.TotalCount = totalCount;
                contribution.PrelevementDate = prelevementDate;
                contribution.ContributionType = contributionType;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method modifies a contribution in the SQL database with a contribution
        /// </summary>
        /// <param name="contribution"></param>
        public void EditContribution(Contribution contribution)
        {
            _bddContext.Contributions.Update(contribution);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method removes a contribution in the SQL database with the contribution Id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveContribution(int id)
        {
            Contribution contribution = _bddContext.Contributions.Find(id);
            if (contribution != null)
            {
                _bddContext.Contributions.Remove(contribution);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method removes a contribution in the SQL database with a contribution
        /// </summary>
        /// <param name="contribution"></param>
        public void RemoveContribution(Contribution contribution)
        {
            _bddContext.Contributions.Remove(contribution);
            _bddContext.SaveChanges();
        }


        /// <summary>
        /// This method creates an Adhesion in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contributionId"></param>
        /// <param name="Echeance"></param>
        /// <param name="adhesionStatus"></param>
        /// <returns>adhesion.Id</returns>
        public int CreateAdhesion(int id, int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus)
        {
            Adhesion adhesion = new Adhesion()
            {
                Id = id,
                ContributionId = contributionId,
                Echeance = Echeance,
                AdhesionStatus = adhesionStatus
            };

            _bddContext.Adhesions.Add(adhesion);
            _bddContext.SaveChanges();
            return adhesion.Id;
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
        /// This method returns a list that contains all adhesions
        /// </summary>
        /// <returns></returns>
        public List<Adhesion> GetAdhesions()
        {
            return _bddContext.Adhesions.ToList();
        }

        /// <summary>
        /// This method modifies an Adhesion in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contributionId"></param>
        /// <param name="Echeance"></param>
        /// <param name="adhesionStatus"></param>
        public void EditAdhesion(int id, int contributionId, DateTime Echeance, AdhesionStatus adhesionStatus)
        {
            Adhesion adhesion = _bddContext.Adhesions.Find(id);
            if (adhesion != null)
            {
                adhesion.ContributionId = contributionId;
                adhesion.Echeance = Echeance;
                adhesion.AdhesionStatus = adhesionStatus;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method modifies an Adhesion in the SQL database with an Adhesion
        /// </summary>
        /// <param name="adhesion"></param>
        public void EditAdhesion(Adhesion adhesion)
        {
            _bddContext.Adhesions.Update(adhesion);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method removes an Adhesion in the SQL database with the Adhesion id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAdhesion(int id)
        {
            Adhesion adhesion = _bddContext.Adhesions.Find(id);
            if (adhesion != null)
            {
                _bddContext.Adhesions.Remove(adhesion);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method removes an Adhesion in the SQL database with an Adhesion
        /// </summary>
        /// <param name="adhesion"></param>
        public void RemoveAdhesion(Adhesion adhesion)
        {
            _bddContext.Adhesions.Remove(adhesion);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method creates a team in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="gameId"></param>
        /// <param name="creationDate"></param>
        /// <param name="NbAdherent"></param>
        /// <returns>team.Id</returns>
        public int CreateTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent)
        {
            Team team = new Team()
            {
                Id = id,
                Name = name,
                GameId = gameId,
                CreationDate = creationDate,
                NbAdherent = NbAdherent
            };

            _bddContext.Teams.Add(team);
            _bddContext.SaveChanges();
            return team.Id;

        }
        /// <summary>
        /// This method creates a team in the SQL database with a team
        /// </summary>
        /// <param name="team"></param>
        public void CreateTeam(Team team)
        {
            _bddContext.Teams.Add(team);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// his method returns a list that contains all teams
        /// </summary>
        /// <returns></returns>
        public List<Team> GetTeams()
        {
            return _bddContext.Teams.ToList();
        }

        /// <summary>
        /// This method modifies a team in the SQL database with all attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="gameId"></param>
        /// <param name="creationDate"></param>
        /// <param name="NbAdherent"></param>
        public void EditTeam(int id, string name, int gameId, DateTime creationDate, int NbAdherent)
        {
            Team team = _bddContext.Teams.Find(id);
            if (team != null)
            {
                team.Name = name;
                team.GameId = gameId;
                team.CreationDate = creationDate;
                team.NbAdherent = NbAdherent;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method modifies a team in the SQL database with a Team
        /// </summary>
        /// <param name="team"></param>
        public void EditTeam(Team team)
        {
            _bddContext.Teams.Update(team);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// This method removes a team in the SQL database with the Team id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveTeam(int id)
        {
            Team team = _bddContext.Teams.Find(id);
            if (team != null)
            {
                _bddContext.Teams.Remove(team);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method removes a team in the SQL database with a team
        /// </summary>
        /// <param name="team"></param>
        public void RemoveTeam(Team team)
        {
            _bddContext.Teams.Remove(team);
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
