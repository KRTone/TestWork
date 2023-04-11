namespace Producer.Domain.Exceptions
{
    public class ProducerDomainException : Exception
    {
        public ProducerDomainException()
        {
        }

        public ProducerDomainException(string message) : base(message)
        {
        }

        public ProducerDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
