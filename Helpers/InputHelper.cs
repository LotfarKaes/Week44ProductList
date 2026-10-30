namespace Week44ProductList.Helpers;

/// <summary>
/// Level 3: Central place for reading and validating console input.
/// Keeps Program.cs readable and groups all "error handling" rules in one file.
/// </summary>
public static class InputHelper
{
    /// <summary>
    /// Reads a product name until it is not empty/whitespace (assignment requirement).
    /// </summary>
    public static string ReadProductName()
    {
        while (true)
        {
            Console.Write("Product name: ");
            string? input = Console.ReadLine();
            string name = input?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Product name cannot be empty. Please try again.");
                continue;
            }

            return name;
        }
    }

    /// <summary>
    /// Reads category (optional text — no strict rule in Level 3 PDF, but we trim it).
    /// </summary>
    public static string ReadCategory()
    {
        Console.Write("Product category: ");
        return (Console.ReadLine() ?? string.Empty).Trim();
    }

    /// <summary>
    /// Reads price until it is a valid number and not negative (assignment requirement).
    /// </summary>
    public static decimal ReadValidPrice()
    {
        while (true)
        {
            Console.Write("Product price: ");
            string? input = Console.ReadLine();
            string priceText = input?.Trim() ?? string.Empty;

            if (!decimal.TryParse(priceText, out decimal price))
            {
                Console.WriteLine("Error: Invalid price. Enter a number (e.g. 19.99).");
                continue;
            }

            if (price < 0)
            {
                Console.WriteLine("Error: Price cannot be negative. Please try again.");
                continue;
            }

            return price;
        }
    }

    /// <summary>
    /// Reads y/n (or yes/no) from the user.
    /// </summary>
    public static bool ReadYesNo(string prompt, bool defaultYes = false)
    {
        while (true)
        {
            Console.Write(prompt);
            string answer = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();

            if (answer.Length == 0)
            {
                return defaultYes;
            }

            if (answer is "y" or "yes")
            {
                return true;
            }

            if (answer is "n" or "no")
            {
                return false;
            }

            Console.WriteLine("Please answer y/yes or n/no.");
        }
    }
}