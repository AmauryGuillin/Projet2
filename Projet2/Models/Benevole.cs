namespace Projet2.Models
{
    /// <summary>
    /// This class represents a volonters
    /// It is connected to the account class
    /// </summary>
    public class Benevole
    {
        /// <summary>
        /// Gets or sets the benevole ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the subaccount ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Gets or sets the number of actions performed as a volunteer.
        /// </summary>
        public int NbActionVolunteering { get; set; }
        
    }
}
