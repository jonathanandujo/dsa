using System;
using System.IO;

string directory = "C:\\personal\\dsa\\";
string outputFile = Path.Combine(directory, "ZCodeExtractor\\AllCode.txt");

try
{
    using (StreamWriter writer = new StreamWriter(outputFile))
    {
        foreach (string file in Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories))
        {
            string filePath = Path.GetFullPath(file);
            if (!filePath.Contains("\\obj\\") && !filePath.Contains("\\Debug\\") && !filePath.Contains("\\bin\\"))
            {
                writer.WriteLine($"// {filePath}");
                writer.WriteLine("----------------------------------------");
                string fileContent = File.ReadAllText(filePath);
                writer.WriteLine(fileContent);
                writer.WriteLine("----------------------------------------");
                writer.WriteLine();
            }
        }
    }

    Console.WriteLine("Files listed successfully!");
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}