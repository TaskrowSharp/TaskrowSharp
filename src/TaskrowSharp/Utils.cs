using System;
using System.Text;

namespace TaskrowSharp;

internal static class Utils
{
    #region SpecialCharacters

    public static string RemoveDiacritics(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] > 255)
                sb.Append(text[i]);
            else
                sb.Append(s_Diacritics[text[i]]);
        }

        return sb.ToString();
    }

    private static readonly char[] s_Diacritics = GetDiacritics();

    private static char[] GetDiacritics()
    {
        char[] accents = new char[256];

        for (int i = 0; i < 256; i++)
            accents[i] = (char)i;

        accents[(byte)'á'] = accents[(byte)'à'] = accents[(byte)'ã'] = accents[(byte)'â'] = accents[(byte)'ä'] = 'a';
        accents[(byte)'Á'] = accents[(byte)'À'] = accents[(byte)'Ã'] = accents[(byte)'Â'] = accents[(byte)'Ä'] = 'A';

        accents[(byte)'é'] = accents[(byte)'è'] = accents[(byte)'ê'] = accents[(byte)'ë'] = 'e';
        accents[(byte)'É'] = accents[(byte)'È'] = accents[(byte)'Ê'] = accents[(byte)'Ë'] = 'E';

        accents[(byte)'í'] = accents[(byte)'ì'] = accents[(byte)'î'] = accents[(byte)'ï'] = 'i';
        accents[(byte)'Í'] = accents[(byte)'Ì'] = accents[(byte)'Î'] = accents[(byte)'Ï'] = 'I';

        accents[(byte)'ó'] = accents[(byte)'ò'] = accents[(byte)'ô'] = accents[(byte)'õ'] = accents[(byte)'ö'] = 'o';
        accents[(byte)'Ó'] = accents[(byte)'Ò'] = accents[(byte)'Ô'] = accents[(byte)'Õ'] = accents[(byte)'Ö'] = 'O';

        accents[(byte)'ú'] = accents[(byte)'ù'] = accents[(byte)'û'] = accents[(byte)'ü'] = 'u';
        accents[(byte)'Ú'] = accents[(byte)'Ù'] = accents[(byte)'Û'] = accents[(byte)'Ü'] = 'U';

        accents[(byte)'ç'] = 'c';
        accents[(byte)'Ç'] = 'C';

        accents[(byte)'ñ'] = 'n';
        accents[(byte)'Ñ'] = 'N';

        accents[(byte)'ÿ'] = accents[(byte)'ý'] = 'y';
        accents[(byte)'Ý'] = 'Y';

        return accents;
    }

    public static string RemoveSpecialCharacters(string text, bool allowSpace = false, bool removeDiacritics = false)
    {
        string ret;

        if (allowSpace)
            ret = System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s]+?", string.Empty);
        else
            ret = System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);

        if (removeDiacritics)
            ret = RemoveDiacritics(ret);

        return ret;
    }

    #endregion

    #region Parse

    public static DateTime ParseToDateTimeFromTaskrowDate(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        //format = "/Date(\"2018-03-09T00:00:00.000-03:00\")/"

        string text = value;
        if (text.StartsWith("/Date(\""))
            text = text.Substring(7, text.Length - 10);

        _ = DateTime.TryParse(text, out DateTime ret);

        return ret;
    }

    #endregion

    #region Application

    private static System.Reflection.Assembly staticExecutingAssembly = null;

    public static string GetAppVersion(bool includeBuild = false)
    {
        var version = GetExecutingAssembly().GetName().Version;

        if (!includeBuild)
            return $"{version.Major}.{version.Minor}.{version.Build}";

        return $"{version.Major}.{version.Minor}.{version.Build}{(version.Revision > 0 ? $" Build {version.Revision}" : "")}";
    }

    public static System.Reflection.Assembly GetExecutingAssembly()
    {
        if (staticExecutingAssembly != null)
            return staticExecutingAssembly;

        staticExecutingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        return staticExecutingAssembly;
    }

    #endregion
}
