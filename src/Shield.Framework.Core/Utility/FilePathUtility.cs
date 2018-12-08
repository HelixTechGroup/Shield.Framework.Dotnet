using System.Linq;
using Shield.Framework.Extensions;
using Shield.Framework.Platform;
using Shield.Framework.Platform.Environment;

namespace Shield.Framework.Utility
{
    public static class FilePathUtility
    {
        #region Methods
        public static string CombinePath(params string[] paths)
        {
            return paths.Where(p 
                => !string.IsNullOrWhiteSpace(p))
                .Aggregate("", (current, p) 
                    => string.Format("{0}{1}{2}", current, PlatformProvider.Environment.PathSeperator, p));
        }

        public static string SetFullFilePath(string entity, string path)
        {
            if (PlatformProvider.Environment.IsUnixBased && entity.Contains(@"\"))
                entity = entity.Replace('\\', '/');

            if (string.IsNullOrWhiteSpace(path))
                return entity;

            if (PlatformProvider.Environment.IsUnixBased && path.Contains(@"\"))
                path = path.Replace('\\', '/');
      
            return path + PlatformProvider.Environment.PathSeperator + entity;
        }
        #endregion
    }
}