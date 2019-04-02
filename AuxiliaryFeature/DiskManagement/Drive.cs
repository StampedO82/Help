using System.Runtime.InteropServices;
using System.Text;

namespace AuxiliaryFeature.DiskManagement
{
    public class Drive
    {
        public Drive()
        {
            VolumeName = string.Empty;
            FileSystemName = string.Empty;
            SerialNumber = uint.MinValue;
            DriveLetter = string.Empty;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetVolumeInformation(
            string rootPathName,
            StringBuilder volumeNameBuffer,
            int volumeNameSize,
            ref uint volumeSerialNumber,
            ref uint maximumComponentLength,
            ref uint fileSystemFlags,
            StringBuilder fileSystemNameBuffer,
            int nFileSystemNameSize);
        
        public string VolumeName { get; set; }
        
        public string FileSystemName { get; set; }
        
        public uint SerialNumber { get; set; }
        
        public string DriveLetter { get; set; }
    }
}
