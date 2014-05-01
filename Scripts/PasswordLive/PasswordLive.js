/*
    Password Live Generator
    ---------------------
    Purpose:       Simple alternative to password managers
    Version:       0.02  
    Last changed:  2014-04-14
    Homepage:      http://passwordlive.github.io or http://passwordlive.com
    Donate BTC:    1mikeGbp2cRBzsBtZtT2k3FX3byPmCR8N
    ---------------------
    Usage:   
    PasswordLive.Compute('my secret keyword', 'amazon', 15, true, true, true, true)
    ---------------------
    Requires:    
    http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/core-min.js
    http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/enc-base64-min.js
    http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/sha256.js
    ---------------------
    Licensing terms:   This script is released under the FreeBSD license.
                       http://en.wikipedia.org/wiki/BSD_license

    Copyright (c) 2009-2014, Password Live 
    All rights reserved. 

    Redistribution and use in source and binary forms, with or without 
    modification, are permitted provided that the following conditions are met: 

        * Redistributions of source code must retain the above copyright notice, 
        this list of conditions and the following disclaimer. 
        * Redistributions in binary form must reproduce the above copyright 
        notice, this list of conditions and the following disclaimer in the 
        documentation and/or other materials provided with the distribution. 

    THIS SOFTWARE IS PROVIDED BY THE AUTHOR AND CONTRIBUTORS ``AS IS'' AND ANY 
    EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
    DISCLAIMED. IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS BE LIABLE FOR ANY 
    DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
    (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
    SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER 
    CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
    LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY 
    OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH 
    DAMAGE. 
*/

var PasswordLive = function () {

    function IsEmpty(x) {
        switch (x) {
            case "":
            case null:
            case typeof this == "undefined":
                return true;
            default: return false;
        }
    }

    return {

        Compute: function (keywordString, whatForString, length, MakeUpperCase, MakeLowerCase, MakeNumbers, MakeSpecialCharacters) {

            var secretKeyword = "";
            var whatFor = "";
            var inputString = "";

            var minimum_password_length = 8;
            var maximum_password_length = 64;

            var upperCase = 0;
            var lowerCase = 0;
            var numbers = 0;
            var specialChars = 0;
            var passwordLength = 0;

            secretKeyword = keywordString;
            passwordLength = length;

            whatFor = whatForString;

            if (MakeUpperCase == true)
                upperCase = 1;
            else
                upperCase = 0;

            if (MakeLowerCase == true)
                lowerCase = 1;
            else
                lowerCase = 0;

            if (MakeNumbers == true)
                numbers = 1;
            else
                numbers = 0;

            if (MakeSpecialCharacters == true)
                specialChars = 1;
            else
                specialChars = 0;

            if ((passwordLength < minimum_password_length) || (passwordLength == null))
                passwordLength = minimum_password_length;

            if (passwordLength > maximum_password_length)
                passwordLength = maximum_password_length;

            if (secretKeyword == null)
                secretKeyword = "";

            if (whatFor == null)
                whatFor = "";

            var modifier = (upperCase * 3) + (lowerCase * 5) + (numbers * 7) + (specialChars * 11);

            inputString = whatFor + modifier.toString() + secretKeyword;

            var inputData = CryptoJS.enc.Utf8.parse(inputString);
            var hashValue;
            var baseResult;

            inputString = secretKeyword + modifier.toString() + whatFor;
            var inputData2 = CryptoJS.enc.Utf8.parse(inputString);
            var hashValue2;
            var baseResult2;

            hashValue = CryptoJS.SHA256(inputData);
            baseResult = hashValue.toString(CryptoJS.enc.Base64);

            hashValue2 = CryptoJS.SHA256(inputData2);
            baseResult2 = hashValue2.toString(CryptoJS.enc.Base64);

            var p = (baseResult.substr(0, baseResult.length - 1)) + (baseResult2.substr(0, baseResult2.length - 1));

            var charMap = [
                                ["A", "U", "o", "5", "!"],
                                ["B", "Z", "c", "6", "_"],
                                ["C", "L", "n", "5", "^"],
                                ["D", "K", "l", "9", ";"],
                                ["E", "R", "h", "3", "?"],
                                ["F", "T", "w", "1", "}"],
                                ["G", "W", "t", "1", "-"],
                                ["H", "M", "b", "7", "^"],
                                ["I", "E", "", "5", ":"],
                                ["J", "", "h", "8", "%"],
                                ["K", "Z", "", "6", ","],
                                ["L", "B", "g", "", "#"],
                                ["M", "Q", "a", "0", "@"],
                                ["N", "", "z", "", ""],
                                ["O", "J", "o", "4", "%"],
                                ["P", "K", "m", "0", ""],
                                ["Q", "", "k", "8", "{"],
                                ["R", "A", "t", "4", "-"],
                                ["S", "", "s", "6", ""],
                                ["T", "S", "v", "0", "|"],
                                ["U", "V", "g", "9", ""],
                                ["V", "D", "", "3", "="],
                                ["W", "", "p", "0", "@"],
                                ["X", "D", "z", "1", ")"],
                                ["Y", "", "u", "6", ""],
                                ["Z", "V", "", "9", "("],
                                ["a", "I", "i", "2", ""],
                                ["b", "", "c", "5", "."],
                                ["c", "J", "r", "3", ""],
                                ["d", "P", "d", "6", "!"],
                                ["c", "J", "r", "3", ""],
                                ["d", "P", "d", "6", "!"],
                                ["e", "S", "l", "4", ""],
                                ["f", "N", "j", "2", "*"],
                                ["g", "X", "", "4", "'"],
                                ["h", "F", "e", "7", "+"],
                                ["i", "A", "y", "8", "/"],
                                ["j", "B", "e", "", ""],
                                ["k", "", "", "7", "\\"],
                                ["l", "O", "b", "3", "?"],
                                ["m", "R", "q", "", "#"],
                                ["n", "H", "", "0", "}"],
                                ["o", "L", "v", "7", "."],
                                ["p", "F", "", "4", ")"],
                                ["q", "", "i", "8", ""],
                                ["r", "W", "", "5", "$"],
                                ["s", "", "w", "9", ""],
                                ["t", "C", "u", "2", "@"],
                                ["u", "H", "f", "6", ""],
                                ["v", "", "q", "9", "'"],
                                ["w", "O", "x", "2", "/"],
                                ["x", "T", "j", "2", ","],
                                ["y", "X", "m", "8", ";"],
                                ["z", "Y", "u", "1", ""],
                                ["0", "C", "", "4", "|"],
                                ["1", "", "d", "5", "="],
                                ["2", "Q", "", "8", "_"],
                                ["3", "G", "x", "7", "*"],
                                ["4", "M", "s", "3", "{"],
                                ["5", "E", "n", "1", ""],
                                ["6", "I", "a", "7", "@"],
                                ["7", "N", "k", "2", ":"],
                                ["8", "G", "r", "3", "\\"],
                                ["9", "U", "p", "1", "("],
                                ["+", "Y", "f", "9", "+"],
                                ["/", "P", "y", "0", "$"]
            ];

            var charMapLocations = new Array(p.length);
            for (var i = 0; i < charMapLocations.length; i++) charMapLocations[i] = 0;

            for (var i = 0; i < p.length; i++) {
                for (var c = 0; c < charMap.length; c++) {
                    if (p.substr(i, 1) == charMap[c][0]) {
                        charMapLocations[i] = c;
                        break;
                    }
                }
            }

            var x = 0;
            var y = 1;
            var N = 0;
            var finalPassword = "";
            var charMapLocationsIndex = -1;
            var currentPasswordLength = 0;

            var contains_upperCase = false;
            var contains_lowerCase = false;
            var contains_numbers = false;
            var contains_specialChars = false;

            if (upperCase == 0)
                contains_upperCase = true;
            if (lowerCase == 0)
                contains_lowerCase = true;
            if (numbers == 0)
                contains_numbers = true;
            if (specialChars == 0)
                contains_specialChars = true;

            if ((lowerCase == 1) || (numbers == 1) || (upperCase == 1) || (specialChars == 1)) {
                while (currentPasswordLength < passwordLength) {
                    if (charMapLocationsIndex == (charMapLocations.length - 1)) {
                        charMapLocationsIndex = 0;
                    }
                    else {
                        charMapLocationsIndex++;
                    }

                    N = charMapLocations[charMapLocationsIndex];
                    x = Math.floor((x * 4 + y + N) / 4);
                    y = Math.floor(((x * 4 + y + N) % 4) + 1);

                    while (x > (charMap.length - 1)) {
                        x = x - (charMap.length);
                    }

                    if ((upperCase == 0) && (y == 1))
                        continue;
                    if ((lowerCase == 0) && (y == 2))
                        continue;
                    if ((numbers == 0) && (y == 3))
                        continue;
                    if ((specialChars == 0) && (y == 4))
                        continue;

                    if (!IsEmpty(charMap[x][y])) {

                        if ((currentPasswordLength > (minimum_password_length - 5)) && (currentPasswordLength < minimum_password_length)) {

                            if ((contains_upperCase == false) || (contains_lowerCase == false) || (contains_numbers == false) || (contains_specialChars == false)) {
                                if ((y == 1) && (contains_upperCase == true)) {
                                    continue;
                                }
                                else if ((y == 2) && (contains_lowerCase == true)) {
                                    continue;
                                }
                                else if ((y == 3) && (contains_numbers == true)) {
                                    continue;
                                }
                                else if ((y == 4) && (contains_specialChars == true)) {
                                    continue;
                                }
                            }

                        }

                        finalPassword = finalPassword + charMap[x][y];
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
    };
}();