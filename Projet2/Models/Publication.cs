using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Projet2.Models
{

    public enum PublicationTypes
    {
        [Display(Name = "Article Informatif")]
        ArticleInformatif,
        [Display(Name = "Vidéo")]
        Video,
        [Display(Name = "Infographie")]
        Infographie,
        [Display(Name = "Podcast")]
        Podcast,
        [Display(Name = "Newsletter")]
        Newsletter,
        [Display(Name = "FAQ")]
        FAQ,
        [Display(Name = "Résultat")]
        Resultat
    }

    public class Publication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PublicationTypes PublicationType { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        //public string Author { get; set; }
        
        public int? AccountId { get; set; }
        public Account Account { get; set; }

        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile PubliImage { get; set; }
    }
}
