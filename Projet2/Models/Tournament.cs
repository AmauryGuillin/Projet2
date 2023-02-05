namespace Projet2.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string FinalScore { get; set; }
        public string Reward { get; set; }

        public int? GameId { get; set; }
        public Game Game { get; set; }

        public int? AssociationActivityId { get; set; }
        public AssociationActivity associationActivity { get; set; }
    }
}
