using Consumer.Domain.Exceptions;

namespace Consumer.Domain.Aggregates.UserAggregate
{
    public record Paging
    {
        public Paging() : this(1, 20)
        {

        }

        public Paging(int page, int count)
        {
            if (Page < 1 && count < 1)
            {
                throw new ConsumerDomainException(nameof(Paging));
            }
        }

        public int Page { get; private set; }
        public int Count { get; private set; }
        public int Skip => Page * Count;
    }
}