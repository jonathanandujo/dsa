using Infrastructure.Services;
namespace NuBank;
class Program
{
    public static void Main(string[] args)
    {
        StockTaxService stockTaxService = new();

        // Manual input mode line by line
        if (args.Length == 0)
        {
            string? input;
            while ((input = Console.ReadLine()) != null && input.Trim() != "")
                stockTaxService.ProcessStockOperations(input);
            
            return;
        }

        // File input mode, it contains at least one parameter, the first parameter is the file path
        string filePath = args[0];

        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                //Ignoring empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                stockTaxService.ProcessStockOperations(line);
            }
        }
        else
        {
            ConsoleHelper.WriteError($"File '{filePath}' does not exists");
        }
    }
}