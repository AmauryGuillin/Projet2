using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Projet2.Models.Informations
{
    public class Contact
    {
        public int Id { get; set; }

        //[Required]
        public string EmailAdress { get; set; }

        //[Required]
        public string TelephoneNumber { get; set; }

    }
}
