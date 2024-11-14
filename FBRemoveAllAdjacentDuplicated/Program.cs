using System.Text;
using System.Collections.Generic;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(RemoveAllAdjacentDuplicated("abacaba")); // abacaba
Console.WriteLine(RemoveAllAdjacentDuplicated("AAAABBBAC")); // AC
Console.WriteLine(RemoveAllAdjacentDuplicated("aaabbc")); // c
Console.WriteLine(RemoveAllAdjacentDuplicated("abbbac")); // c

static string RemoveAllAdjacentDuplicated(string s)
{
    if (string.IsNullOrEmpty(s))
        return s;
    StringBuilder result = new();
    char? lastSeen = null;
    foreach (char c in s)
    {
        if ((result.Length > 0 && result[^1] == c) || lastSeen == c)
        {
            if(result.Length > 0)
                result.Length--;
        }
        else
            result.Append(c);
        lastSeen = c;
    }

    return result.ToString();
}