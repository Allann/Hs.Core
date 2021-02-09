using System.Linq;

namespace Hs.Core.Concepts
{
    /// <summary>
    /// Provides useful methods for dealing with HashCodes
    /// </summary>
    /// <remarks>
    /// Check Eric Lippert's blog post: https://ericlippert.com/2011/02/28/guidelines-and-rules-for-gethashcode/
    /// </remarks>
    public static class HashCodeHelper
    {
        /// <summary>
        /// Encapsulates an algorithm for generating a hashcode from a series of parameters
        /// </summary>
        /// <param name="parameters">Properties to generate the HashCode from.</param>
        /// <returns>Hash Code</returns>
        public static int Generate(params object?[] parameters)
        {
            unchecked
            {
#nullable disable
                return parameters
                    .Where(param => param != null)
                    .Aggregate(17, (current, param) => current * 29 + param.GetHashCode());
            }
        }
    }

}
