using Week44ProductList.Models;

// =============================================================================
// Checkpoint 2 – Product List Management System
// LEVEL 1 – Basic Product List
// =============================================================================
// Learning focus for Level 1:
//   • One class (Product)
//   • List<T> collection to store many products
//   • Add products while the program runs ("dynamically")
//   • Show every product on screen
// =============================================================================

Console.WriteLine("=== Product List Management (Level 1) ===");
Console.WriteLine();

// List<Product> is a generic collection: it only holds Product objects.
// We create it empty and grow it each time the user adds a product.
List<Product> products = new List<Product>();

// Main loop: keep asking until the user chooses to stop.
bool keepAdding = true;

while (keepAdding)
{
    Console.WriteLine("--- Add a new product ---");

    // Read three values from the user (assignment: Category, Name, Price).
    Console.Write("Product category: ");
    string category = Console.ReadLine() ?? string.Empty;

    Console.Write("Product name: ");
    string name = Console.ReadLine() ?? string.Empty;

    Console.Write("Product price: ");
    string priceText = Console.ReadLine() ?? string.Empty;

    // Level 1 keeps parsing simple. Level 3 adds proper validation and error messages.
    if (!decimal.TryParse(priceText, out decimal price))
    {
        Console.WriteLine("Could not read price as a number. Using 0 for now (Level 3 will handle this better).");
        price = 0;
    }

    // "new Product" creates an object (instance) from the Product class.
    Product newProduct = new Product
    {
        Category = category,
        Name = name,
        Price = price
    };

    // Add pushes the object onto the end of the list.
    products.Add(newProduct);

    Console.WriteLine("Product added!");
    Console.WriteLine();

    Console.Write("Add another product? (y/n): ");
    string again = (Console.ReadLine() ?? "n").Trim().ToLowerInvariant();
    keepAdding = again == "y" || again == "yes";
}

// Present all products after the user is done adding.
Console.WriteLine();
Console.WriteLine("=== Your product list ===");

if (products.Count == 0)
{
    Console.WriteLine("(No products were added.)");
}
else
{
    // foreach walks through every item in the list — no index needed.
    foreach (Product product in products)
    {
        Console.WriteLine(product);
    }

    Console.WriteLine();
    Console.WriteLine($"Total products: {products.Count}");
}

Console.WriteLine();
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();