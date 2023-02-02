using System;

namespace Projet2.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public string JobName { get; set; }

        public DateTime DateOfEmployement { get; set; }

        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }


    }
}
