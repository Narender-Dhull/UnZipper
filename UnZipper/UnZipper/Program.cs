using System;
using System.IO;
using System.IO.Compression;

namespace UnZipper
{
    class Program
    {
        public static string ZipPath, ExtractPath, Choice;
        public static int Counter, TotalFileCount = 0;
        static void Main(string[] args)
        {
            try
            {
                GiveOptions();
                if (!string.IsNullOrWhiteSpace(Choice) && Choice.ToUpper().Equals("Y"))
                {
                    Console.WriteLine("Wait...");
                    ExtractPath = Path.GetFullPath(ExtractPath);
                    if (!ExtractPath.EndsWith(Path.DirectorySeparatorChar))
                        ExtractPath += Path.DirectorySeparatorChar;

                    using var archive = ZipFile.OpenRead(ZipPath);
                    TotalFileCount = archive.Entries.Count;
                    Console.WriteLine($"Total {TotalFileCount} found.");
                    foreach (var entry in archive.Entries)
                    {
                        var destinationPath = Path.GetFullPath(Path.Combine(ExtractPath, entry.FullName));
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                        if (destinationPath.StartsWith(ExtractPath, StringComparison.Ordinal))
                            entry.ExtractToFile(destinationPath, true);

                        Counter++;
                        Console.WriteLine($"File completed: {Counter}/{TotalFileCount}");

                    }

                    Console.WriteLine($"All files Done.Unzipped at {ExtractPath}");
                }
                else
                {
                    GiveOptions();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: Error occured, total file completed: {Counter / TotalFileCount}. Error {ex.Message}");
            }
        }

        public static void GiveOptions()
        {
            Console.WriteLine(@"Note: Sample ZIP path - D:\fearFactor.zip");
            Console.WriteLine("Enter source ZIP path: ");
            ZipPath = Console.ReadLine();
            Console.WriteLine(@"Note: Sample ZIP path - D:\");
            Console.WriteLine("Enter destination path: ");
            ExtractPath = Console.ReadLine();
            Console.WriteLine("Start? Y/N");
            Choice = Console.ReadLine();
        }
    }
}
