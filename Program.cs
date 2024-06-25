internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("");
            return;
        }
        var input = args[0];
        var nonLowercaseEnglish = GetNonLowercaseEnglish(input);
        if (nonLowercaseEnglish.Count() > 0)
        {
            Console.WriteLine($"Invalid characters: {string.Join(", ", nonLowercaseEnglish)}");
            return;
        }
        Console.WriteLine(InvertAndJoin(input));
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
}