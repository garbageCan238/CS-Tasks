using CS_Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProcessedStringController : ControllerBase
{
    [HttpGet("{originalString}")]
    public async Task<ActionResult<StringProccession>> GetStringProccessionAsync(string originalString)
    {
        var stringProcession = new StringProccession(originalString);
        var processedStringService = new ProcessedStringService();
        try
        {
            await processedStringService.ProcessString(stringProcession);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return stringProcession;
    }
}


public class StringProccession
{
    public StringProccession(string originalString)
    {
        OriginalString = originalString;
    }

    public string OriginalString { get; set; }

    public string ProcessedString { get; set; }

    public Dictionary<char, int> CharacterOccurrences { get; set; }

    public string LongestSubstring { get; set; }

    public string SortedString { get; set; }

    public string TruncatedString { get; set; }
}