using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordLive
{
    public class Generator
    {
        string secretKeyword;
        string whatFor;
        string inputString;

        private const int minimum_password_length = 8;
        private const int maximum_password_length = 64;

        public int MinimumLength
        {
            get { return minimum_password_length; }
        }

        public int MaximumLength
        {
            get { return maximum_password_length; }
        }

        int upperCase;
        int lowerCase;
        int numbers;
        int specialChars;
        int passwordLength;

        public Generator() { }

        public string Compute(string keywordString, string whatForString, int length, bool MakeUpperCase, bool MakeLowerCase, bool MakeNumbers, bool MakeSpecialCharacters)
        {
            this.secretKeyword = keywordString;
            this.passwordLength = length;

            this.whatFor = whatForString;

            if (MakeUpperCase == true)
                this.upperCase = 1;
            else
                this.upperCase = 0;

            if (MakeLowerCase == true)
                this.lowerCase = 1;
            else
                this.lowerCase = 0;

            if (MakeNumbers == true)
                this.numbers = 1;
            else
                this.numbers = 0;

            if (MakeSpecialCharacters == true)
                this.specialChars = 1;
            else
                this.specialChars = 0;

            if ((passwordLength < minimum_password_length) || (passwordLength == null))
                passwordLength = minimum_password_length;

            if (passwordLength > maximum_password_length)
                passwordLength = maximum_password_length;

            if (keywordString == null)
                keywordString = string.Empty;

            if (whatForString == null)
                whatForString = string.Empty;

            int modifier = (this.upperCase * 3) + (this.lowerCase * 5) + (this.numbers * 7) + (this.specialChars * 11);

            inputString = whatForString + modifier.ToString() + keywordString;

            byte[] inputData = Encoding.UTF8.GetBytes(inputString);
            byte[] hashValue;
            string baseResult;

            inputString = keywordString + modifier.ToString() + whatForString;
            byte[] inputData2 = Encoding.UTF8.GetBytes(inputString);
            byte[] hashValue2;
            string baseResult2;

            SHA256 shaM = new SHA256Managed();

            hashValue = shaM.ComputeHash(inputData);
            baseResult = Convert.ToBase64String(hashValue, 0, hashValue.Length);

            hashValue2 = shaM.ComputeHash(inputData2);
            baseResult2 = Convert.ToBase64String(hashValue2, 0, hashValue2.Length);

            string p = (baseResult.Substring(0, baseResult.Length - 1)) + (baseResult2.Substring(0, baseResult2.Length - 1));

            string[,] charMap = { 
                                    { "A", "U", "o", "5", "!" }, 
                                    { "B", "Z", "c", "6", "_" }, 
                                    { "C", "L", "n", "5", "^" }, 
                                    { "D", "K", "l", "9", ";" }, 
                                    { "E", "R", "h", "3", "?" }, 
                                    { "F", "T", "w", "1", "}" }, 
                                    { "G", "W", "t", "1", "-" }, 
                                    { "H", "M", "b", "7", "^" }, 
                                    { "I", "E", "" , "5", ":" }, 
                                    { "J", "" , "h", "8", "%" }, 
                                    { "K", "Z", "" , "6", "," }, 
                                    { "L", "B", "g", "" , "#" }, 
                                    { "M", "Q", "a", "0", "@" }, 
                                    { "N", "" , "z", "" , ""  }, 
                                    { "O", "J", "o", "4", "%" }, 
                                    { "P", "K", "m", "0", ""  }, 
                                    { "Q", "" , "k", "8", "{" }, 
                                    { "R", "A", "t", "4", "-" }, 
                                    { "S", "" , "s", "6", ""  }, 
                                    { "T", "S", "v", "0", "|" }, 
                                    { "U", "V", "g", "9", ""  }, 
                                    { "V", "D", "" , "3", "=" }, 
                                    { "W", "" , "p", "0", "@" }, 
                                    { "X", "D", "z", "1", ")" }, 
                                    { "Y", "" , "u", "6", ""  }, 
                                    { "Z", "V", "" , "9", "(" }, 
                                    { "a", "I", "i", "2", ""  }, 
                                    { "b", "" , "c", "5", "." }, 
                                    { "c", "J", "r", "3", ""  }, 
                                    { "d", "P", "d", "6", "!" }, 
                                    { "c", "J", "r", "3", ""  }, 
                                    { "d", "P", "d", "6", "!" }, 
                                    { "e", "S", "l", "4", ""  }, 
                                    { "f", "N", "j", "2", "*" }, 
                                    { "g", "X", "" , "4", "'" }, 
                                    { "h", "F", "e", "7", "+" }, 
                                    { "i", "A", "y", "8", "/" }, 
                                    { "j", "B", "e", "" , ""  }, 
                                    { "k", "" , "" , "7", "\\" },
                                    { "l", "O", "b", "3", "?" }, 
                                    { "m", "R", "q", "" , "#" }, 
                                    { "n", "H", "" , "0", "}" }, 
                                    { "o", "L", "v", "7", "." }, 
                                    { "p", "F", "" , "4", ")" }, 
                                    { "q", "" , "i", "8", ""  }, 
                                    { "r", "W", "" , "5", "$" }, 
                                    { "s", "" , "w", "9", ""  }, 
                                    { "t", "C", "u", "2", "@" }, 
                                    { "u", "H", "f", "6", ""  }, 
                                    { "v", "" , "q", "9", "'" }, 
                                    { "w", "O", "x", "2", "/" }, 
                                    { "x", "T", "j", "2", "," }, 
                                    { "y", "X", "m", "8", ";" }, 
                                    { "z", "Y", "u", "1", ""  }, 
                                    { "0", "C", "" , "4", "|" }, 
                                    { "1", "" , "d", "5", "=" }, 
                                    { "2", "Q", "" , "8", "_" }, 
                                    { "3", "G", "x", "7", "*" }, 
                                    { "4", "M", "s", "3", "{" }, 
                                    { "5", "E", "n", "1", ""  }, 
                                    { "6", "I", "a", "7", "@" }, 
                                    { "7", "N", "k", "2", ":" }, 
                                    { "8", "G", "r", "3", "\\" },
                                    { "9", "U", "p", "1", "(" }, 
                                    { "+", "Y", "f", "9", "+" }, 
                                    { "/", "P", "y", "0", "$" }  
                                };

            int[] charMapLocations = new int[p.Length];

            for (int i = 0; i < p.Length; i++)
            {
                for (int c = 0; c < charMap.GetLength(0); c++)
                {
                    if (p.Substring(i, 1) == charMap[c, 0])
                    {
                        charMapLocations[i] = c;
                        break;
                    }
                }
            }

            int x = 0;
            int y = 1;
            int N;
            string finalPassword = string.Empty;
            int charMapLocationsIndex = -1;
            int currentPasswordLength = 0;

            bool contains_upperCase = false;
            bool contains_lowerCase = false;
            bool contains_numbers = false;
            bool contains_specialChars = false;

            if (this.upperCase == 0)
                contains_upperCase = true;
            if (this.lowerCase == 0)
                contains_lowerCase = true;
            if (this.numbers == 0)
                contains_numbers = true;
            if (this.specialChars == 0)
                contains_specialChars = true;

            if ((this.lowerCase == 1) || (this.numbers == 1) || (this.upperCase == 1) || (this.specialChars == 1))
            {
                while (currentPasswordLength < passwordLength)
                {
                    if (charMapLocationsIndex == (charMapLocations.Length - 1))
                    {
                        charMapLocationsIndex = 0;
                    }
                    else
                    {
                        charMapLocationsIndex++;
                    }

                    N = charMapLocations[charMapLocationsIndex];
                    x = (x * 4 + y + N) / 4;
                    y = ((x * 4 + y + N) % 4) + 1;

                    while (x > (charMap.GetLength(0) - 1))
                    {
                        x = x - (charMap.GetLength(0));
                    }

                    if ((this.upperCase == 0) && (y == 1))
                        continue;
                    if ((this.lowerCase == 0) && (y == 2))
                        continue;
                    if ((this.numbers == 0) && (y == 3))
                        continue;
                    if ((this.specialChars == 0) && (y == 4))
                        continue;

                    if (charMap[x, y] != string.Empty)
                    {

                        if ((currentPasswordLength > (minimum_password_length - 5)) && (currentPasswordLength < minimum_password_length))
                        {

                            if ((contains_upperCase == false) || (contains_lowerCase == false) || (contains_numbers == false) || (contains_specialChars == false))
                            {
                                if ((y == 1) && (contains_upperCase == true))
                                {
                                    continue;
                                }
                                else if ((y == 2) && (contains_lowerCase == true))
                                {
                                    continue;
                                }
                                else if ((y == 3) && (contains_numbers == true))
                                {
                                    continue;
                                }
                                else if ((y == 4) && (contains_specialChars == true))
                                {
                                    continue;
                                }
                            }

                        }

                        finalPassword = finalPassword + charMap[x, y];
                        currentPasswordLength++;

                        if (y == 1)
                            contains_upperCase = true;
                        else if (y == 2)
                            contains_lowerCase = true;
                        else if (y == 3)
                            contains_numbers = true;
                        else if (y == 4)
                            contains_specialChars = true;
                    }
                }
            }
            return finalPassword;
        }
    }
}