using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StringId.Library
{
    [Flags]
    public enum StringIdRanges
    {
        Lower = 1,
        Upper = 2,
        Numeric = 4,
        Special = 8
    }

    /// <summary>
    /// this is a way to generate random string Ids of a desired length and format
    /// </summary>
    public static class StringId
    {
        // idea from https://scottlilly.com/create-better-random-numbers-in-c/
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

        private static Dictionary<StringIdRanges, Dictionary<byte, char>> _charMaps = new Dictionary<StringIdRanges, Dictionary<byte, char>>();

        public static string New(int length, StringIdRanges charRanges = StringIdRanges.Lower | StringIdRanges.Numeric)
        {
            var ranges = new Dictionary<StringIdRanges, string>()
            {
                [StringIdRanges.Lower] = "abcdefghijklmnopqrstuvwxyz",
                [StringIdRanges.Upper] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                [StringIdRanges.Numeric] = "1234567890",
                [StringIdRanges.Special] = ".<>?!@#$%^&*()"
            };

            string source = string.Join(string.Empty, ranges.Where(r => (charRanges & r.Key) == r.Key).Select(kp => kp.Value));
            var sourceBytes = Encoding.ASCII.GetBytes(source);

            if (!_charMaps.ContainsKey(charRanges))
            {
                _charMaps.Add(charRanges, CreateByteCharMap(source));
            }

            var charMap = _charMaps[charRanges];

            StringBuilder result = new StringBuilder();
            
            byte[] randomBytes = new byte[length];
            _generator.GetNonZeroBytes(randomBytes);
            for (int i = 0; i < length; i++) result.Append(charMap[randomBytes[i]]);               
            return result.ToString();
        }

        public static string NewPassword() => New(16, StringIdRanges.Lower | StringIdRanges.Upper | StringIdRanges.Numeric | StringIdRanges.Special);

        private static Dictionary<byte, char> CreateByteCharMap(string source)
        {
            var charArray = source.ToCharArray();

            int index = 0;
            return Enumerable.Range(1, byte.MaxValue)
                .Select(i => new 
                { 
                    ByteValue = (byte)i, 
                    Character = getNextChar() 
                })
                .ToDictionary(val => val.ByteValue, val => val.Character);
            
            char getNextChar()
            {
                char result = charArray[index];
                index++;
                if (index > charArray.Length - 1) index = 0;
                return result;
            }
        }
    }

    public class StringIdBuilder
    {
        private StringBuilder _result = new StringBuilder();

        public StringIdBuilder Add(int length, StringIdRanges charRanges)
        {
            _result.Append(StringId.New(length, charRanges));
            return this;
        }

        public StringIdBuilder Add(string literal)
        {
            _result.Append(literal);
            return this;
        }

        public string Build() => _result.ToString();
    }
}
