namespace Projet2.Models
{
    
    public enum Level
    {
        Sauron,
        Galadriel,
        GandalfLeBlanc,
        SamGamgeeLeBrave,
        Orc           
    }
    
    public class Coaching
    {
        public int Id { get; set; }

        public Level Level { get; set; }
    }
}
