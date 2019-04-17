#region Usings
using System;
using System.IO;
using System.Linq;
using System.Text;
#endregion

namespace Shield.Framework.Platform.IO
{
    public static class StoragePath
    {
        #region Members
        public const char PathSeparatorChar = '/';

        public const char AltPathSeparatorChar = '\\';

        public const string PathSeparator = "/";

        public const string AltPathSeparator = @"\";

        public const string RootPath = "/";
        #endregion

        #region Methods
        public static string GetName(string path)
        {
            var p = Path.GetFileNameWithoutExtension(path);
            if (string.IsNullOrWhiteSpace(p))
                p = Path.GetDirectoryName(path);

            return p;
        }

        public static string GetFileExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public static string GetDirectory(string path)
        {
            if (path == RootPath)
                return string.Empty;

            var lastIndex = path.LastIndexOf(PathSeparatorChar);
            if (lastIndex > 0)
                return path.Substring(0, lastIndex);

            return lastIndex == 0 ? RootPath : string.Empty;
        }

        public static string GetRootDirectory(string path)
        {
            var dir = GetDirectory(path);
            var arry = dir.Split(PathSeparatorChar);
            return arry.Length == 0 ? string.Empty : CombinePath(arry[1]);
        }

        public static string CombinePath(params string[] paths)
        {
            return paths.Where(p
                                   => !string.IsNullOrWhiteSpace(p) && p != PathSeparator)
                        .Aggregate("",
                                   (current, p)
                                       => $"{current}{PathSeparatorChar}{p}");
        }

        public static string ValidateAndNormalizePath(string path)
        {
            path = path.ToLowerInvariant();
            Throw.If(path.Contains("...")).InvalidOperationException();
            switch (path)
            {
                case null:
                    return null;
                case RootPath:
                case "..":
                case ".":
                    return path;
                case AltPathSeparator:
                    return RootPath;
                default:
                    var uri = new UriBuilder(FilePathToFileUrl(path)).Uri;
                    return uri.LocalPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                              .Replace(AltPathSeparatorChar, PathSeparatorChar);
            }
        }

        public static string CombineAndNormalizePath(params string[] paths)
        {
            return ValidateAndNormalizePath(CombinePath(paths));
        }

        public static bool IsRelativePath(string path)
        {
            return !path.StartsWith(PathSeparatorChar.ToString());
        }

        public static bool IsAbsolutePath(string path)
        {
            return !IsRelativePath(path);
        }

        public static string FilePathToFileUrl(string filePath)
        {
            StringBuilder uri = new StringBuilder();
            foreach (char v in filePath)
            {
                if ((v >= 'a' && v <= 'z')
                    || (v >= 'A' && v <= 'Z')
                    || (v >= '0' && v <= '9')
                    || v == '+'
                    || v == '/'
                    || v == ':'
                    || v == '.'
                    || v == '-'
                    || v == '_'
                    || v == '~'
                    || v > '\xFF')
                {
                    uri.Append(v);
                }
                else if (v == Path.DirectorySeparatorChar || v == Path.AltDirectorySeparatorChar)
                {
                    uri.Append('/');
                }
                else
                {
                    uri.Append(String.Format("%{0:X2}", (int)v));
                }
            }
            if (uri.Length >= 2 && uri[0] == '/' && uri[1] == '/') // UNC path
                uri.Insert(0, "file:");
            else
                uri.Insert(0, "file:///");
            return uri.ToString();
        }
        #endregion
    }
}