// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Text;

Console.WriteLine("Hello, World!");

Console.WriteLine(IsStrobogrammatic("69")); // true
Console.WriteLine(IsStrobogrammatic("88")); // true
Console.WriteLine(IsStrobogrammatic("962")); // false

bool IsStrobogrammatic(string num) {
    Hashtable ht = new();
    ht.Add('0', '0');
    ht.Add('1', '1');
    ht.Add('6', '9');
    ht.Add('8', '8');
    ht.Add('9', '6');

    StringBuilder reversed = new();

    foreach(char c in num) {
        if (!ht.ContainsKey(c))
            return false;

        reversed.Insert(0, ht[c]);
    }

    return reversed.ToString() == num;
}