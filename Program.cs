using CS_Tasks;
using StringSorting;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter a string: ");
        var input = Console.ReadLine();
        if (input == null || input == "")
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
        Console.WriteLine($"Longest substring = {GetLongestSubstring(processedString, "aeiouy")}");
        Console.WriteLine("Choose sort algorithm : \n1 - quick sort\n2 - tree sort");
        switch (Console.ReadKey().KeyChar)
        {
            case '1':
                Console.WriteLine($"\n{processedString.QuickSorted()}");
                break;
            case '2':
                Console.WriteLine($"\n{processedString.TreeSorted()}");
                break;
            default:
                Console.WriteLine("You haven't chose sort algorithm");
                break;
        }
        int randInt;
        using var randomService = new RandomService();
        try
        {
            randInt = await randomService.Next(processedString.Length - 1);
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("Connection failed");
            randInt = new Random().Next(processedString.Length - 1);
        }
        var removedChar = processedString[randInt];
        Console.WriteLine($"{removedChar} was removed at index {randInt}");
        Console.WriteLine($"Old string: {processedString}");
        processedString = processedString.Remove(randInt, 1);
        Console.WriteLine($"New string: {processedString}");
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

    private static string GetLongestSubstring(string input, string boundaries) 
    {
        var left = -1;
        var right = -1;
        for (var i = 0; i < input.Length; i++)
        {
            if (boundaries.Contains(input[i]))
            {
                left = i;
                break;
            }
        }
        for (var i = input.Length - 1; i >= 0; i--)
        {
            if (boundaries.Contains(input[i]))
            {
                right = i;
                break;
            }
        }
        if (left == -1 || right == -1)
            return "";
        return input.Substring(left, right - left + 1);
    }
    
}