// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.WriteLine(ValidWordAbbreviation("apple","a3e")); //True
Console.WriteLine(ValidWordAbbreviation("apple","a2e")); //False
Console.WriteLine(ValidWordAbbreviation("apple","a0e")); //False
Console.WriteLine(ValidWordAbbreviation("apple","a30")); //False
Console.WriteLine(ValidWordAbbreviation("internationalization","i5a11o1")); //True


bool ValidWordAbbreviation(string word, string abbr) {
	int wordIndex=0, number=0;
	for(int i=0; i<abbr.Length;i++)
	{
		if(char.IsDigit(abbr[i]))
		{
			number = number * 10 + abbr[i] - '0'; //-'0' means will convert the char in ascii to integer
			if(number == 0)
				return false;
		}
		else
		{
			wordIndex += number+1; //1
			number=0;

			if (word.Length < wordIndex || word[wordIndex-1] != abbr[i])
				return false;
		}
	}
	return wordIndex + number == word.Length;
}