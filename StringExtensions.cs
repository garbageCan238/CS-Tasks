
namespace StringExtensions
{
    public static class StringExtensions
    {

        public static string QuickSorted(this string str)
        {
            var array = str.ToCharArray();
            QuickSort(array, 0, array.Length - 1);
            return new string(array);
        }

        public static string TreeSorted(this string str)
        {
            var array = str.ToCharArray();
            array = TreeSort(array);
            return new string(array);
        }

        public static string GetLongestSubstring(this string str, string boundaries)
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

        public static Dictionary<char, int> CountCharacterOccurrences(this string str)
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

        public static HashSet<char> GetMissingChars(this string str, string mask)
        {
            var missingChars = new HashSet<char>();
            foreach (var ch in str)
            {
                if (!mask.Contains(ch))
                    missingChars.Add(ch);
            }
            return missingChars;
        }

        private static char[] TreeSort(char[] array)
        {
            var tree = new Tree(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                tree.Insert(new Tree(array[i]));
            }
            var list = new List<char>();
            tree.Traverse(c => list.Add(c));
            return list.ToArray();
        }

        private class Tree
        {
            internal Tree? left;
            internal Tree? right;
            internal char key;

            internal Tree(char key)
            {
                this.key = key;
            }

            internal void Insert(Tree tree)
            {
                if (tree.key < key)
                {
                    if (left != null)
                        left.Insert(tree);
                    else 
                        left = tree;
                }
                else
                {
                    if (right != null) 
                        right.Insert(tree);
                    else
                        right = tree;
                }
            }

            internal void Traverse(Action<char> action)
            {
                left?.Traverse(action);
                action(key);
                right?.Traverse(action);
            }
        }

        private static void QuickSort(char[] arr, int low, int high)
        {
            if (low < high)
            {
                var pi = Partition(arr, low, high);

                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        private static int Partition(char[] arr, int low, int high)
        {
            var pivot = arr[high];
            var i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    (arr[j], arr[i]) = (arr[i], arr[j]);
                }
            }

            (arr[high], arr[i + 1]) = (arr[i + 1], arr[high]);
            return i + 1;
        }
    }
}
