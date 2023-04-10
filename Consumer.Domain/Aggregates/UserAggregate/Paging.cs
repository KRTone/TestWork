using Consumer.Domain.Exceptions;

namespace Consumer.Domain.Aggregates.UserAggregate
{
    public class Paging
    {
        public Paging(int page, int count)
        {
            if (page < 0 && count < 1)
            {
                throw new ConsumerDomainException(nameof(Paging));
            }
            Page = page;
            Count = count;
        }

        public int Page { get; private set; }
        public int Count { get; private set; }
        public int Skip => Page * Count;
    }
}