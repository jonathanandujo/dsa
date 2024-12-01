using System.Text;
using System.Collections.Generic;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(RemoveAllAdjacentDuplicated("abacaba")); // abacaba
Console.WriteLine(RemoveAllAdjacentDuplicated("AAAABBBAC")); // AC
Console.WriteLine(RemoveAllAdjacentDuplicated("aaabbc")); // c
Console.WriteLine(RemoveAllAdjacentDuplicated("abbbac")); // c

Console.WriteLine(RemoveAllAdjacentDuplicatesCopilot("abacaba")); // abacaba
Console.WriteLine(RemoveAllAdjacentDuplicatesCopilot("AAAABBBAC")); // AC
Console.WriteLine(RemoveAllAdjacentDuplicatesCopilot("aaabbc")); // c
Console.WriteLine(RemoveAllAdjacentDuplicatesCopilot("abbbac")); // c

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

static string RemoveAllAdjacentDuplicatesCopilot(string s)
{
    if (string.IsNullOrEmpty(s))
        return s;

    Stack<char> stack = new();
    foreach (char c in s)
    {
        if (stack.Count > 0 && stack.Peek() == c)
        {
            stack.Pop();
        }
        else
        {
            stack.Push(c);
        }
    }

    // Construct the result from the stack
    char[] resultArray = stack.ToArray();
    Array.Reverse(resultArray);
    return new string(resultArray);
}

