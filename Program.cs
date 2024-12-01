using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NYoutubeDLTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Result: {GetFullPath(new FileInfo("youtube-dlp"))}");
            Console.ReadLine();
        }

        internal static string GetFullPath(FileInfo fileInfo)
        {
            Console.WriteLine($"File info: {fileInfo.Name}");

            if (File.Exists(fileInfo.Name))
            {
                Console.WriteLine("File exists");
                return Path.GetFullPath(fileInfo.Name);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Windows");
                string filePath = fileInfo.Name + ".exe";
                Console.WriteLine($"File path: {filePath}");
                if (File.Exists(filePath))
                {
                    Console.WriteLine("File exists with .exe extension");
                    return Path.GetFullPath(filePath);
                }
            }

            string environmentVariable = Environment.GetEnvironmentVariable("PATH");
            Console.WriteLine($"Environment variable: {environmentVariable}");
            if (environmentVariable != null)
            {
                Console.WriteLine("Environment variable is not null");
                foreach (string path in environmentVariable.Split(Path.PathSeparator))
                {
                    Console.WriteLine($"Path: {path}");
                    string fullPath = Path.Combine(path, fileInfo.Name);
                    Console.WriteLine($"Full path: {fullPath}");
                    if (File.Exists(fullPath))
                    {
                        Console.WriteLine("File exists");
                        return fullPath;
                    }

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        Console.WriteLine("Windows");
                        fullPath += ".exe";
                        Console.WriteLine($"Full path: {fullPath}");
                        if (File.Exists(fullPath))
                        {
                            Console.WriteLine("File exists with .exe extension");
                            return fullPath;
                        }
                    }
                }
            }
            return null;
        }
    }
}
