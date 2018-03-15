using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Utils
{
    internal static class Application
    {
        private static System.Reflection.Assembly staticExecutingAssembly = null;

        public static string GetAppVersion(bool includeBuild = false)
        {
            var version = Utils.Application.GetExecutingAssembly().GetName().Version;

            if (!includeBuild)
                return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            else
                return string.Format("{0}.{1}.{2}{3}", version.Major, version.Minor, version.Build, (version.Revision > 0 ? string.Format(" Build {0}", version.Revision) : string.Empty));
        }

        public static System.Reflection.Assembly GetExecutingAssembly()
        {
            if (staticExecutingAssembly != null)
                return staticExecutingAssembly;

            staticExecutingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            return staticExecutingAssembly;
        }

        public static bool IsTaskrowEception(System.Exception ex)
        {
            return (ex is TaskrowException);
        }

        public static bool IsWebException(System.Exception ex)
        {
            return (ex is System.Web.HttpException);
        }
    }
}
