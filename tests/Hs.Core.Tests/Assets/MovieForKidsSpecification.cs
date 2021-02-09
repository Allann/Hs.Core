using Hs.Core.Specification;
using System;
using System.Linq.Expressions;

namespace Hs.Core.Tests.Assets
{
    public sealed class MovieForKidsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.MpaaRating <= MpaaRating.PG;
        }
    }
}
