using CS_Tasks;
using StringExtensions;
using NUnit.Framework;

namespace CS_TasksTests
{
    public class ProcessedStringFunctionsTests
    {
        [Test]
        public void InvertAndJoin_abc_cbaabc()
        {
            string original = "abc";
            string result = ProcessedStringService.InvertAndJoin(original);
            Assert.That(result, Is.EqualTo("cbaabc"));
        }

        [Test]
        public void InvertAndJoin_abcd_badc()
        {
            string original = "abcd";
            string result = ProcessedStringService.InvertAndJoin(original);
            Assert.That(result, Is.EqualTo("badc"));
        }

        [Test]
        public void GetNonLowercaseEnglish_whenEnglishLowerAlphabet_emptyHashSet()
        {
            string str = Constans.englishLower;
            HashSet<char> result = ProcessedStringService.GetNonLowercaseEnglish(str);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetNonLowercaseEnglish_whenEnglishUpperAlphabet_HashSetOfEnglishUppers()
        {
            HashSet<char> hashSet = Constans.englishLower.ToUpper().ToHashSet();
            string str = Constans.englishLower.ToUpper();
            HashSet<char> result = ProcessedStringService.GetNonLowercaseEnglish(str);
            Assert.That(result, Is.EqualTo(hashSet));
        }

        [Test]
        public void CountOccurences_whenEnglishLowerAlphabet_EveryCharacterIsOnlyOnce()
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (char c in Constans.englishLower)
            {
                dict[c] = 1;
            }
            string str = Constans.englishLower;
            Dictionary<char, int> result = ProcessedStringService.CountCharacterOccurrences(str);
            Assert.That(result, Is.EqualTo(dict));
        }

        [Test]
        public void CountOccurences_aaaabbbbcccc_3LettersEach4Times()
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (char c in "abc")
            {
                dict[c] = 4;
            }
            string str = "aaaabbbbcccc";
            Dictionary<char, int> result = ProcessedStringService.CountCharacterOccurrences(str);
            Assert.That(result, Is.EqualTo(dict));
        }

        [Test]
        public void GetLongestVowelSubstring_abcde_abcde()
        {
            string str = "abcde";
            string result = ProcessedStringService.GetLongestVowelSubstring(str);
            Assert.That(result, Is.EqualTo("abcde"));
        }

        [Test]
        public void GetLongestVowelSubstring_gfhahtf_a()
        {
            string str = "gfhahtf";
            string result = ProcessedStringService.GetLongestVowelSubstring(str);
            Assert.That(result, Is.EqualTo("a"));
        }

        [Test]
        public void GetLongestVowelSubstring_bcdfgrt_emptyString()
        {
            string str = "bcdfgrt";
            string result = ProcessedStringService.GetLongestVowelSubstring(str);
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetQuickSortedString_reversedEnglishAlphabet_EnglishAlphabet()
        {
            string str = new String(Constans.englishLower.Reverse().ToArray());
            string result = str.QuickSorted();
            Assert.That(result, Is.EqualTo(Constans.englishLower));
        }

        [Test]
        public void GetTreeSortedString_reversedEnglishAlphabet_EnglishAlphabet()
        {
            string str = new String(Constans.englishLower.Reverse().ToArray());
            string result = str.TreeSorted();
            Assert.That(result, Is.EqualTo(Constans.englishLower));
        }
    }
}