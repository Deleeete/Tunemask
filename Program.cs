using System.Drawing;
using KLG.Drawing.ImageReaders;
using KLG.Drawing.ImageSavers;
using Tunemask;

if (args.Length < 3)
{
    Console.WriteLine("Usage: tunemask <radius> <in_dir> <out_dir>");
    Environment.Exit(-1);
}

var imgReader = new SkiaImageReader();
var imgSaver = new SkiaImageSaver();

if (!int.TryParse(args[0], out int radius))
{
    Console.WriteLine("Invalid radius. Expecting integer");
    Environment.Exit(-1);
}
string inDir = args[1];
string outDir = args[2];
string[] file_paths = Directory.GetFiles(inDir);

foreach (var file_path in file_paths)
{
    Color[,] pixels = imgReader.LoadImageFile(file_path);
    Tune.ExpandMask(pixels, radius);
    string fileName = Path.GetFileNameWithoutExtension(file_path);
    string outputFilePath = Path.Combine(outDir, $"{fileName}.png");
    await imgSaver.SaveImageFile(outputFilePath, pixels);
    Console.WriteLine($"[{file_path}] -> [{outputFilePath}]");
}
