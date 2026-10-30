using Week44ProductList.Models;

namespace Week44ProductList.Services;

/// <summary>
/// Manages the product list. Level 3 adds validation on add, more LINQ, and lookup helpers.
/// </summary>
public class ProductManager
{
    private readonly List<Product> _products = new List<Product>();

    /// <summary>
    /// Level 3: Validates before adding. Returns false and an error message if input is invalid.
    /// </summary>
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

    /// <summary>
    /// Adds a product that is already validated (used when InputHelper already checked input).
    /// </summary>
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void ShowProducts()
    {
        if (!_products.Any())
        {
            Console.WriteLine("(No products were added.)");
            return;
        }

        Console.WriteLine("Products (sorted by price, low → high):");
        Console.WriteLine(new string('-', 40));

        foreach (Product product in _products.OrderBy(p => p.Price))
        {
            Console.WriteLine(product);
        }

        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"Total products: {_products.Count}");
        Console.WriteLine($"Total price: {CalculateTotal():C}");
    }

    /// <summary>
    /// Level 3 LINQ refactor: Sum() replaces the manual foreach from Level 2.
    /// </summary>
    public decimal CalculateTotal()
    {
        return _products.Sum(p => p.Price);
    }

    /// <summary>
    /// LINQ Where(): all products in a category (case-insensitive). Used in Level 4 search too.
    /// </summary>
    public IReadOnlyList<Product> GetProductsByCategory(string category)
    {
        return _products
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// LINQ FirstOrDefault(): finds one product by name, or null if none match.
    /// </summary>
    public Product? FindByName(string name)
    {
        return _products.FirstOrDefault(p =>
            p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public bool HasProducts => _products.Any();
}