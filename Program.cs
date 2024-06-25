internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter a string: ");
        var input = Console.ReadLine();
        if (input == null)
            return;
        var nonLowercaseEnglish = GetNonLowercaseEnglish(input);
        if (nonLowercaseEnglish.Count() > 0)
        {
            Console.WriteLine($"Invalid characters: {string.Join(", ", nonLowercaseEnglish)}");
            return;
        }
        var processedString = InvertAndJoin(input);
        Console.WriteLine(processedString);
        Console.WriteLine(GenerateOccurencesMessage(processedString));
    }

    private static string InvertAndJoin(string input)
    {
        var isEven = input.Length % 2 == 0;
        if (isEven)
        {
            var firstHalf = input[..(input.Length / 2)];
            var secondHalf = input.Substring(input.Length / 2, input.Length / 2);
            return new string(firstHalf.Reverse().ToArray()) + new string(secondHalf.Reverse().ToArray());
        }
        else
            return new string(input.Reverse().ToArray()) + input;
    }

    private static HashSet<char> GetNonLowercaseEnglish(string input)
    {
        var lowerEnglish = "abcdefghijklmnopqrstuvwxyz";
        var nonLowerCaseEnglish = new HashSet<char>();
        foreach (var ch in input)
        {
            if (!lowerEnglish.Contains(ch))
                nonLowerCaseEnglish.Add(ch);
        }
        return nonLowerCaseEnglish;
    }

    private static Dictionary<char, int> CountCharacterOccurrences(string input)
    {
        var characterOccurences = new Dictionary<char, int>();
        foreach (var ch in input)
        {
            if (characterOccurences.ContainsKey(ch))
                characterOccurences[ch]++;
            else
                characterOccurences.Add(ch, 1);
        }
        return characterOccurences;
    }

    private static string GenerateOccurencesMessage(string input)
    {
        var msg = "Number of occurrences in processed string: ";
        var characterOccurences = CountCharacterOccurrences(input);
        foreach (var character in characterOccurences.Keys)
        {
            msg += $"\n{character}: {characterOccurences[character]}";
        }
        return msg;
    }
}