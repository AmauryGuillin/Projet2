using System.Collections.Generic;
using Projet2.Models.UserMessagerie;
namespace Projet2.Models.Messagerie
{
    /// <summary>
    /// This class represents a digital conversation between two users. 
    /// These two users are idenfied by the user account ID.
    /// This class is closely related to the Message class
    /// </summary>
    public class Conversation
    {
        /// <summary>
        /// Gets or sets the conversation id needed by the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the first conversation sender. 
        /// Serves as a foreign key in the database
        /// </summary>
        public int? FirstSenderId { get; set; }

        /// <summary>
        /// Gets or sets the chat sender account
        /// </summary>
        public Account SenderAccount { get; set; }

        /// <summary>
        /// Gets or sets the conversation recipient ID.
        /// Serves as a foreign key in the database
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets the conversation recipient's account
        /// </summary>
        public Account ReceiverAccount { get; set; }

    }
}
