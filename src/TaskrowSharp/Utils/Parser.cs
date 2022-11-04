using System;

namespace TaskrowSharp.Utils
{
    public class Parser
    {
        public static DateTime ToDateTimeFromTaskrowDate(string value)
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
    }
}
