using System.Collections.Generic;

namespace Hazel.Core.Domain.Messages
{
    /// <summary>
    /// A container for tokens that are added.
    /// </summary>
    /// <typeparam name="U">Type.</typeparam>
    public class MessageTokensAddedEvent<U>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTokensAddedEvent{U}"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="tokens">Tokens.</param>
        public MessageTokensAddedEvent(MessageTemplate message, IList<U> tokens)
        {
            Message = message;
            Tokens = tokens;
        }

        /// <summary>
        /// Gets the Message
        /// Message.
        /// </summary>
        public MessageTemplate Message { get; }

        /// <summary>
        /// Gets the Tokens
        /// Tokens.
        /// </summary>
        public IList<U> Tokens { get; }
    }
}
