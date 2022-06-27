
using PhotoExtractor;

Console.WriteLine("Hola Caracola");


ExtractorOptions opt = new()
{
    DirectoryKeyWords = new[] { "JPG", "VIDEO" },
    VolumeNames = new[] { "" },
    OutputDirectories = new[] { "./Ailenchus" },
    DryRun = true
};

Extractor extractor = new(opt);
List<FileInfo> files = extractor.ExtractFiles();

if (!files.Any())
{
    Console.WriteLine("No se han encontrado ficheros que copiar");
}


Console.WriteLine("Se ha finalizado el programa.");