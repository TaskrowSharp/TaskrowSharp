using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TaskrowSharp.Utils
{
    internal class Parser
    {
        #region ToInt32

        public static Int32 ToInt32(object value)
        {
            return ToInt32(value, 0);
        }

        public static Int32 ToInt32(object value, Int32 defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            int ret;
            int.TryParse(value.ToString(), out ret);

            return ret;
        }

        public static Int32? ToInt32Nullable(object value)
        {
            return ToInt32Nullable(value, null);
        }

        public static Int32? ToInt32Nullable(object value, Int32? defaultValue)
        {
            int ret;

            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            int.TryParse(value.ToString(), out ret);

            return ret;
        }

        #endregion

        #region ToInt64

        public static Int64 ToInt64(object value)
        {
            return ToInt64(value, 0);
        }

        public static Int64 ToInt64(object value, Int64 defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            Int64 ret;
            Int64.TryParse(value.ToString(), out ret);

            return ret;
        }

        public static Int64? ToInt64Nullable(object value)
        {
            return ToInt64Nullable(value, null);
        }

        public static Int64? ToInt64Nullable(object value, Int64? defaultValue)
        {
            Int64 ret;

            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            Int64.TryParse(value.ToString(), out ret);

            return ret;
        }

        #endregion

        #region ToFloat

        public static float ToFloat(object value)
        {
            return ToFloat(value, 0);
        }

        public static float ToFloat(object value, float defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            float ret;
            float.TryParse(value.ToString(), out ret);

            return ret;
        }

        public static float? ToFloatNullable(object value)
        {
            return ToFloatNullable(value, null);
        }

        public static float? ToFloatNullable(object value, float? defaultValue)
        {
            float ret;

            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            //Números devem vir com ponto separando casas decimais, como vem do JS, etc: 7.23
            float.TryParse(value.ToString(), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US").NumberFormat, out ret);

            return ret;
        }

        #endregion

        #region ToDouble

        public static double ToDouble(object value)
        {
            return ToDouble(value, 0);
        }

        public static double ToDouble(object value, double defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            double ret;
            double.TryParse(value.ToString(), out ret);

            return ret;
        }

        public static double? ToDoubleNullable(object value)
        {
            return ToDoubleNullable(value, null);
        }

        public static double? ToDoubleNullable(object value, double? defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            double ret;
            //Números devem vir com ponto separando casas decimais, como vem do JS, etc: 7.23
            double.TryParse(value.ToString(), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US").NumberFormat, out ret);

            return ret;
        }

        public static double ToDoubleFormatEnglish(object value)
        {
            return ToDoubleFormatEnglish(value, 0);
        }

        public static double ToDoubleFormatEnglish(object value, double defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            double ret;
            double.TryParse(value.ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out ret);

            return ret;
        }

        public static double? ToDoubleFormatEnglishNullable(object value)
        {
            return ToDoubleEnglishNullable(value, null);
        }

        public static double? ToDoubleEnglishNullable(object value, double? defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            double ret;

            double.TryParse(value.ToString(), System.Globalization.NumberStyles.Float, CultureInfo.GetCultureInfo("en-US").NumberFormat, out ret);

            return ret;
        }

        #endregion

        #region ToDecimal

        public static Decimal ToDecimal(object value)
        {
            return ToDecimal(value, 0);
        }

        public static Decimal ToDecimal(object value, Decimal defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            Decimal ret;
            Decimal.TryParse(value.ToString(), out ret);

            return ret;
        }

        public static Decimal? ToDecimalNullable(object value)
        {
            return ToDecimalNullable(value, null);
        }

        public static Decimal? ToDecimalNullable(object value, Decimal? defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            Decimal ret;
            //Números devem vir com ponto separando casas decimais, como vem do JS, etc: 7.23
            Decimal.TryParse(value.ToString(), System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("en-US").NumberFormat, out ret);

            return ret;
        }

        public static Decimal ToDecimalFormatEnglish(object value)
        {
            return ToDecimalFormatEnglish(value, 0);
        }

        public static Decimal ToDecimalFormatEnglish(object value, Decimal defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            Decimal ret;
            Decimal.TryParse(value.ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out ret);

            return ret;
        }

        public static Decimal? ToDecimalFormatEnglishNullable(object value)
        {
            return ToDecimalEnglishNullable(value, null);
        }

        public static Decimal? ToDecimalEnglishNullable(object value, Decimal? defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            Decimal ret;

            Decimal.TryParse(value.ToString(), System.Globalization.NumberStyles.Float, CultureInfo.GetCultureInfo("en-US").NumberFormat, out ret);

            return ret;
        }

        #endregion

        #region ToBool

        public static bool ToBool(object value)
        {
            return ToBool(value, false);
        }

        public static bool ToBool(object value, bool defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            if (string.Equals(value, "1"))
                return true;

            if (string.Equals(value, "0"))
                return false;

            bool ret;
            bool.TryParse(value.ToString(), out ret);

            return ret;
        }

        public static bool? ToBoolNullable(object value)
        {
            return ToBoolNullable(value, null);
        }

        public static bool? ToBoolNullable(object value, bool? defaultValue)
        {
            bool ret;

            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            if (string.Equals(value, "1"))
                return true;

            if (string.Equals(value, "0"))
                return false;

            bool.TryParse(value.ToString(), out ret);

            return ret;
        }

        #endregion

        #region ToUri

        public static Uri ToUri(object value)
        {
            return ToUri(value, null);
        }

        public static Uri ToUri(object value, Uri defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            try
            {
                Uri ret = new Uri(value.ToString());
                return ret;
            }
            catch (System.Exception)
            {
                return defaultValue;
            }
        }

        #endregion

        #region ToDateTime

        public static DateTime ToDateTime(object value)
        {
            return ToDateTime(value, DateTime.MinValue);
        }

        public static DateTime ToDateTime(object value, DateTime defaultValue)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            DateTime ret;
            DateTime.TryParse(value.ToString(), out ret);

            return ret;
        }

        public static DateTime? ToDateTimeNullable(object value)
        {
            return ToDateTimeNullable(value, null);
        }

        public static DateTime? ToDateTimeNullable(object value, DateTime? defaultValue)
        {
            DateTime ret;

            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return defaultValue;

            if (!DateTime.TryParse(value.ToString(), out ret))
                return defaultValue;

            return ret;
        }

        #endregion

        #region Base64

        public static string ToBase64(string plainText)
        {
            if (plainText == null)
                return null;

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string FromBase64(string base64EncodedData, bool completeBase64 = false)
        {
            if (base64EncodedData == null)
                return null;

            if (completeBase64)
            {
                int mod4 = base64EncodedData.Length % 4;
                if (mod4 > 0)
                    base64EncodedData = string.Concat(base64EncodedData, new string('=', 4 - mod4));
            }

            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ToBase64(byte[] byteArray)
        {
            if (byteArray == null)
                return null;

            return System.Convert.ToBase64String(byteArray);
        }

        public static byte[] FromBase64ToByteArray(string base64EncodedData)
        {
            if (base64EncodedData == null)
                return null;

            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return base64EncodedBytes;
        }

        #endregion
    }
}