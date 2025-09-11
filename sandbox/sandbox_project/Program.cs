using System;

public class Program
{
    static void Main(string[] args)
    {
        // This project is here for you to use as a "Sandbox" to play around
        // with any code or ideas you have that do not directly apply to
        // one of your projects.

        var numbers = new int[3];

        numbers[0] = 1;
        numbers[1] = 2;
        numbers[2] = 3;
        int[] ages = { 20, 21, 30, 56 };
        // or: var ages = new int[] { 20, 21, 30 };
        string[] meals = { "breakfast", "dinner", "lunch" };
        var songs = new string[3] { "hallelujah", "Roisin", "Diana" };
        var cats = new List<string> { "Bob", "Luno", "Milo" };

        // join the whole array, not a single element
        Console.WriteLine(string.Join(", ", songs));
        Console.WriteLine(string.Join(", ", ages));
        Console.WriteLine(numbers[1]);
        Console.WriteLine(string.Join(", ", cats));
    }
}