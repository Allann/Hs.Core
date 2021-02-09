using System.Diagnostics;

namespace Hs.Core.Data
{
    [DebuggerDisplay("Page: {Page}, Skip: {Skip}, Take: {Take}")]
    public class Paging : IPaging
    {
        public Paging(int page, int skip, int take)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (skip < 0)
            {
                skip = 0;
            }

            if (take < 1)
            {
                take = 1;
            }

            if (take > 100)
            {
                take = 100;
            }

            Page = page;
            Skip = skip;
            Take = take;
        }

        public int Page { get; }

        public int Skip { get; }

        public int Take { get; }

        public override string ToString() => $"Page: {Page}, Skip: {Skip}, Take: {Take}";

        public static Paging Default()
        {
            return Setup(1, 0, 10);
        }
        public static Paging Setup(int page, int skip, int take)
        {
            return new Paging(page, skip, take);
        }
    }

}
