using System;
using System.IO;


namespace PhotoExtractor
{
    public class SdReader
    {
        public static List<FileInfo> ReadSdCameraCard()
        {
            List<FileInfo> cameraFiles = new List<FileInfo>();
            List<DriveInfo> drives = DriveInfo.GetDrives().ToList();
            IEnumerable<DriveInfo> posibleDrives = SearchPosibleDrives(drives);

            foreach (var drive in posibleDrives)
            {
                Console.WriteLine("Nombre: {0} Volumen: {1} Tipo: {2} Formato: {3} Root: {4}", drive.Name, drive.VolumeLabel, drive.DriveType, drive.DriveFormat, drive.RootDirectory);
                IEnumerable<DirectoryInfo> directories = SearchKeyDirectories(drive);

                if (!directories.Any()) return;

                foreach (var directory in directories)
                {
                    Console.WriteLine("Cámara identificada en {0}.", directory.FullName);

                    cameraFiles.AddRange(directory.GetFiles());
                }
            }
            return cameraFiles;
        }

        private static IEnumerable<DirectoryInfo> SearchKeyDirectories(DriveInfo drive)
        {
            return drive.RootDirectory.GetDirectories().Where(d => d.Name == "JPG" || d.Name == "VIDEO");
        }

        private static IEnumerable<DriveInfo> SearchPosibleDrives(List<DriveInfo> drives)
        {
            return drives.Where(d => d.VolumeLabel == "" && d.DriveType == DriveType.Removable);
        }
    }
}