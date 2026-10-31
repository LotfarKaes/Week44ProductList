# Week 44 – Product List Management System
Console application in C# and .NET 8, built step by step for Checkpoint 2 (Product List Management System).

Implement five levels in this code base project folder. Each level adds new concepts. Commit and push one level at a time so progress can be reviewed.

## Prerequisites

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
2. Open a terminal in this folder (`Week2ProductList`).
3. Run:

```bash
dotnet run
```

To build without running:

```bash
dotnet build
```

---

## Assignment overview (all levels)

| Level | Topic | What you will add |
|-------|--------|-------------------|
| **1** | Basic list | `Product` class, `List<Product>`, add & show products |
| **2** | OOP structure | `ProductManager`, methods, sort by price, total price |
| **3** | Robustness | Validation, loop after listing, LINQ (`OrderBy`, `Sum`, …) |
| **4** | Search | Search by name/category, highlight matches with console colors |
| **5** | Advanced | IDs, edit/delete, save/load (JSON), statistics, menu UI |
| **6** | Bonus (optional) | Pagination, price filter, export report, category colors, etc. |

Initial input for every level (from the PDF):

1. Product **category**
2. Product **name**
3. Product **price**

---

## Level 4 – Search & highlight (current code)

### Goal

- **Search** products by **name** or **category** (assignment part A).
- When products match, **highlight** them with console colors (part B).
- Keep Level 3 validation and the ability to add more products after each session.

### New / updated files

| File | Role |
|------|------|
| `Helpers/ConsoleUiHelper.cs` | Saves/restores colors; prints highlighted lines |
| `Services/ProductManager.cs` | `SearchByName`, `SearchByCategory`, `ShowSearchResults`, `ShowProducts(highlight)` |
| `Helpers/InputHelper.cs` | `ReadSearchTerm`, `ReadSearchOption` |
| `Program.cs` | `RunSearchLoop` after each list |

### How search works

1. After the sorted list is shown, you can answer **y** to “Would you like to search products?”
2. Choose **1** (name) or **2** (category).
3. Type part of a name or category — matching is **case-insensitive** and uses **Contains** (e.g. `phone` finds “Smartphone”).
4. The app prints:
   - Matching rows **highlighted** (dark yellow background).
   - The **full list** again, with the same rows highlighted so you see them in context.

Highlighting uses:

```csharp
Console.BackgroundColor = ConsoleColor.DarkYellow;
Console.ForegroundColor = ConsoleColor.Black;
// print line, then restore previous colors
```

See `ConsoleUiHelper.WriteHighlightedLine`.

### LINQ used in search

```csharp
_products.Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
```

Same pattern for category in `SearchByCategory`.

### How to test Level 4

1. Add e.g. `[Electronics] Phone — 500`, `[Electronics] Case — 20`, `[Food] Apple — 5`.
2. After the list, search **y** → **1** → `phone` → one highlighted match.
3. Search **y** → **2** → `elec` → two matches highlighted on the full list.
4. Search for `xyz` → “No products found.”
5. Answer **n** to stop searching, then **n** to exit (or **y** to add more products).

### Git commit example

```bash
git add Week2ProductList/
git commit -m "Week2 Level 4: search by name/category with console highlight"
git push
```

---

## Level 3 – Error handling & dynamic management (completed earlier)

### Goal

- **Error handling** for empty product name, invalid price, and negative price.
- After the list is shown, the user can **keep managing** (add more products).
- **LINQ** refactors: `OrderBy`, `Sum`, `Where`, `FirstOrDefault`.

### Project layout

```
Week2ProductList/
├── Program.cs                 # Outer + inner loops
├── Models/Product.cs
├── Services/ProductManager.cs # TryAddProduct, LINQ helpers
├── Helpers/
│   └── InputHelper.cs         # Read and validate console input
└── README.md
```

### Program flow (two loops)

1. **Inner loop** — add products until the user answers **n** to “Add another product now?”
2. **Show** the sorted list and total (same as Level 2).
3. **Outer loop** — ask “Continue managing products?” If **y**, go back to step 1. If **n**, exit.

This matches the PDF: you are not forced to quit after one list.

### Validation rules

| Input | Rule | Where |
|--------|------|--------|
| Name | Cannot be empty | `InputHelper.ReadProductName()` |
| Price | Must be a number | `InputHelper.ReadValidPrice()` |
| Price | Cannot be negative | `InputHelper.ReadValidPrice()` + `TryAddProduct()` |

Invalid input shows an **error message** and asks again — the product is **not** added.

### LINQ in `ProductManager`

| Method | LINQ | Purpose |
|--------|------|---------|
| `ShowProducts()` | `OrderBy(p => p.Price)` | Sort for display |
| `CalculateTotal()` | `Sum(p => p.Price)` | Total price |
| `GetProductsByCategory()` | `Where(...)` | Filter by category (Level 4 search builds on this) |
| `FindByName()` | `FirstOrDefault(...)` | Find one product by name |
| `HasProducts` | `Any()` | True if list is not empty |

### How to test Level 3

1. Run `dotnet run`.
2. Press Enter on **name** with nothing typed — you should see an error and get asked again.
3. Enter **abc** as price — error, retry.
4. Enter **-5** — error, retry.
5. Add two valid products, answer **n** to stop adding in this session.
6. Confirm sorted list and total.
7. Answer **y** to “Continue managing?” — add another product and confirm the list updates.
8. Answer **n** to exit.

### Git commit example

```bash
git add Week2ProductList/
git commit -m "Week2 Level 3: validation, manage loop, LINQ Sum/Where/FirstOrDefault"
git push
```

---

## Level 2 – Multiple classes & price summary (completed earlier)

### Goal

- Use **two classes**: `Product` and **`ProductManager`**.
- Move list logic into methods: **`AddProduct()`**, **`ShowProducts()`**, **`CalculateTotal()`**.
- When showing products, sort **lowest → highest price**.
- Display **total price** at the bottom of the list.

### Project layout

```
Week2ProductList/
├── Week2ProductList.csproj
├── Program.cs                 # Reads input; uses ProductManager
├── Models/
│   └── Product.cs             # Data only
├── Services/
│   └── ProductManager.cs      # List + add / show / total
└── README.md
```

### What changed from Level 1?

| Level 1 | Level 2 |
|---------|---------|
| `List<Product>` in `Program.cs` | List hidden inside `ProductManager` |
| `products.Add(...)` in Program | `manager.AddProduct(...)` |
| Print loop in Program | `manager.ShowProducts()` |
| Count only at end | **Sorted** list + **total price** |

### Key C# ideas

#### 1. Separation of concerns

- **`Product`**: holds category, name, price.
- **`ProductManager`**: knows how to store and display many products.

`Program.cs` stays thin: it only reads from the console and calls the manager.

#### 2. Methods on a class

```csharp
manager.AddProduct(newProduct);
manager.ShowProducts();
decimal total = manager.CalculateTotal();
```

Methods are functions that belong to an object (`manager`).

#### 3. Sorting with `OrderBy`

Inside `ShowProducts()`:

```csharp
foreach (Product product in _products.OrderBy(p => p.Price))
```

`OrderBy(p => p.Price)` means “sort by the `Price` property.” This is **LINQ** (we use more of it in Level 3).

#### 4. `CalculateTotal()`

A `foreach` loop adds each `Price` into a `decimal` variable. Using `decimal` avoids rounding errors with money.

### How to test Level 2

1. Run `dotnet run`.
2. Add products with prices out of order (e.g. 50, 10, 30).
3. Answer **n** to stop adding.
4. Confirm lines appear **10 → 30 → 50** (low to high).
5. Confirm **Total price** equals the sum.

### Git commit example

```bash
git add Week2ProductList/
git commit -m "Week2 Level 2: ProductManager, sort by price, total"
git push
```

---

## Level 1 – Basic Product List (completed earlier)

### Goal

- Use **at least one class** (`Product`).
- Store products in a **`List<Product>`**.
- **Add** products while the program runs.
- **Show** all products when the user is finished adding.

### Project layout

```
Week2ProductList/
├── Week2ProductList.csproj   # Project file (targets net8.0)
├── Program.cs
├── Models/Product.cs
├── Services/ProductManager.cs   # Added in Level 2
└── README.md
```

### Key C# ideas (beginner-friendly)

#### 1. Namespace and folders

- `Models/Product.cs` lives in namespace `Week2ProductList.Models`.
- `Program.cs` uses `using Week2ProductList.Models;` so it can use `Product`.

#### 2. Class = blueprint

`Product` has three **properties** (fields with get/set):

- `Category` – string  
- `Name` – string  
- `Price` – decimal (good type for money)

#### 3. List&lt;T&gt;

```csharp
List<Product> products = new List<Product>();
products.Add(newProduct);
```

`List<T>` is a growable collection. `T` is the type — here, `Product`.

#### 4. Object initializer

```csharp
Product newProduct = new Product
{
    Category = category,
    Name = name,
    Price = price
};
```

This sets properties right after creating the object.

#### 5. `foreach`

```csharp
foreach (Product product in products)
{
    Console.WriteLine(product);
}
```

Prints each product. `ToString()` on `Product` controls what appears on screen.

#### 6. `while` loop

The program repeats “add product” until the user answers **n** to “Add another product?”.

### What Level 1 did not include (added in later levels)

- `ProductManager`, sorting, and total price → **Level 2**.
- Strong validation for empty names or bad prices (Level 3).
- No search or colors (Level 4).
- No file save or menu (Level 5).

That keeps Level 1 small and easy to understand.

### How to test Level 1

1. Run `dotnet run`.
2. Add two or three products with different categories and prices.
3. Answer **n** when asked to add another.
4. Confirm all products appear in the summary.

---

## Roadmap: what changes in later levels

### Level 5 – Advanced system (next)

- Auto **product ID**, **edit**, **delete**.
- **Save/load** with JSON (or text file).
- **Statistics** (min, max, average, count per category).
- **Menu-driven** console UI.
- Pagination, price-range filter, sort direction, export TXT, category colors, admin mode, unit tests, etc.

---

## Git workflow (one level per push)

Example after finishing Level 1:

```bash
git add Week2ProductList/
git commit -m "Week2 Level 1: basic Product class and list"
git push
```

Repeat for Level 2, 3, … with clear commit messages.

---

## Troubleshooting

| Problem | What to try |
|--------|-------------|
| `'dotnet' is not recognized` | Install .NET 8 SDK and restart the terminal. |
| Build errors after pulling Level 2+ | Run `dotnet build` and read the line number in the error message. |
| Wrong decimal separator | Depends on regional settings; type `19.99` or `19,99` as your culture expects. |

---
