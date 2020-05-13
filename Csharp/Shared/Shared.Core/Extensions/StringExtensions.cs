using System.Collections.Generic;

namespace Shared.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value) =>
            string.IsNullOrWhiteSpace(value);
        
        public static NonEmptyString ToNonEmpty(this string value) =>
            new NonEmptyString(value);

        public static string JoinWith(this IEnumerable<string> values, string separator) =>
            string.Join(separator, values);
    }
}