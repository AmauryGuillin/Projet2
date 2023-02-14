using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Projet2.Models.Informations
{
    /// <summary>
    /// This class represents a user's contact information.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets or sets the contact ID needed by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        [Display(Name = "entrez votre adresse mail: ")]
        public string EmailAdress { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        [Display(Name = "Entrez votre numero de telephone: ")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Le numéro de téléphone doit contenir 10 chiffres !")]
        public string TelephoneNumber { get; set; }

    }
}
