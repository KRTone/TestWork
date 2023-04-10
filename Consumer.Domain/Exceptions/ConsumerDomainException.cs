namespace Consumer.Domain.Exceptions
{
    public class ConsumerDomainException : Exception
    {
        public ConsumerDomainException()
        {
        }

        public ConsumerDomainException(string message) : base(message)
        {
        }

        public ConsumerDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
