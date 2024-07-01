using StringExtensions;

namespace CS_Tasks
{
    public interface IProcessedStringService
    {
        Task ProcessString(ProcessedStringData procesedStringData);
    }

    public class ProcessedStringService : IProcessedStringService, IDisposable
    {
        private readonly RandomService randomService = new RandomService();
        public async Task ProcessString(ProcessedStringData procesedStringData)
        {
            var notAllowedChars = GetNotAllowedChars(procesedStringData, "abcdefghijklmnopqrstuvwxyz");
            if (notAllowedChars.Count > 0)
                throw new Exception($"Characters are not allowed: {string.Join(", ", notAllowedChars)}");
            InvertAndJoin(procesedStringData);
            CountCharacterOccurrences(procesedStringData);
            GetLongestSubstring(procesedStringData, "aeiouy");
            SortString(procesedStringData);
            await TruncateString(procesedStringData);
        }

        private HashSet<char> GetNotAllowedChars(ProcessedStringData procesedStringData, string allowedChars)
        {
            var str = procesedStringData.OriginalString;
            var notAllowedChars = new HashSet<char>();
            foreach (var ch in str)
            {
                if (!allowedChars.Contains(ch))
                    notAllowedChars.Add(ch);
            }
            return notAllowedChars;
        }

        private void InvertAndJoin(ProcessedStringData procesedStringData)
        {
            var str = procesedStringData.OriginalString;
            var isEven = str.Length % 2 == 0;
            if (isEven)
            {
                var firstHalf = str[..(str.Length / 2)];
                var secondHalf = str.Substring(str.Length / 2, str.Length / 2);
                procesedStringData.ProcessedString = new string(firstHalf.Reverse().ToArray()) + new string(secondHalf.Reverse().ToArray());
            }
            else
                procesedStringData.ProcessedString = new string(str.Reverse().ToArray()) + str;
        }

        private void GetLongestSubstring(ProcessedStringData procesedStringData, string boundaries)
        {
            var str = procesedStringData.ProcessedString;
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
                procesedStringData.LongestSubstring = "";
            procesedStringData.LongestSubstring = str.Substring(left, right - left + 1);
        }

        private void CountCharacterOccurrences(ProcessedStringData procesedStringData)
        {
            var str = procesedStringData.ProcessedString;
            var characterOccurences = new Dictionary<char, int>();
            foreach (var ch in str)
            {
                if (characterOccurences.ContainsKey(ch))
                    characterOccurences[ch]++;
                else
                    characterOccurences.Add(ch, 1);
            }
            procesedStringData.CharacterOccurrences = characterOccurences; ;
        }

        private void SortString(ProcessedStringData procesedStringData)
        {
            procesedStringData.SortedString = procesedStringData.ProcessedString.QuickSorted();
        }

        private async Task TruncateString(ProcessedStringData procesedStringData)
        {
            int randInt;
            try
            {
                randInt = await randomService.Next(procesedStringData.ProcessedString.Length - 1);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Connection failed");
                randInt = new Random().Next(procesedStringData.ProcessedString.Length - 1);
            }
            procesedStringData.TruncatedString = procesedStringData.ProcessedString.Remove(randInt, 1);
        }

        public void Dispose()
        {
            randomService.Dispose();
        }
    }
}
