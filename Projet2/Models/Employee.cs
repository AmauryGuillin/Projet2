using System;

namespace Projet2.Models
{

    /// <summary>
    /// This class represents a employee
    /// It is connected to the account class
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the employee number
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the employee's position
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the employee's hire date
        /// </summary>
        public string DateOfEmployement { get; set; }

        /// <summary>
        /// Gets or sets the subaccount ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        public virtual Account Account { get; set; }


    }
}
