using System;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models.Messagerie
{

    /// <summary>
    /// This class represents a message in a conversation between two users.
    /// This class is closely related to the Conversation class.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the message identifier needed by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the sender ID of the message.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Gets or sets the account of the sender of the message.
        /// </summary>
        public virtual Account Sender { get; set; }

        /// <summary>
        /// Gets or sets the message recipient ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets the message recipient's account.
        /// </summary>
        public virtual Account Receiver { get; set; }

        /// <summary>
        /// Gets or sets the ID of the conversation to which the message belongs.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? ConversationId { get; set; }

        /// <summary>
        /// Gets or sets the conversation to which the message belongs.
        /// </summary>
        public Conversation UserConversation { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the message.
        /// </summary>
        public string MessageTimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the message content.
        /// </summary>
        [Display(Name="Veuillez Ecrire votre message")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the message has been read or not.
        /// </summary>
        public Boolean isRead { get; set; }
    }
}
