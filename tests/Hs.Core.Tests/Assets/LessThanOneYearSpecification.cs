using Hs.Core.Specification;
using System;
using System.Linq.Expressions;

namespace Hs.Core.Tests.Assets
{
    public sealed class LessThanOneYearSpecification : Specification<Movie>
    {
        private const int Months = 12;

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.ReleaseDate >= DateTime.Now.AddMonths(-Months);
        }
    }
}
