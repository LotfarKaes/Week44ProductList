using Week44ProductList.Helpers;
using Week44ProductList.Services;

// =============================================================================
// Checkpoint 2 – Product List Management System
// LEVEL 4 – Search & Highlight Feature
// =============================================================================
// Learning focus for Level 4:
//   • Search by product name or category (LINQ Where + Contains)
//   • Highlight matches with Console.ForegroundColor / BackgroundColor
//   • Still supports Level 3 validation and "continue managing" flow
// =============================================================================

Console.WriteLine("=== Product List Management (Level 4) ===");
Console.WriteLine();

ProductManager manager = new ProductManager();
bool keepManaging = true;

while (keepManaging)
{
    Console.WriteLine("--- Add products (enter details for each item) ---");
    Console.WriteLine();

    bool addMoreInThisSession = true;

    while (addMoreInThisSession)
    {
        string category = InputHelper.ReadCategory();
        string name = InputHelper.ReadProductName();
        decimal price = InputHelper.ReadValidPrice();

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

    // Level 4: optional search loop after the list is shown.
    RunSearchLoop(manager);

    Console.WriteLine();
    keepManaging = InputHelper.ReadYesNo(
        "Continue managing products (add more after this list)? (y/n): ");
    Console.WriteLine();
}

Console.WriteLine("Thank you for using Product List Management.");
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();

/// <summary>
/// Lets the user run one or more searches before going back to "continue managing?".
/// </summary>
static void RunSearchLoop(ProductManager manager)
{
    if (!manager.HasProducts)
    {
        return;
    }

    while (InputHelper.ReadYesNo("Would you like to search products? (y/n): "))
    {
        int searchOption = InputHelper.ReadSearchOption();

        if (searchOption == 1)
        {
            string term = InputHelper.ReadSearchTerm("Enter name (or part of name) to search: ");
            var matches = manager.SearchByName(term);
            manager.ShowSearchResults(matches, $"name contains \"{term}\"");
        }
        else
        {
            string term = InputHelper.ReadSearchTerm("Enter category (or part of category) to search: ");
            var matches = manager.SearchByCategory(term);
            manager.ShowSearchResults(matches, $"category contains \"{term}\"");
        }

        Console.WriteLine();
    }
}