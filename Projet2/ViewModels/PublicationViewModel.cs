using Projet2.Models;
using System.Collections.Generic;

namespace Projet2.ViewModels
{
    public class PublicationViewModel
    {
        public Publication Publication { get; set; }
        public Employee Employee { get; set; }
        public Account Account { get; set; }
        public List<Publication> Publications { get; set; }
        public bool Authentificate { get; set; }


    }
}
