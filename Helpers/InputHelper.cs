namespace Week44ProductList.Helpers;

/// <summary>
/// Central place for reading and validating console input.
/// </summary>
public static class InputHelper
{
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

    public static string ReadCategory()
    {
        Console.Write("Product category: ");
        return (Console.ReadLine() ?? string.Empty).Trim();
    }

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
    /// Level 4: Search text cannot be empty (otherwise every product would "match").
    /// </summary>
    public static string ReadSearchTerm(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            string term = input?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(term))
            {
                Console.WriteLine("Error: Search text cannot be empty. Please try again.");
                continue;
            }

            return term;
        }
    }

    /// <summary>
    /// Level 4: User picks search by name (1) or category (2).
    /// </summary>
    public static int ReadSearchOption()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Search by:");
            Console.WriteLine("  1 - Product name");
            Console.WriteLine("  2 - Category");
            Console.Write("Your choice (1 or 2): ");

            string? input = Console.ReadLine();
            string choice = input?.Trim() ?? string.Empty;

            if (choice == "1")
            {
                return 1;
            }

            if (choice == "2")
            {
                return 2;
            }

            Console.WriteLine("Please enter 1 or 2.");
        }
    }

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