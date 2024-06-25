internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter a string: ");
        var input = Console.ReadLine();
        if (input == null)
            return;
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
}