using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
    public class InfoPerso
    {
        public int Id { get; set; }

        //[MaxLength(30)]
        //[Required]
        public string LastName { get; set; }

        //[MaxLength(30)]
        //[Required]
        public string FirstName { get; set; }

        //[Required]
        public string Birthday { get; set; }

        //rajouter le genre




    }
}
