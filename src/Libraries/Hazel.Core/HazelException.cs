using System;
using System.Runtime.Serialization;

namespace Hazel.Core
{
    /// <summary>
    /// Represents errors that occur during application execution.
    /// </summary>
    [Serializable]
    public class HazelException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HazelException"/> class.
        /// </summary>
        public HazelException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public HazelException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelException"/> class.
        /// </summary>
        /// <param name="messageFormat">The exception message format.</param>
        /// <param name="args">The exception message arguments.</param>
        public HazelException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelException"/> class.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected HazelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HazelException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public HazelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
