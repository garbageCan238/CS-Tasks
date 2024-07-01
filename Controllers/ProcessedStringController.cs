using CS_Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProcessedStringController : ControllerBase
{
    private readonly IProcessedStringService processedStringService;
    public ProcessedStringController(IProcessedStringService processedStringService)
    {
        this.processedStringService = processedStringService;
    }

    /// <summary>
    /// Gets a proccesed string info.
    /// </summary>
    /// <returns>A proccesed string info</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /ProcessedString?originalString=abcdef
    ///     {
    ///       "originalString": "abcdef",
    ///       "processedString": "cbafed",
    ///       "characterOccurrences": {
    ///         "c": 1,
    ///         "b": 1,
    ///         "a": 1,
    ///         "f": 1,
    ///         "e": 1,
    ///         "d": 1
    ///       },
    ///       "longestSubstring": "afe",
    ///       "sortedString": "abcdef",
    ///       "truncatedString": "bafed"
    ///     }
    ///
    /// </remarks>
    ///  <response code="400">if there are characters not from lower english alphabet</response>
    [HttpGet]
    public async Task<ActionResult<ProcessedStringData>> GetProcessedStringData([FromQuery] string originalString, [FromQuery] string? sortMethod = "quickSort")
    {
        var procesedStringData = new ProcessedStringData(originalString);
        try
        {
            await processedStringService.ProcessString(procesedStringData, sortMethod);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        return procesedStringData;
    }
}


public class ProcessedStringData
{
    public ProcessedStringData(string originalString)
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