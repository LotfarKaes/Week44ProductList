using Week44ProductList.Models;
using Week44ProductList.Services;


// =============================================================================
// Checkpoint 2 – Product List Management System
// LEVEL 2 – Multiple Classes & Price Summary
// =============================================================================
// Learning focus for Level 2:
//   • Two classes: Product (data) + ProductManager (behavior)
//   • Methods: AddProduct(), ShowProducts(), CalculateTotal()
//   • Sort by price (low → high) when showing the list
//   • Show total price at the bottom
// =============================================================================

Console.WriteLine("=== Product List Management (Level 2) ===");
Console.WriteLine();

// ProductManager owns the List<Product> now — Program only talks to the manager.
ProductManager manager = new ProductManager();

bool keepAdding = true;

while (keepAdding)
{
    Console.WriteLine("--- Add a new product ---");

    Console.Write("Product category: ");
    string category = Console.ReadLine() ?? string.Empty;

    Console.Write("Product name: ");
    string name = Console.ReadLine() ?? string.Empty;

    Console.Write("Product price: ");
    string priceText = Console.ReadLine() ?? string.Empty;

    // Level 3 will add proper validation; Level 2 keeps the same simple parse as Level 1.
    if (!decimal.TryParse(priceText, out decimal price))
    {
        Console.WriteLine("Could not read price as a number. Using 0 for now (Level 3 will handle this better).");
        price = 0;
    }

    Product newProduct = new Product
    {
        Category = category,
        Name = name,
        Price = price
    };

    // Delegate storage to ProductManager instead of products.Add(...) in Program.
    manager.AddProduct(newProduct);

    Console.WriteLine("Product added!");
    Console.WriteLine();

    Console.Write("Add another product? (y/n): ");
    string again = (Console.ReadLine() ?? "n").Trim().ToLowerInvariant();
    keepAdding = again == "y" || again == "yes";
}

Console.WriteLine();
Console.WriteLine("=== Your product list ===");
Console.WriteLine();

// ShowProducts sorts, prints each line, and shows total price.
manager.ShowProducts();

Console.WriteLine();
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();