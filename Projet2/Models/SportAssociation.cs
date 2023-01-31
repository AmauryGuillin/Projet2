namespace Projet2.Models
{
    public class SportAssociation
    {
        public int Id { get; set; }

        public int? GameId { get; set; }
        public virtual Games Game { get; set; }


        public string Logo { get; set; }
        public string Adresse { get; set; }


    }
}
