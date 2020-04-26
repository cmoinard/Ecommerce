namespace Shared.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value) =>
            string.IsNullOrWhiteSpace(value);
        
        public static NonEmptyString ToNonEmpty(this string value) =>
            new NonEmptyString(value);
    }
}