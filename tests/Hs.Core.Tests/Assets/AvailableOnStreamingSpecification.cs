using Hs.Core.Specification;
using System;
using System.Linq.Expressions;

namespace Hs.Core.Tests.Assets
{
    public sealed class AvailableOnStreamingSpecification : Specification<Movie>
    {
        private const int MonthsBeforeStreamingIsOut = 6;

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.ReleaseDate <= DateTime.Now.AddMonths(-MonthsBeforeStreamingIsOut);
        }
    }
}
