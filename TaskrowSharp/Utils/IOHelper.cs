using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Utils
{
    internal static class IOHelper
    {
        public static string GetFullPathFromRelative(string relativePath, bool checkIfExists = false, bool removeDebugPath = false)
        {
            string path = relativePath;

            try
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(GetExecutingAssemblyFolderPath());
                
                if (string.IsNullOrEmpty(path) || path.Equals(".") || path.Equals("\\") || path.Equals("/"))
                    return directoryInfo.FullName;

                if (relativePath.StartsWith("..\\") || relativePath.StartsWith("\\"))
                {
                    // "..\log.txt" -> c:\[parent]\log.txt

                    while (path.StartsWith("..\\"))
                    {
                        directoryInfo = directoryInfo.Parent;
                        path = path.Substring(3);
                    }

                    if (path.StartsWith("\\"))
                        path = path.Substring(1);

                    path = System.IO.Path.Combine(directoryInfo.FullName, path);
                }
                else if (relativePath.IndexOf(":\\") == -1)
                {
                    // "log.txt" -> c:\[parent]\[current_path]\log.txt
                    path = System.IO.Path.Combine(directoryInfo.FullName, relativePath);
                }
            }
            catch (System.Exception)
            {
                throw new System.InvalidOperationException(string.Format("Error converting relative path: {0}", relativePath));
            }

            if (path.EndsWith(@"\"))
                path = path.Substring(0, path.Length - 1);

            if (removeDebugPath)
            {
                string[] debugPaths = new string[] { @"\\bin\\debug", @"\\bin\\release" };
                foreach (var debugPath in debugPaths)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(path, debugPath, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        path = System.Text.RegularExpressions.Regex.Replace(path, debugPath, @"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                }
            }

            if (checkIfExists && !System.IO.File.Exists(path))
                throw new System.InvalidOperationException(string.Format("File not found: {0}", path));

            return path;
        }

        public static string GetExecutingAssemblyFolderPath()
        {
            var codeBase = Utils.Application.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = System.IO.Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            return path;
        }
    }
}
