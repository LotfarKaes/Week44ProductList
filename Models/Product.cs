namespace Week44ProductList.Models;

/// <summary>
/// Level 1: Our first custom class.
/// A "class" is a blueprint for data — here, one item in the product list.
/// </summary>
public class Product
{
    /// <summary>
    /// What kind of product this is (e.g. Electronics, Food).
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The display name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Price in your chosen currency (we use decimal for money — more accurate than double).
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Turns one product into readable text for the console.
    /// override ToString() is a common C# pattern for printing objects.
    /// </summary>
    public override string ToString()
    {
        return $"[{Category}] {Name} — {Price:C}";
    }
}