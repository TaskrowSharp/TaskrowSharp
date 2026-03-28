using System;
using System.Text;
using System.Text.RegularExpressions;

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

    public static string ConvertFromBase64ToString(string input, bool fixLength = true)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        if (fixLength)
        {
            int remainder = input.Length % 4;
            if (remainder > 0)
                input += new string('=', 4 - remainder);
        }

        byte[] bytes = Convert.FromBase64String(input);
        return Encoding.UTF8.GetString(bytes);
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

    #region String

    public static string? GetOnlyNumbers(string? text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return Regex.Replace(text!, @"[^0-9]+?", string.Empty);
    }

    #endregion

    #region CNPJ

    private const int CNPJ_LENGTH = 14;
    private static int[] CNPJ_MULTIPLICADOR_1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    private static int[] CNPJ_MULTIPLICADOR_2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

    public static bool IsValidCnpj(string? cnpj)
    {
        if (string.IsNullOrEmpty(cnpj))
            return false;

        var result = cnpj;
        result = Utils.GetOnlyNumbers(result)!;
        if (result.Length != CNPJ_LENGTH)
            return false;
        if (result.Equals("00000000000000") || result.Equals("111111111111") || result.Equals("222222222222") || result.Equals("333333333333")
            || result.Equals("444444444444") || result.Equals("555555555555") || result.Equals("666666666666") || result.Equals("777777777777")
            || result.Equals("888888888888") || result.Equals("999999999999"))
            return false;
        var tempCnpj = result[..12];
        var soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * CNPJ_MULTIPLICADOR_1[i];
        int resto = (soma % 11);
        resto = (resto < 2 ? 0 : 11 - resto);
        var digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * CNPJ_MULTIPLICADOR_2[i];
        resto = (soma % 11);
        resto = (resto < 2 ? 0 : 11 - resto);
        digito += resto.ToString();
        return result.EndsWith(digito);
    }

    public static string? FormatCnpj(string? text, bool returnNullWhenInvalid = false)
    {
        if (string.IsNullOrEmpty(text))
            return null;

        var numbers = GetOnlyNumbers(text)!;
        if (!IsValidCnpj(numbers))
            return (!returnNullWhenInvalid ? numbers : null);

        return Convert.ToUInt64(numbers).ToString(@"00\.000\.000\/0000\-00");
    }

    public static string GenerateFakeCnpj(bool formatted = false)
    {
        var now = DateTime.Now;
        string baseCnpj = $"{now.Hour:00}{now.Minute:00}{now.Second:00}{new Random().Next(0, 99):00}0001";

        var numbers = GetOnlyNumbers(baseCnpj)!;
        if (numbers.Length != 12)
            throw new InvalidOperationException($"Invalid base CNPJ length ({baseCnpj.Length}) expected (12)");
        var tempCnpj = numbers;
        var soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * CNPJ_MULTIPLICADOR_1[i];
        int resto = (soma % 11);
        resto = (resto < 2 ? 0 : 11 - resto);
        var digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * CNPJ_MULTIPLICADOR_2[i];
        resto = (soma % 11);
        resto = (resto < 2 ? 0 : 11 - resto);
        digito += resto.ToString();

        var cnpj = baseCnpj + digito;

        if (!IsValidCnpj(cnpj))
            throw new InvalidOperationException($"Invalid generated CNPJ ({cnpj})");

        if (formatted)
            cnpj = FormatCnpj(cnpj);

        return cnpj;
    }

    #endregion
}
