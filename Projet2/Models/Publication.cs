using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Projet2.Models
{

    /// <summary>
    /// This class represents a post. 
    /// </summary>
    public class Publication
    {

        /// <summary>
        /// Gets or sets the post identifier needed by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the post title
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the post. 
        /// The different types of post are provided in an enumeration.
        /// </summary>
        public PublicationTypes PublicationType { get; set; }

        /// <summary>
        /// Gets or sets the content of the post
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date the post was created.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the post relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account of the user who created the post.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Gets or sets the path to the publication image
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets an IFormFile object that contains the publication image (not database-mapped).
        /// </summary>
        [NotMapped]
        public IFormFile PubliImage { get; set; }


    }

    /// <summary>
    /// Enumeration that represents the type of the publication.
    /// </summary>
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


}
