using System.Text;

string version = "v1";
Console.WriteLine("V1");
Console.WriteLine(CroppedString("Hello World", 9)); // Hello ...
Console.WriteLine(CroppedString("xxx x", 9)); // xxx x
Console.WriteLine(CroppedString("xxx xxxxx", 7)); // xxx ...
Console.WriteLine(CroppedString("xxx xxxxx", 6)); // ...
Console.WriteLine(CroppedString("123456789", 5)); // 12345 ...
Console.WriteLine(CroppedString("123456789", 9)); // 123456789
Console.WriteLine(CroppedString("1 2 3 4 5 6 7", 5)); // 1 ...
Console.WriteLine(CroppedString("12 34 56 7", 5)); // ...

version = "v2";
Console.WriteLine("V2");
Console.WriteLine(CroppedString("Hello World", 9)); // Hello ...
Console.WriteLine(CroppedString("xxx x", 9)); // xxx x
Console.WriteLine(CroppedString("xxx xxxxx", 7)); // xxx ...
Console.WriteLine(CroppedString("xxx xxxxx", 6)); // ...
Console.WriteLine(CroppedString("123456789", 5)); // 12345 ...
Console.WriteLine(CroppedString("123456789", 9)); // 123456789
Console.WriteLine(CroppedString("1 2 3 4 5 6 7", 5)); // 1 ...
Console.WriteLine(CroppedString("12 34 56 7", 5)); // ...

string CroppedString(string message, int K)
{
    if (version == "v1")
        return CroppedStringv1(message, K);
    else
        return CroppedStringv2(message, K);
}

string CroppedStringv1(string message, int K)
{
    if (message.Length <= K)
        return message;
    
    var words = message.Split(' ');
    var result = new StringBuilder();
    bool needBeCropped = false;
    for(int i = 0; i < words.Length; i++)
    {
        if(i == words.Length - 1 && result.Length + words[i].Length < K)
        {
            result.Append(words[i]);
            break;
        }
        else if(result.Length + words[i].Length < K - 3)
        {
            result.Append(words[i]);
            result.Append(" ");
        }
        else
        {
            needBeCropped = true;
            break;
        }
    }
    
    if (needBeCropped)
        result.Append("...");

    return result.ToString();
}

string CroppedStringv2(string message, int K) //("xxx xxxxx", 6))
{
    if (message.Length <= K)
        return message;
    
    int lastIndexOfSpace = -1;
    for(int i = K - 4; i >= 0; i--)
    {
        if (message[i] == ' ')
            return message.Substring(0, i+1) + "...";
    }

    if (lastIndexOfSpace == -1)
        return "...";
    
    return message.Substring(0, lastIndexOfSpace+1) + "...";
}