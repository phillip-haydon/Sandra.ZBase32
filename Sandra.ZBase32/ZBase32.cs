using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Sandra.ZBase32
{
    public class ZBase32
    {
        private static readonly IDictionary<string, string> EncodingMap = new Dictionary<string, string>
        {
            ["00000"] = "y", ["00001"] = "b", ["00010"] = "n", ["00011"] = "d",
            ["00100"] = "r", ["00101"] = "f", ["00110"] = "g", ["00111"] = "8",
            ["01000"] = "e", ["01001"] = "j", ["01010"] = "k", ["01011"] = "m",
            ["01100"] = "c", ["01101"] = "p", ["01110"] = "q", ["01111"] = "x",
            ["10000"] = "o", ["10001"] = "t", ["10010"] = "1", ["10011"] = "u",
            ["10100"] = "w", ["10101"] = "i", ["10110"] = "s", ["10111"] = "z",
            ["11000"] = "a", ["11001"] = "3", ["11010"] = "4", ["11011"] = "5",
            ["11100"] = "h", ["11101"] = "7", ["11110"] = "6", ["11111"] = "9"
        };

        private static readonly IDictionary<char, string> DecodingMap = new Dictionary<char, string>
        {
            ['y'] = "00000", ['b'] = "00001", ['n'] = "00010", ['d'] = "00011",
            ['r'] = "00100", ['f'] = "00101", ['g'] = "00110", ['8'] = "00111",
            ['e'] = "01000", ['j'] = "01001", ['k'] = "01010", ['m'] = "01011",
            ['c'] = "01100", ['p'] = "01101", ['q'] = "01110", ['x'] = "01111",
            ['o'] = "10000", ['t'] = "10001", ['1'] = "10010", ['u'] = "10011",
            ['w'] = "10100", ['i'] = "10101", ['s'] = "10110", ['z'] = "10111",
            ['a'] = "11000", ['3'] = "11001", ['4'] = "11010", ['5'] = "11011",
            ['h'] = "11100", ['7'] = "11101", ['6'] = "11110", ['9'] = "11111"
        };

        public string Encode(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return string.Empty;
            }
            
            var bytes = Encoding.UTF8.GetBytes(message);
            var result = string.Empty;
            var encoded = string.Empty;

            for (int i = 0; i < bytes.Length; i++)
            {
                result += Convert.ToString(bytes[i], 2).PadLeft(8, '0');
            }

            var remaining = result.Length % 5;
            if (remaining != 0)
            {
                var add = 5 - remaining;
                result = result.PadRight(result.Length + add, '0');
            }

            var len = result.Length / 5;

            for (int i = 0; i < len; i++)
            {
                encoded += EncodingMap[result.Substring(i * 5, 5)];
            }

            return encoded;
        }

        public string Decode(string encoding)
        {
            if (string.IsNullOrEmpty(encoding))
            {
                return string.Empty;
            }
            
            if (!Regex.IsMatch(encoding, "^[13456789abcdefghijkmnopqrstuwxyz]+$"))
            {
                throw new ArgumentException("The argument isn't a valid Z-Base-32 encoding", nameof(encoding));
            }

            var thing = string.Empty;
            
            for (int i = 0; i < encoding.Length; i++)
            {
                thing += DecodingMap[encoding[i]];
            }

            var bytes = new byte[thing.Length / 8];

            for (int i = 0; i < thing.Length / 8; i++)
            {
                bytes[i] = Convert.ToByte(thing.Substring(i * 8, 8), 2);
            }

            return Encoding.UTF8.GetString(bytes);
        }
    }
}