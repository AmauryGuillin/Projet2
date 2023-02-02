using Projet2.Models.Informations;
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
    


        public int CreateBenevole( int accountId )

        {
            int nbActionVolunteering = 0;
            Benevole benevole = new Benevole() { AccountId=accountId ,NbActionVolunteering = nbActionVolunteering };

            _bddContext.Benevoles.Add(benevole);

            _bddContext.SaveChanges();

            return benevole.Id;
        }

        /// <summary>
        /// This method creates a Benevole in the SQL database with a benevole
        /// </summary>
        /// <param name="benevole"></param>
        
        public void CreateBenevole(Benevole benevole)
        {
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
        /// This method modifies a Benevole in the SQL database
        /// </summary>
        /// <param name="accountId"></param>
        public void EditBenevole(int accountId,  int id, int nbActionVolunteering)
        {
            Benevole benevole = _bddContext.Benevoles.Find(id);
            if (benevole != null)
            {
                benevole.AccountId = accountId;
                benevole.NbActionVolunteering = nbActionVolunteering;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// This method removes a Benevole int the SQL database with a Benevole
        /// </summary>
        /// <param name="benevole"></param>
        public void RemoveBenevole(Benevole benevole)
        {
            _bddContext.Benevoles.Remove(benevole);
            _bddContext.SaveChanges();
        }

        public void RemoveBenevole(int id)
        {
            Benevole benevole = _bddContext.Benevoles.Find(id);
            if (benevole != null)
            {
                _bddContext.Benevoles.Remove(benevole);
                _bddContext.SaveChanges();
            }
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
            Account account = new Account() { Username = username, Password = wordpass, Profile = new Profile() , Contact= new Informations.Contact(),infoPerso= new InfoPerso(),Inventory= new Inventory ()};
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


        public int CreateEmployee(string serialNumber, string jobName, DateTime dateOfEmployement, int accountId)
        {
            Employee employee = new Employee() { SerialNumber=serialNumber, JobName=jobName, DateOfEmployement=dateOfEmployement, AccountId=accountId } ;
            _bddContext.Employees.Add(employee);
            _bddContext.SaveChanges();

            return employee.Id;
        }

        public void CreateEmployee(Employee employee)
        {
            _bddContext.Employees.Add(employee);
            _bddContext.SaveChanges();
        }

        public void EditEmployee(int id, string serialNumber, string jobName, DateTime dateOfEmployement, int accountId)
        {
            Employee employee = _bddContext.Employees.Find(id);
            if (employee != null)
            {
                employee.SerialNumber = serialNumber;
                employee.JobName = jobName;
                employee.DateOfEmployement = dateOfEmployement;
                employee.AccountId = accountId;
                _bddContext.SaveChanges();
            }
        }
        public void EditEmployee(Employee employee)
        {
            _bddContext.Employees.Update(employee);
            _bddContext.SaveChanges();
        }

        public void RemoveEmployee(int id)
        {
            Employee employee = _bddContext.Employees.Find(id);
            if (employee != null)
            {
                _bddContext.Employees.Remove(employee);
                _bddContext.SaveChanges();
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            _bddContext.Employees.Remove(employee);
            _bddContext.SaveChanges();

        }

        /// <summary>
        /// This method creates an empty profile returns a profile id 
        /// </summary>
        /// <returns></returns>
        /// 

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
        ////////////////////////////////////INFOPERSO (CONTACT)
        public int EditContact(int id, string email, string tel)
        {
            Contact contact = this._bddContext.Contact.Find(id);
            if (contact != null)
            {
               contact.EmailAdress = email;
                contact.TelephoneNumber = tel;
                _bddContext.SaveChanges();
            }
            return contact.Id;
        }
        public int EditContact(Contact contact)
        {
            _bddContext.Contact.Update(contact);
            _bddContext.SaveChanges();
            return contact.Id;
        }
        public Contact GetContact(int id)
        {
            return this._bddContext.Contact.Find(id);
        }
        public Contact GetContact(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetContact(id);
            }
            return null;
        }


        public List<Contact> GetContacts()
        {
            return _bddContext.Contact.ToList();
        }

        public int CreatePublication(string name, PublicationTypes publicationType, DateTime creationdate, string author, int employeId)
        {
            Publication publication = new Publication() { Name = name, PublicationType = publicationType, Date = creationdate, Author = author, EmployeeId = employeId };
            _bddContext.Publications.Add(publication);
            _bddContext.SaveChanges();

            return publication.Id;

        }

        public void CreatePublication(Publication publication)
        {
            _bddContext.Publications.Add(publication);
            _bddContext.SaveChanges();
        }

        public void EditPublication(int id, string name, PublicationTypes publicationType, DateTime creationdate, string author, int employeId)
        {
            Publication publication = _bddContext.Publications.Find(id);

            if (publication != null)
            {
                publication.Name = name;
                publication.PublicationType = publicationType;
                publication.Date = creationdate;
                publication.Author = author;
                publication.EmployeeId = employeId;
                _bddContext.SaveChanges();
            }
        }

        public void EditPublication(Publication publication)
        {
            _bddContext.Publications.Add(publication);
            _bddContext.SaveChanges();
        }

        public void RemovePublication(int id)
        {
            Publication publication = _bddContext.Publications.Find(id);
            if (publication != null)
            {
                _bddContext.Publications.Remove(publication);
                _bddContext.SaveChanges();
            }
        }

        public void RemovePublication(Publication publication)
        {
            _bddContext.Publications.Remove(publication);
            _bddContext.SaveChanges();

        }

        //////////////////TND

    }
}
