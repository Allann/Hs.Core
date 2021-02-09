using Hs.Core.Specification;
using System;
using System.Linq.Expressions;

namespace Hs.Core.Tests.Assets
{
    public sealed class MovieDirectedBySpecification : Specification<Movie>
    {
        private readonly string _director;

        public MovieDirectedBySpecification(string director)
        {
            _director = director;
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Director.Name == _director;
        }
    }
}
