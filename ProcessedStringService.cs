using StringExtensions;

namespace CS_Tasks
{
    public interface IProcessedStringService
    {
        Task ProcessString(StringProccession processedString);

        HashSet<char> GetNotAllowedChars(StringProccession processedString, string allowedChars);
    }

    public class ProcessedStringService : IProcessedStringService
    {
        RandomService randomService = new RandomService();
        public async Task ProcessString(StringProccession processedString)
        {
            var notAllowedChars = GetNotAllowedChars(processedString, "abcdefghijklmnopqrstuvwxyz");
            if (notAllowedChars.Count > 0)
                throw new Exception($"Characters are not allowed: {string.Join(", ", notAllowedChars)}");
            InvertAndJoin(processedString);
            CountCharacterOccurrences(processedString);
            GetLongestSubstring(processedString, "aeiouy");
            SortString(processedString);
            await TruncateString(processedString);
        }

        public HashSet<char> GetNotAllowedChars(StringProccession processedString, string allowedChars)
        {
            var str = processedString.OriginalString;
            var notAllowedChars = new HashSet<char>();
            foreach (var ch in str)
            {
                if (!allowedChars.Contains(ch))
                    notAllowedChars.Add(ch);
            }
            return notAllowedChars;
        }

        private void InvertAndJoin(StringProccession processedString)
        {
            var str = processedString.OriginalString;
            var isEven = str.Length % 2 == 0;
            if (isEven)
            {
                var firstHalf = str[..(str.Length / 2)];
                var secondHalf = str.Substring(str.Length / 2, str.Length / 2);
                processedString.ProcessedString = new string(firstHalf.Reverse().ToArray()) + new string(secondHalf.Reverse().ToArray());
            }
            else
                processedString.ProcessedString = new string(str.Reverse().ToArray()) + str;
        }

        private void GetLongestSubstring(StringProccession processedString, string boundaries)
        {
            var str = processedString.ProcessedString;
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
                processedString.LongestSubstring = "";
            processedString.LongestSubstring = str.Substring(left, right - left + 1);
        }

        private void CountCharacterOccurrences(StringProccession processedString)
        {
            var str = processedString.ProcessedString;
            var characterOccurences = new Dictionary<char, int>();
            foreach (var ch in str)
            {
                if (characterOccurences.ContainsKey(ch))
                    characterOccurences[ch]++;
                else
                    characterOccurences.Add(ch, 1);
            }
            processedString.CharacterOccurrences = characterOccurences; ;
        }

        private void SortString(StringProccession processedString)
        {
            processedString.SortedString = processedString.ProcessedString.QuickSorted();
        }

        private async Task TruncateString(StringProccession processedString)
        {
            var randInt = 0;
            try
            {
                randInt = await randomService.Next(processedString.ProcessedString.Length - 1);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Connection failed");
                randInt = new Random().Next(processedString.ProcessedString.Length - 1);
            }
            var removedChar = processedString.ProcessedString[randInt];
            processedString.TruncatedString = processedString.ProcessedString.Remove(randInt, 1);
        }
    }
}
