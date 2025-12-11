using Godot;
using System.Collections.Generic;

namespace ProjectSyntax._Game.Scripts.Utils
{
    public static class ContentLoader
    {
        // Loads all resources of type T from a specific directory
        public static List<T> LoadAll<T>(string folderPath) where T : Resource
        {
            var results = new List<T>();
            using var dir = DirAccess.Open(folderPath);

            if (dir != null)
            {
                dir.ListDirBegin();
                string fileName = dir.GetNext();

                while (fileName != "")
                {
                    if (!dir.CurrentIsDir() && (fileName.EndsWith(".tres") || fileName.EndsWith(".remap")))
                    {
                        // In exported games, .tres files become .remap, so we strip that extension to load
                        var cleanPath = folderPath + "/" + fileName.Replace(".remap", "");
                        var resource = GD.Load<T>(cleanPath);
                        if (resource != null)
                        {
                            results.Add(resource);
                        }
                    }
                    fileName = dir.GetNext();
                }
            }
            return results;
        }
    }
}