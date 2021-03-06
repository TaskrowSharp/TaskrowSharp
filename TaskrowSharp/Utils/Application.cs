﻿namespace TaskrowSharp.Utils
{
    internal static class Application
    {
        private static System.Reflection.Assembly staticExecutingAssembly = null;

        public static string GetAppVersion(bool includeBuild = false)
        {
            var version = Utils.Application.GetExecutingAssembly().GetName().Version;

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

        public static bool IsExceptionWithoutRetry(System.Exception ex)
        {
            return !(ex is TaskrowWebException);
        }
    }
}
