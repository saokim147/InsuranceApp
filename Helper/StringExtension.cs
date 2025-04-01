using System.Globalization;
using System.Text;
namespace InsuranceWebApp.Helper
{
    public static class StringExtension
    {
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string ChangeSortState(this string sortState)
        {
            if (sortState == "default")
                return "asc";
            if (sortState == "asc")
                return "desc";
            return "default";
        }
        public static string ConvertBoolToChar(this bool value, string trueChar, string falseChar)
        {
            return value ? trueChar : falseChar;
        }

     

    }
}
