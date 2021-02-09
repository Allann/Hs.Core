using Hs.Core.Specification;
using System;
using System.Linq.Expressions;

namespace Hs.Core.Tests.Assets
{
    public sealed class BestRatedFilmsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Rating >= 4;
        }
    }
}
