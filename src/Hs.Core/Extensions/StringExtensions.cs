using System.Linq;

namespace Hs.Core.Extensions
{
    public static class StringExtensions
    {
        public static string OnlyNumbers(this string str)
        {
            return new string(str.Where(char.IsDigit).ToArray());
        }
    }
}
