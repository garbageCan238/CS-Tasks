using Microsoft.AspNetCore.Mvc;
using StringExtensions;

[ApiController]
[Route("[controller]")]
public class ProcessedStringController : ControllerBase
{
    [HttpGet("{input}")]
    public ActionResult<ProcessedString> GetProductById(string input)
    {
        if (input.GetMissingChars("abcdefghijklmnopqrstuvwxyz").Count > 0)
        {
            return BadRequest("This is a bad request");
        }
        var model = new ProcessedString();
        model.Value = InvertAndJoin(input);
        model.CharacterOccurrencesInfo = model.Value.CountCharacterOccurrences();
        model.LongestSubstring = model.Value.GetLongestSubstring("aeiouy");
        model.SortedString = model.Value.QuickSorted();
        model.TruncatedString = "";
        return model;
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


public class ProcessedString
{
    public string Value { get; set; }

    public Dictionary<char, int> CharacterOccurrencesInfo { get; set; }

    public string LongestSubstring { get; set; }

    public string SortedString { get; set; }

    public string TruncatedString { get; set; }
}