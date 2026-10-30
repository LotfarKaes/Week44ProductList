using Week44ProductList.Models;

namespace Week44ProductList.Services;

/// <summary>
/// Level 2: A second class that *manages* products (OOP: separate data from behavior).
/// Product = what one item looks like.
/// ProductManager = operations on the whole list (add, show, calculate).
/// </summary>
public class ProductManager
{
    // Private list: only this class should change the collection directly.
    // "Encapsulation" — outside code calls methods instead of touching the list.
    private readonly List<Product> _products = new List<Product>();

    /// <summary>
    /// Adds one product to the internal list.
    /// </summary>
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    /// <summary>
    /// Prints every product sorted from lowest to highest price,
    /// then prints the total price at the bottom (assignment requirement).
    /// </summary>
    public void ShowProducts()
    {
        if (_products.Count == 0)
        {
            Console.WriteLine("(No products were added.)");
            return;
        }

        Console.WriteLine("Products (sorted by price, low → high):");
        Console.WriteLine(new string('-', 40));

        // Sort for display without changing the order stored in _products:
        // we copy the sorted sequence with OrderBy and loop with foreach.
        IEnumerable<Product> sortedByPrice = _products.OrderBy(p => p.Price);

        foreach (Product product in sortedByPrice)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"Total products: {_products.Count}");
        Console.WriteLine($"Total price: {CalculateTotal():C}");
    }

    /// <summary>
    /// Adds up every product price and returns the sum.
    /// </summary>
    public decimal CalculateTotal()
    {
        decimal total = 0m;

        foreach (Product product in _products)
        {
            total += product.Price;
        }

        return total;
    }
}