using StringExtensions;

namespace CS_Tasks
{
    public interface IProcessedStringService
    {
        Task ProcessString(ProcessedStringData procesedStringData, string sortMethod);
    }

    public class ProcessedStringService : IProcessedStringService, IDisposable
    {
        private readonly IRandomService randomService;

        private readonly List<String> blackList;

        public ProcessedStringService(IRandomService randomService, List<string> blackList)
        {
            this.randomService = randomService;
            this.blackList = blackList;
        }

        public async Task ProcessString(ProcessedStringData procesedStringData, string sortMethod)
        {
            // validation
            var notAllowedChars = GetNonLowercaseEnglish(procesedStringData.OriginalString);
            if (notAllowedChars.Count > 0)
                throw new ArgumentException($"Characters are not allowed: {string.Join(", ", notAllowedChars)}");
            if (IsInBlackList(procesedStringData.OriginalString))
                throw new ArgumentException($"String {procesedStringData.OriginalString} is in blacklist");

            procesedStringData.ProcessedString = InvertAndJoin(procesedStringData.OriginalString);
            procesedStringData.CharacterOccurrences = CountCharacterOccurrences(procesedStringData.ProcessedString);
            procesedStringData.LongestSubstring = GetLongestVowelSubstring(procesedStringData.ProcessedString);
            procesedStringData.SortedString = SortString(procesedStringData.ProcessedString, sortMethod);
            procesedStringData.TruncatedString = await TruncateString(procesedStringData.ProcessedString);
        }

        public static string SortString(string str, string sortMethod)
        {
            if (sortMethod == "treeSort")
                str = str.TreeSorted();
            else if (sortMethod == "quickSort")
                str = str.QuickSorted();
            else 
                throw new ArgumentException($"{sortMethod} sorting method is not available");
            return str;
        }

        private bool IsInBlackList(string str)
        {
            foreach (string blackListed in blackList)
            {
                if (blackListed == str) 
                    return true;
            }
            return false;
        }

        private static HashSet<char> ValidateString(string str, string allowedChars)
        {
            var notAllowedChars = new HashSet<char>();
            foreach (var ch in str)
            {
                if (!allowedChars.Contains(ch))
                    notAllowedChars.Add(ch);
            }
            return notAllowedChars;
        }

        public static string InvertAndJoin(string str)
        {
            var isEven = str.Length % 2 == 0;
            if (isEven)
            {
                var firstHalf = str[..(str.Length / 2)];
                var secondHalf = str.Substring(str.Length / 2, str.Length / 2);
                return new string(firstHalf.Reverse().ToArray()) + new string(secondHalf.Reverse().ToArray());
            }
            else
                return new string(str.Reverse().ToArray()) + str;
        }
        public static string GetLongestVowelSubstring(string str)
        {
            return GetLongestSubstring(str, Constans.englishVowels);
        }

        public static HashSet<char> GetNonLowercaseEnglish(string str)
        {
            return ValidateString(str, Constans.englishLower);
        }
        private static string GetLongestSubstring(string str, string boundaries)
        {
            var left = -1;
            var right = -1;
            for (var i = 0; i < str.Length; i++)
            {
                if (boundaries.Contains(str[i]))
                {
                    left = i;
                    break;
                }
            }
            for (var i = str.Length - 1; i >= 0; i--)
            {
                if (boundaries.Contains(str[i]))
                {
                    right = i;
                    break;
                }
            }
            if (left == -1 || right == -1)
                return "";
            return str.Substring(left, right - left + 1);
        }

        public static Dictionary<char, int> CountCharacterOccurrences(string str)
        {
            var characterOccurences = new Dictionary<char, int>();
            foreach (var ch in str)
            {
                if (characterOccurences.ContainsKey(ch))
                    characterOccurences[ch]++;
                else
                    characterOccurences.Add(ch, 1);
            }
            return characterOccurences;
        }

        private async Task<string> TruncateString(string str)
        {
            int randInt;
            try
            {
                randInt = await randomService.Next(str.Length - 1);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Connection failed");
                randInt = new Random().Next(str.Length - 1);
            }
            return str.Remove(randInt, 1);
        }
        public void Dispose()
        {
            randomService.Dispose();
        }
    }
}
