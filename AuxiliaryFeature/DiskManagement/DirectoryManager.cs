using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.DiskManagement
{
    public class DirectoryManager
    {
        public List<string> Folders { get; private set; }

        public DirectoryManager()
        {
            Folders = new List<string>();
        }        

        //public void LoadFolders(string rootPath)
        //{
        //    var rootFolders = Directory.GetDirectories(rootPath);
        //    if (rootFolders != null && rootFolders.Length > 0)
        //        AddFoldersToCollection(rootFolders.ToList());
        //    else
        //        return;

        //    foreach (var folder in rootFolders)
        //    {
        //        var subFolders = Directory.GetDirectories(folder);
        //        if (subFolders != null && subFolders.Length > 0)
        //            AddFoldersToCollection(subFolders.ToList());
        //        LoadFolders(folder, folders);
        //    }
        //}

        //void AddFoldersToCollection(List<string> foldersToAdd)
        //{
        //    foreach (var folder in foldersToAdd)
        //    {
        //        if (!_folders.Contains(folder))
        //            _folders.Add(folder);
        //    }
        //}

        //public virtual void AddFolderToCollection(string folder, List<string> folders)
        //{
        //    if (!folders.Contains(folder))
        //        folders.Add(folder);
        //}

        /// <summary>
        /// Method searches for the last directory name in the path
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>name of the last directory in the path</returns>
        public string GetDirectoryNameFromPath(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            return info.Name;
        }

        //public string GetPathWithoutFileName(string fullPath)
        //{
        //    return RemovePath(fullPath, GetFileNameFromPath(fullPath));
        //}  

        //public bool CreateFolder(string path)
        //{
        //    Directory.CreateDirectory(path);
        //    return true;
        //}

        //public bool CreateFolder(string rootPath, string folder)
        //{
        //    var directoryInfo = Directory.CreateDirectory(Path.Combine(rootPath, folder));
        //    return true;
        //}
    }
}
