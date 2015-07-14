using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

class Program
{
    const int AlienEncodingKey = 17;
    const int EarthEncodingKey = 26;

    static void Main()
    {
        string[] alienWords = Console.ReadLine().Split(' ');

        var decodedMessage = new StringBuilder();
        for (int i = 0; i < alienWords.Length; i++)
        {
            string alienWord = alienWords[i];
            BigInteger decodedDecimal = DecodeWordToDecimal(alienWord, AlienEncodingKey);
            string earthWord = EncodeDecimalToWord(decodedDecimal, EarthEncodingKey);

            decodedMessage.Append(earthWord);
            if (i != alienWords.Length - 1)
            {
                decodedMessage.Append(" ");
            }
        }

        Console.WriteLine(decodedMessage);
    }

    static BigInteger DecodeWordToDecimal(string encodedWord, int encodingKey)
    {
        var extractedDecimalDigits = new List<int>();
        for (int i = 0; i < encodedWord.Length; i++)
        {
            char currentLetter = Char.ToLower(encodedWord[i]);
            int currentDigit = currentLetter - 'a';
            extractedDecimalDigits.Add(currentDigit);
        }

        BigInteger decimalNumber = 0;
        for (int i = 0; i < extractedDecimalDigits.Count; i++)
        {
            decimalNumber *= encodingKey;
            decimalNumber += (BigInteger)extractedDecimalDigits[i];
        }

        return decimalNumber;
    }

    static string EncodeDecimalToWord(BigInteger num, int encodingKey)
    {
        var result = new StringBuilder();
        do
        {
            int remainder = (int)(num % encodingKey);
            num /= encodingKey;
            result.Insert(0, (char)(remainder + 'a'));
        }
        while ((num != 0));
        return result.ToString();
    }
}
