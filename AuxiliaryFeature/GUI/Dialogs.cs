using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryFeature.GUI
{
    class Dialogs
    {/*
        #region Open/Save dialogs - CHECK, REFACTOR
        public static string OpenFileDialog()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "ModulePackage"; // Default file name
            dlg.DefaultExt = Cu8esFileSystem.PackageExtension; // ".pkg"; // Default file extension
            //dlg.Filter = "Module Package (.pkg)|*.pkg"; // Filter files by extension
            dlg.Filter = "Module Package (" + Cu8esFileSystem.PackageExtension + ")|" + Cu8esFileSystem.AllPackageExtension + ""; // Filter files by extension
            dlg.CheckFileExists = true;
            dlg.InitialDirectory = Cu8esFileSystem.DefaultDirectory;
            dlg.Title = "Select the package you want to import...";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            string filename = string.Empty;
            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dlg.FileName;
            }
            return filename;
        }

        public static string SaveFileDialog()
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Module Package"; // Default file name
            dlg.DefaultExt = Cu8esFileSystem.PackageExtension; ; // ".pkg"; // Default file extension
            //dlg.Filter = "Module Package (.pkg)|*.pkg"; // Filter files by extension
            dlg.Filter = "Module Package (" + Cu8esFileSystem.PackageExtension + ")|" + Cu8esFileSystem.AllPackageExtension + ""; // Filter files by extension
            dlg.InitialDirectory = Cu8esFileSystem.DefaultDirectory;
            dlg.Title = "Select the directory where you want to export...";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            string filename = string.Empty;
            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
            }
            return filename;
        }
        #endregion
        */
    }
}
