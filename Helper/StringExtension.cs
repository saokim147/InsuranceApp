using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Bibliography;
using Org.BouncyCastle.Bcpg.OpenPgp;
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

        //public static string GetCity(this string address)
        //{
        //    return CityRegex().Match(address).Groups[1].Value;
        //}
        //public static string GetDistrict(this string address)
        //{
        //    return DistrictRegex().Match(address).Groups[1].Value;
        //}
        //public static string GetWard(this string address)
        //{
        //    return WardsRegex().Match(address).Groups[1].Value;
        //}

        //[GeneratedRegex(@"Thành phố([^,]+)")]
        //private static partial Regex CityRegex();

        //[GeneratedRegex(@"(Quận|Huyện) ([^,]+)")]
        //private static partial Regex DistrictRegex();
        //[GeneratedRegex(@"Phường([^,]+)")]
        //private static partial Regex WardsRegex();
    }
}
