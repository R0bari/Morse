using System.Collections.Generic;
using System.Text;

namespace NoVowels
{
    public static class MorseDecoder
    {
        private static readonly Dictionary<string, char> MorseCodes = new()
        {
            {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'},
            {"-..", 'D'}, {".", 'E'}, {"..-.", 'F'},
            {"--.", 'G'}, {"....", 'H'}, {"..", 'I'},
            {".---", 'J'}, {"-.-", 'K'}, {".-..", 'L'},
            {"--", 'M'}, {"-.", 'N'}, {"---", 'O'},
            {".--.", 'P'}, {"--.-", 'Q'}, {".-.", 'R'},
            {"...", 'S'}, {"-", 'T'}, {"..-", 'U'},
            {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'},
            {"-.--", 'Y'}, {"--..", 'Z'}, {".----", '1'},
            {"..---", '2'}, {"...--", '3'}, {"....-", '4'},
            {".....", '5'}, {"-....", '6'}, {"--...", '7'},
            {"---..", '8'}, {"----.", '9'}, {"-----", '0'},
            {"--..--", ','}, {".-.-.-", '.'}, {"..--..", '?'},
            {"-..-.", '/'}, {"-....-", '-'}, {"-.--.", '('}, {"-.--.-", ')'}
        };


        public static string DecodeBits(string bits)
        {
            var result = new StringBuilder();
            var currentSequence = new StringBuilder();
            var trimmedBits = bits.Trim('0');

            var timeUnitLength = DetermineTimeUnitLength(trimmedBits);

            for (var i = 0; i < trimmedBits.Length - timeUnitLength + 1; i += timeUnitLength)
            {
                var current = trimmedBits[i];
                currentSequence.Append(current);

                if (i != trimmedBits.Length - timeUnitLength &&
                    !IsEndOfBitSequence(current, trimmedBits[i + timeUnitLength]))
                {
                    continue;
                }

                result.Append(ProcessBitSequence(currentSequence.ToString()));
                currentSequence.Clear();
            }

            return result.ToString();
        }

        private static int DetermineTimeUnitLength(string bits)
        {
            var timeUnitLengths = new List<int>();
            var currentTimeUnitLength = 0;
            var maxTimeUnitLength = 0;

            for (var i = 0; i < bits.Length; ++i)
            {
                ++currentTimeUnitLength;

                if (i == bits.Length - 1 || IsEndOfBitSequence(bits[i], bits[i + 1]))
                {
                    timeUnitLengths.Add(currentTimeUnitLength);
                    if (currentTimeUnitLength > maxTimeUnitLength)
                    {
                        maxTimeUnitLength = currentTimeUnitLength;
                    }

                    currentTimeUnitLength = 0;
                }
            }

            bool isLengthCorrect;
            var timeUnitLength = maxTimeUnitLength;
            do
            {
                isLengthCorrect = true;
                foreach (var unitLength in timeUnitLengths)
                {
                    if (unitLength % timeUnitLength == 0)
                    {
                        continue;
                    }

                    --timeUnitLength;
                    isLengthCorrect = false;
                    break;
                }
            } while (!isLengthCorrect);

            return timeUnitLength;
        }

        private static bool IsEndOfBitSequence(char currentSymbol, char nextSymbol)
        {
            return currentSymbol == '1' && nextSymbol == '0' || currentSymbol == '0' && nextSymbol == '1';
        }

        private static string ProcessBitSequence(string sequence)
        {
            return sequence switch
            {
                "000" => " ",
                "0000000" => "   ",
                "1" => ".",
                "111" => "-",
                _ => ""
            };
        }

        public static string DecodeMorse(string morseCode)
        {
            var result = new StringBuilder();
            var currentSequence = new StringBuilder();
            var trimmedCode = morseCode.Trim();

            for (var i = 0; i < trimmedCode.Length; ++i)
            {
                var current = trimmedCode[i];
                currentSequence.Append(current);

                if (i != trimmedCode.Length - 1 && !IsEndOfMorseSequence(current, trimmedCode[i + 1]))
                {
                    continue;
                }

                result.Append(ProcessMorseSequence(currentSequence.ToString()));
                currentSequence.Clear();
            }

            return result.ToString();
        }

        private static bool IsEndOfMorseSequence(char currentSymbol, char nextSymbol)
        {
            return currentSymbol == ' ' && nextSymbol != ' ' || currentSymbol != ' ' && nextSymbol == ' ';
        }

        private static string ProcessMorseSequence(string sequence)
        {
            return sequence switch
            {
                " " => "",
                "   " => " ",
                _ => ConvertMorse(sequence).ToString()
            };
        }

        private static char ConvertMorse(string morseSequence)
        {
            MorseCodes.TryGetValue(morseSequence, out var value);
            return value;
        }
    }
}