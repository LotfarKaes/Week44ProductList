namespace Week44ProductList.Helpers;

/// <summary>
/// Level 4: Console presentation helpers — especially color highlighting for search hits.
/// </summary>
public static class ConsoleUiHelper
{
    /// <summary>
    /// Prints one line with inverted/highlight colors, then restores the previous colors.
    /// Always restore colors so the rest of the app does not stay yellow/black.
    /// </summary>
    public static void WriteHighlightedLine(string text)
    {
        ConsoleColor previousForeground = Console.ForegroundColor;
        ConsoleColor previousBackground = Console.BackgroundColor;

        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);

        Console.ForegroundColor = previousForeground;
        Console.BackgroundColor = previousBackground;
    }
}