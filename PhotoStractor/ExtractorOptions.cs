namespace PhotoExtractor
{
    public class ExtractorOptions
    {
        public string[]? DirectoryKeyWords { get; set; }
        public string[]? VolumeNames { get; set; }
        public string[]? OutputDirectories { get; set; }
        public bool DryRun { get; set; }
    }
}