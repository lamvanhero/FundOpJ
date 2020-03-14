using OppJar.Common.Enum;

namespace OppJar.Dto
{
    public class QuerySearchDefault
    {
        public virtual string SearchKey { get; set; }

        public virtual int Page { get; set; } = 1;

        public virtual int Size { get; set; } = 10;

        public virtual string SortField { get; set; } = "createdAt";

        public virtual OrderType SortOrder { get; set; } = OrderType.Descending;

        public int GetSkip()
        {
            return (Page - 1) * Size;
        }

        public int GetTake()
        {
            return Size;
        }
    }
}
