using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace InterPrep
{
    class StringEx
    {
        // Q: Retrieves a substring from input string. The substring starts at a specified character position and has a specified length.
        public static string Substring(string input, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            // if startIndex is equal to the length of input string and length is zero.
            if (startIndex == input.Length && length == 0)
                return String.Empty;

            // startIndex or length is less than zero. 
            if (startIndex < 0 || length < 0)
                throw new ArgumentOutOfRangeException();

            // startIndex plus length indicates a position not within input string.
            if (input.Length < startIndex + length)
                throw new ArgumentOutOfRangeException();
         
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                builder.Append(input[i+ startIndex]);
            }

            return builder.ToString();
        }

        // Q: Implement IndexOf function.
        public static int IndexOf(string inputString, string subString)
        {
            if (string.IsNullOrEmpty(inputString) || string.IsNullOrEmpty(subString))
                return -1;

            if (subString.Length > inputString.Length)
                return -1;
            
            int index = -1;
            int i = 0;
            int j = 0;

            while (i < inputString.Length && j < subString.Length)
            {
                if (inputString[i] == subString[j])
                {
                    j++;
                }
                else
                {
                    j = 0;
                }

                i++;

                if (j == subString.Length)
                {
                    index = i - subString.Length;
                    break;
                }
            }

            return index;
        }


        // Q: Write code to find out if two string words are anagrams
        public static bool IsAnagram(string string1, string string2)
        {
            if (string.IsNullOrEmpty(string1) || string.IsNullOrEmpty(string2))
                return false;
            if (string1.Length != string2.Length)
                return false;
            
            char[] array1 = string1.ToCharArray();
            char[] array2 = string2.ToCharArray();

            Array.Sort<char>(array1);
            Array.Sort<char>(array2);

            for (int i = 0; i < string1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        // Q: Write code to find out if two string words are anagrams (without sorting)
        public static bool IsAnagramWithoutSort(string string1, string string2)
        {
            if (string.IsNullOrEmpty(string1) || string.IsNullOrEmpty(string2))
                return false;
            if (string1.Length != string2.Length)
                return false;

            int[] array = new int[26];

            for (int i = 0; i < string1.Length; i++)
            {                
                array[string1[i] - 'a']++;
            }

            for (int j = 0; j < string1.Length; j++)
            {
                array[string2[j] - 'a']--;
            }

            for (int k = 0; k < 26; k++)
            {
                if (array[k] != 0)
                    return false;
            }           

            return true;
        }

        // Q: Input: ["star", "rats", "arc", "arts", "car", "foo"] 
        //    Output: [["star", "rats", "arts"], ["arc", "car"]]
        public static List<List<string>> FindAnagramList(List<string> input)
        {
            Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();

            for (int i = 0; i < input.Count; i++) // ["arst", "arst", "acr", "arst", "acr", "foo"]
            {
                char[] array = input[i].ToCharArray();
                Array.Sort<char>(array); // O (n log n)

                string word = new string(array);

                if (dictionary.ContainsKey(word))
                {
                    dictionary[word].Add(i);
                }                    
                else
                {
                    List<int> list = new List<int>();
                    list.Add(i);
                    dictionary.Add(word, list);
                }
            }
            
            List<List<string>> result = new List<List<string>>();
                        
            foreach(KeyValuePair<string, List<int>> entry in dictionary)
            {
                if (entry.Value.Count > 1)
                {
                    List<string> strList = new List<string>();

                    for (int i = 0; i < entry.Value.Count; i++)
                        strList.Add(input[entry.Value[i]]);

                    result.Add(strList);
                }
            }

            return result;
        }

        // Q: Write a method that takes a camelCase string as a parameter and returns underscore_case as output. Assume that input can be null or empty.
        public static string ToUnderscore(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return null;
            }

            string result = null;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    if (i > 0)
                        result += '_';
                    result += char.ToLower(input[i]);
                }
                else
                {
                    result += input[i];
                }
            }

            return result;
        }

        // Q: Given 2 strings, a and b, return the number of the positions where they contain the same length 2 substring. 
        // So "xxcaazz" and "xxbaaz" yields 3, since the "xx", "aa", and "az" substrings appear in the same place in both strings.
        public static int GetTwoLengthSubstringNumber(string str1, string str2)
        {
            if (String.IsNullOrEmpty(str1) || String.IsNullOrEmpty(str2))
                return 0;
            
            if (str1.Length < 2 || str2.Length < 2)
                return 0;

            int count = 0;

            for (int i = 0; i < str1.Length-1 && i < str2.Length-1; i++)
            {
                if (str1[i] == str2[i] && str1[i+1] == str2[i+1])
                {
                    count++;
                }
            }

            return count;
        }

        // Q: Write a function to check the given string is palindrome or not
        public bool IsPalindrome(string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;

            int length = str.Length;

            str = str.ToLower();

            for (int i = 0; i < (length / 2); i++)
            {
                if (str[i] != str[length - 1 - i])
                    return false;
            }

            return true;
        }

        public static int atoi(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            int num = 0;
            int index = 0;
            bool isNegative = false;

            if (str[0] == '-')
            {
                isNegative = true;
                index++;
            }

            while (index < str.Length)
            {
                num *= 10;
                num += str[index] - '0';
                index++;
            }

            if (isNegative)
                num *= -1;

            return num;
        }

        // Q: Given a string, find the start position of the largest block of repeated charactes.
        // "abeeeeefeegkkkkkkkkf" returns 11.
        public static int FindLargetstBlock(string input)
        {
            if (string.IsNullOrEmpty(input))
                return -1;
            
            if (input.Length == 1)
                return 0;

            int index = 0;
            int runner = 1;

            int count = 1;
            int maxCount = 0;
            int maxCountStartIndex = 0;

            while (runner < input.Length)
            {
                if (input[runner] == input[index])
                {
                    count++;
                }
                else
                {                    
                    index = runner;
                    count = 1;
                }

                runner++;

                if (count > maxCount)
                {
                    maxCount = count;
                    maxCountStartIndex = index;
                }
            }

            return maxCountStartIndex;
        }

        // Q: Write run-length encoding function
        // aaaaabbbbbbbcccbbbbcccdddddaaaa -> a5b7c3b4c3d5a4
        public static string RunLengthEncoding(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            StringBuilder result = new StringBuilder();
            int count = 1;
            char prev = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == prev)
                {
                    count++;
                }
                else
                {
                    result.Append(prev).Append(count);
                    count = 1;
                }

                prev = input[i];
            }

            result.Append(prev).Append(count);

            return result.ToString();
        }

        // Q: Write run-length decoding function
        // a5b7c3b4c3d5a4 -> aaaaabbbbbbbcccbbbbcccdddddaaaa
        public static string RunLengthDecoding(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            StringBuilder result = new StringBuilder();
            
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (Char.IsLetter(c))
                {
                    result.Append(c);
                    i++;
                }
                
                if (Char.IsNumber(input[i]))
                {
                    for (int j = 0; j < input[i] - '0' - 1; j++)
                    {
                        result.Append(c);
                    }
                }
            }

            return result.ToString();
        }
    }

    // Permutation of a String
    // "dog” as input, then it will print out the strings "god”, "gdo”, "odg”, "ogd”, "dgo”, and "dog"
    class Permutations
    {
        private bool[] used;
        private StringBuilder output = new StringBuilder();
        private readonly string input;

        public Permutations(string str)
        {
            input = str;
            used = new bool[input.Length];
        }

        public void Permute()
        {
            if (output.Length == input.Length)
            {
                Console.WriteLine(output.ToString());
                return;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (used[i]) continue;
                output.Append(input[i]);
                used[i] = true;
                Permute();
                used[i] = false;
                output.Length = output.Length - 1;
            }
        }
    }

    // Combinations of a String
    public class Combinations
    {
        private StringBuilder output = new StringBuilder();
        private readonly string input;

        public Combinations(string str)
        {
            input = str;
        }

        public void Combine()
        {
            Combine(0);
        }

        private void Combine(int start)
        {
            for (int i = start; i < input.Length; i++)
            {
                output.Append(input[i]);
                Console.WriteLine(output);
                if (i < input.Length)
                    Combine(i + 1);
                output.Length = output.Length - 1;
            }
        }
    }

    // Q: Given an integer, return all sequences of numbers that sum to it. (Example: 3 -> (1, 2), (2, 1), (1, 1, 1))
    public class DecomposeNumber
    {
        private static void Print(int[] values, int n) {
            for (int i = 0; i < n; i++) {
                Console.Write(" " + values[i]);
            }
            Console.WriteLine();
        }

        private static void Decompose(int x, int[] values, int index)
        {
            if (x == 0)
            {
                Print(values, index);
                return;
            }

            for (int i = 1; i < x; i++)
            {
                values[index] = i;
                Decompose(x - i, values, index + 1);
            }
            // special case for non-zero component
            if (index > 0)
            {
                values[index] = x;
                Decompose(0, values, index + 1);
            }
        }

        public static void Decompose(int x)
        {
            int[] values = new int[x];
            Decompose(x, values, 0);
        }
    }

    // Telephone Words
    public class TelephoneNumber
    {
        private const int PhoneNumberLength = 7;
        private readonly int[] phoneNumber;
        private char[] result = new char[PhoneNumberLength];

        public TelephoneNumber(int[] n)
        {
            phoneNumber = n;
        }

        public void PrintWords()
        {
            PrintWords(0);
        }

        private void PrintWords(int currentDigit)
        {
            if (currentDigit == PhoneNumberLength)
            {
                System.Console.WriteLine(result);
                return;
            }
            // Running time is O(3^N).
            for (int i = 1; i <= 3; i++)
            {
                result[currentDigit] = GetCharKey(phoneNumber[currentDigit], i);
                PrintWords(currentDigit + 1);
                if (phoneNumber[currentDigit] == 0 || phoneNumber[currentDigit] == 1)
                    return;
            }
        }

        private char GetCharKey(int telephoneKey, int place)
        {
            char c = ' ';

            switch (telephoneKey)
            {
                case 2 :
                {
                    if (place == 1)
                        c = 'A';
                    else if (place == 2)
                        c = 'B';
                    else
                        c = 'C';
                    break;
                }
                case 3:
                {
                    if (place == 1)
                        c = 'D';
                    else if (place == 2)
                        c = 'E';
                    else
                        c = 'F';
                    break;
                }
                case 4:
                {
                    if (place == 1)
                        c = 'G';
                    else if (place == 2)
                        c = 'H';
                    else
                        c = 'I';
                    break;
                }
                case 5:
                {
                    if (place == 1)
                        c = 'J';
                    else if (place == 2)
                        c = 'K';
                    else
                        c = 'L';
                    break;
                }
                case 6:
                {
                    if (place == 1)
                        c = 'N';
                    else if (place == 2)
                        c = 'M';
                    else
                        c = 'O';
                    break;
                }
                case 7:
                {
                    if (place == 1)
                        c = 'P';
                    else if (place == 2)
                        c = 'R';
                    else
                        c = 'S';
                    break;
                }
                case 8:
                {
                    if (place == 1)
                        c = 'T';
                    else if (place == 2)
                        c = 'U';
                    else
                        c = 'V';
                    break;
                }
                case 9:
                {
                    if (place == 1)
                        c = 'W';
                    else if (place == 2)
                        c = 'X';
                    else
                        c = 'Y';
                    break;
                }
                default:
                    break;
            }

            return c;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int idx = StringEx.IndexOf("abcdef", "cd");

            string in1 = "aaaaabbbbbbbcccbbbbcccdddddaaaab";
            string s = StringEx.RunLengthEncoding(in1);
            string s1 = StringEx.RunLengthDecoding(s);

            int largestStartIndex = StringEx.FindLargetstBlock("abeeeeefeegkkkkkkkkf");

            DecomposeNumber.Decompose(3);

            string str = "abc";
            
            Debug.Assert(StringEx.Substring(str, 0, 3).Equals("abc"), "Error");
            Debug.Assert(StringEx.Substring(str, 1, 2).Equals("bc"), "Error");
            Debug.Assert(StringEx.Substring(str, 0, 2).Equals("ab"), "Error");
            Debug.Assert(StringEx.Substring(str, 0, 0).Equals(""), "Error");
            Debug.Assert(StringEx.Substring("", 0, 3) == null, "Error");

            try
            {
                StringEx.Substring(str, -1, -1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.WriteLine("Passed");
            }

            try
            {
                StringEx.Substring(str, 0, 4);
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.WriteLine("Passed");
            }

            Console.WriteLine(StringEx.GetTwoLengthSubstringNumber("xxcaazz", "xxbaaz"));
            Console.WriteLine(StringEx.GetTwoLengthSubstringNumber("abc", "abc"));
            Console.WriteLine(StringEx.GetTwoLengthSubstringNumber("abc", "axc"));
            Console.WriteLine(StringEx.GetTwoLengthSubstringNumber("abcdtyty", "abcdwstywd"));

            string result = StringEx.ToUnderscore("camelCase");
            result = StringEx.ToUnderscore("CamelCase");

            int atoiResult = StringEx.atoi("-123");

            Permutations permute = new Permutations("ABCD");
            permute.Permute();

            Combinations combine = new Combinations("wxyz");
            combine.Combine();

            Debug.Assert(StringEx.IsAnagram("cat", "tac") == true);
            Debug.Assert(StringEx.IsAnagram("cat", "tap") == false);

            List<string> inputList = new List<string>();
            inputList.Add("star");
            inputList.Add("rats");
            inputList.Add("arc");
            inputList.Add("arts");
            inputList.Add("car");
            inputList.Add("foo");
            List<List<string>> outputList = new List<List<string>>();
            outputList = StringEx.FindAnagramList(inputList); //  Output: [["star", "rats", "arts"], ["arc", "car"]]

            int[] telephoneNumber = new int[]{2,5,6,8,4,4,0};
            TelephoneNumber telephone = new TelephoneNumber(telephoneNumber);
            telephone.PrintWords();
        }
    }
}