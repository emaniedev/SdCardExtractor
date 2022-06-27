

namespace PhotoExtractor
{

    public class Extractor
    {
        public string[]? DirectoryKeyWords { get; private set; }
        public string[]? VolumeNames { get; private set; }
        public string[]? OutputDirectories { get; private set; }
        public bool DryRun { get; set; }

        public List<FileInfo> Files { get; set; }

        public Extractor()
        {
            DirectoryKeyWords = new string[] { "JPG", "MOVIES" };
            VolumeNames = new string[] { "" };
            OutputDirectories = new string[] { Directory.GetCurrentDirectory() };
            DryRun = false;
            Files = new List<FileInfo>();
        }

        public Extractor(ExtractorOptions options)
        {
            DirectoryKeyWords = options.DirectoryKeyWords;
            VolumeNames = options.VolumeNames;
            OutputDirectories = options.OutputDirectories;
            DryRun = options.DryRun;
            Files = new List<FileInfo>();
        }
        public List<FileInfo> ExtractFiles()
        {
            List<FileInfo> cameraFiles = new();
            List<DriveInfo> drives = DriveInfo.GetDrives().ToList();
            IEnumerable<DriveInfo>? posibleDrives = SearchPosibleDrives(drives);

            if (posibleDrives == default) return cameraFiles;
            foreach (var drive in posibleDrives)
            {

                Console.WriteLine("Nombre: {0} Volumen: {1} Tipo: {2} Formato: {3} Root: {4}", drive.Name, drive.VolumeLabel, drive.DriveType, drive.DriveFormat, drive.RootDirectory);

                IEnumerable<DirectoryInfo> directories = SearchKeyDirectories(drive);

                if (directories == null || !directories.Any()) return cameraFiles;

                foreach (var directory in directories)
                {
                    Console.WriteLine("Cámara identificada en {0}.", directory.FullName);

                    cameraFiles.AddRange(directory.GetFiles());
                }
            }
            if (!cameraFiles.Any()) Console.WriteLine("No se han encontrado ficheros");
            cameraFiles.ForEach(f => Console.WriteLine(f.FullName));
            return cameraFiles;
        }

        private IEnumerable<DirectoryInfo>? SearchKeyDirectories(DriveInfo drive)
        {
            if (DirectoryKeyWords == null) return default;
            return drive.RootDirectory.GetDirectories().Where(d =>
                DirectoryKeyWords.Contains(d.Name));
        }
        private IEnumerable<DriveInfo>? SearchPosibleDrives(List<DriveInfo> drives)
        {
            if (VolumeNames == null) return default;
            return drives.Where(d =>
            VolumeNames.Contains(d.VolumeLabel)
            && d.DriveType == DriveType.Removable);
        }


    }
}