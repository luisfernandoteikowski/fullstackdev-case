using System;
using System.Runtime.Serialization;

namespace GerenciadorEscolar.Api.Exceptions
{
    public class AppException : Exception
    {
        public virtual int StatusCode { get; set; } = 500;
        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AppException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}