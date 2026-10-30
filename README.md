# Week44ProductList
 
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

## Level 1 – Basic Product List (current code)

### Goal

- Use **at least one class** (`Product`).
- Store products in a **`List<Product>`**.
- **Add** products while the program runs.
- **Show** all products when the user is finished adding.

### Project layout

```
Week2ProductList/
├── Week2ProductList.csproj   # Project file (targets net8.0)
├── Program.cs                # Entry point — user interaction & list
├── Models/
│   └── Product.cs            # Data class for one product
└── README.md                 # This file
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

### What Level 1 deliberately does *not* do yet

- No `ProductManager` class (Level 2).
- No sorting or total price (Level 2).
- No strong validation for empty names or bad prices (Level 3).
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

### Level 2 – Multiple classes & price summary

- New class: **`ProductManager`** with `AddProduct()`, `ShowProducts()`, `CalculateTotal()`.
- Sort products **lowest → highest price** before display.
- Show **total price** at the bottom.

### Level 3 – Error handling & dynamic management

- Reject **empty names**, **invalid** or **negative** prices.
- After showing the list, let the user **add more** products (main menu loop).
- Use **LINQ**: `OrderBy()`, `Sum()`, `Where()`, `FirstOrDefault()`.

### Level 4 – Search & highlight

- Search by **name** or **category**.
- Use **`Console.ForegroundColor`** / **`BackgroundColor`** to highlight matches.

### Level 5 – Advanced system

- Auto **product ID**, **edit**, **delete**.
- **Save/load** with JSON (or text file).
- **Statistics** (min, max, average, count per category).
- **Menu-driven** console UI.

### Level 6 – Bonus (optional)

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
