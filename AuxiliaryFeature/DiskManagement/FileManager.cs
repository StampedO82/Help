using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.DiskManagement
{
    public static class FileManager
    {
        private static void ChangeExtensionToAllFiles(string rootDirectory, string oldExtension, string newExtension)
        {
            string[] filePaths = System.IO.Directory.GetFiles(rootDirectory, oldExtension, SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                var newFilePath = System.IO.Path.ChangeExtension(filePath, newExtension);
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    var content = sr.ReadToEnd();
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(newFilePath))
                    {
                        sw.WriteLine(content);
                    }
                }
                System.IO.File.Delete(filePath);
            }
        }

        public  static string GetFileNameFromPath(string fullPath, bool withExtension = true)
        {
            if (withExtension)
                return Path.GetFileName(fullPath);
            else
                return Path.GetFileNameWithoutExtension(fullPath);
        }

        public static bool SaveStringToFile(string data, string fullPath, bool overrideFile = true)
        {
            if (string.IsNullOrEmpty(fullPath))
                return false;

            if (string.IsNullOrEmpty(data))
                return false;

            if (File.Exists(fullPath))
            {
                if (overrideFile)
                    File.Delete(fullPath);
                else
                    return false;
            }

            using (TextWriter textWriter = new StreamWriter(fullPath, false, UTF8Encoding.UTF8))
            {
                textWriter.Write(data);
            }

            return File.Exists(fullPath) ? true : false;
        }

        public static string ReadFile(string fileName, string filePath = ".")
        {
            string fullPath = Path.Combine(filePath, fileName);
            StringBuilder sb = new StringBuilder();

            if (File.Exists(fullPath))
            {
                using (TextReader textReader = new StreamReader(fullPath, UTF8Encoding.UTF8, true))
                {
                    sb.AppendLine(textReader.ReadToEnd());
                }
            }
            return sb.ToString();
        }

        public static bool IsFileInUse(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                try
                {
                    //if this does not throw exception then the file is not use by another program
                    using (FileStream fileStream = File.Open(fileFullPath, FileMode.Open, FileAccess.ReadWrite))
                    {
                        if (fileStream == null)
                            return true;
                    }
                    return false;
                }
                catch
                {
                    return true;
                }
            }
            return false;
        }
    }
}
