using Week44ProductList.Helpers;
using Week44ProductList.Models;

namespace Week44ProductList.Services;

/// <summary>
/// Manages the product list. Level 4 adds search and highlighted display.
/// </summary>
public class ProductManager
{
    private readonly List<Product> _products = new List<Product>();

    public bool TryAddProduct(string category, string name, decimal price, out string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            errorMessage = "Product name cannot be empty.";
            return false;
        }

        if (price < 0)
        {
            errorMessage = "Price cannot be negative.";
            return false;
        }

        _products.Add(new Product
        {
            Category = category,
            Name = name.Trim(),
            Price = price
        });

        errorMessage = string.Empty;
        return true;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    /// <summary>
    /// Shows all products sorted by price. Pass search matches to highlight those rows (Level 4).
    /// </summary>
    public void ShowProducts(IReadOnlyCollection<Product>? highlightMatches = null)
    {
        if (!_products.Any())
        {
            Console.WriteLine("(No products were added.)");
            return;
        }

        HashSet<Product>? highlightSet = null;
        if (highlightMatches is { Count: > 0 })
        {
            highlightSet = new HashSet<Product>(highlightMatches);
        }

        Console.WriteLine("Products (sorted by price, low → high):");
        Console.WriteLine(new string('-', 40));

        foreach (Product product in _products.OrderBy(p => p.Price))
        {
            string line = $"| {product.Category} | {product.Name} | {product.Price:C}";

            if (highlightSet != null && highlightSet.Contains(product))
            {
                ConsoleUiHelper.WriteHighlightedLine(line);
            }
            else
            {
                Console.WriteLine(line);
            }
        }
        
        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"Total products: {_products.Count}");
        Console.WriteLine($"Total price: {CalculateTotal():C}");
    }

    /// <summary>
    /// Level 4: Partial, case-insensitive search on product name (Where + Contains).
    /// </summary>
    public IReadOnlyList<Product> SearchByName(string searchTerm)
    {
        return _products
            .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Price)
            .ToList();
    }

    /// <summary>
    /// Level 4: Partial, case-insensitive search on category.
    /// </summary>
    public IReadOnlyList<Product> SearchByCategory(string searchTerm)
    {
        return _products
            .Where(p => p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Price)
            .ToList();
    }

    /// <summary>
    /// Runs a search and prints results + full list with matches highlighted in the console.
    /// </summary>
    public void ShowSearchResults(IReadOnlyList<Product> matches, string searchDescription)
    {
        Console.WriteLine();
        Console.WriteLine($"=== Search: {searchDescription} ===");

        if (matches.Count == 0)
        {
            Console.WriteLine("No products found.");
            Console.WriteLine();
            return;
        }

        Console.WriteLine($"{matches.Count} match(es):");
        foreach (Product product in matches)
        {
            ConsoleUiHelper.WriteHighlightedLine(product.ToString());
        }

        Console.WriteLine();
        Console.WriteLine("Full list (matches highlighted):");
        Console.WriteLine();
        ShowProducts(matches);
    }

    public decimal CalculateTotal()
    {
        return _products.Sum(p => p.Price);
    }

    public IReadOnlyList<Product> GetProductsByCategory(string category)
    {
        return _products
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Product? FindByName(string name)
    {
        return _products.FirstOrDefault(p =>
            p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public bool HasProducts => _products.Any();
}