using System;
using System.IO;

namespace TaskrowSharp.Utils
{
    internal static class IOHelper
    {
        public static string GetFullPathFromRelative(string relativePath, bool checkIfExists = true)
        {
            string path = relativePath;

            try
            {
                var directoryInfo = new DirectoryInfo(GetExecutingAssemblyFolderPath());
                
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
                throw new TaskrowException($"Error converting relative path: {relativePath}");
            }

            if (path.EndsWith(@"\"))
                path = path.Substring(0, path.Length - 1);

            if (checkIfExists && !File.Exists(path))
                throw new TaskrowException($"File not found: {path}");

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
