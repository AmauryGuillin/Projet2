namespace Projet2.Models
{
    public class Benevole
    {
       public int Id { get; set; }  
       public int? AccountId { get; set; }
       public Account Account { get; set; }
       public int NbActionVolunteering { get; set; }
        
    }
}
