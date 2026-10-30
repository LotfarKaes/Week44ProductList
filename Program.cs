using Week44ProductList.Helpers;
using Week44ProductList.Services;

// =============================================================================
// Checkpoint 2 – Product List Management System
// LEVEL 3 – Error Handling & Dynamic Product Management
// =============================================================================
// Learning focus for Level 3:
//   • Validation: empty name, invalid price, negative price
//   • After showing the list, user can add more products (outer loop)
//   • LINQ: OrderBy, Sum, Where, FirstOrDefault (see ProductManager)
// =============================================================================

Console.WriteLine("=== Product List Management (Level 3) ===");
Console.WriteLine();

ProductManager manager = new ProductManager();

// Outer loop: "dynamic product management" — show list, then optionally add more.
bool keepManaging = true;

while (keepManaging)
{
    Console.WriteLine("--- Add products (enter details for each item) ---");
    Console.WriteLine();

    // Inner loop: add one or more products before we show the list again.
    bool addMoreInThisSession = true;

    while (addMoreInThisSession)
    {
        string category = InputHelper.ReadCategory();
        string name = InputHelper.ReadProductName();
        decimal price = InputHelper.ReadValidPrice();

        // Double-check via manager (defense in depth — rules live in one place too).
        if (!manager.TryAddProduct(category, name, price, out string error))
        {
            Console.WriteLine($"Could not add product: {error}");
        }
        else
        {
            Console.WriteLine("Product added!");
        }

        Console.WriteLine();
        addMoreInThisSession = InputHelper.ReadYesNo("Add another product now? (y/n): ");
    }

    Console.WriteLine();
    Console.WriteLine("=== Your product list ===");
    Console.WriteLine();
    manager.ShowProducts();

    Console.WriteLine();
    keepManaging = InputHelper.ReadYesNo(
        "Continue managing products (add more after this list)? (y/n): ");
    Console.WriteLine();
}

Console.WriteLine("Thank you for using Product List Management.");
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();