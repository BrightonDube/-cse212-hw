using System;
using System.Collections.Generic;
using System.Linq;
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
        var set1 = new HashSet<string> { "Bob", "Luno", "Milo" };
        var set2 = new HashSet<string> { "Luno", "Milo", "Zed" };
        var set3 = new HashSet<string> { "Milo", "Zed", "Max" };
        var set4 = set1.Intersect(set2);
        var set5 = set1.Union(set2);
        var set6 = set1.Except(set2);
        var set7 = new HashSet<string>(set1);
        set7.SymmetricExceptWith(set2);
        var set8 = set1.IsSubsetOf(set2);
        var set9 = set1.IsSupersetOf(set2);
        var set10 = set1.IsProperSubsetOf(set2);

        // join the whole array, not a single element
        Console.WriteLine(string.Join(", ", songs));
        Console.WriteLine(string.Join(", ", ages));
        Console.WriteLine(numbers[1]);
        Console.WriteLine(string.Join(", ", cats));
    }
}