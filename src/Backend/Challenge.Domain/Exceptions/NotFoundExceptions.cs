using System.Runtime.Serialization;

namespace Challenge.Domain.Exceptions
{
    public class NotFoundExceptions : BaseException
    {
        public NotFoundExceptions()
        {
        }

        public NotFoundExceptions(string? message) : base(message)
        {
        }

        public NotFoundExceptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
