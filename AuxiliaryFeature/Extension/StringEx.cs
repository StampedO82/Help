using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace AuxiliaryFeature.Extension
{
    public static class StringEx
    {
        static System.Text.UnicodeEncoding _encoding = new System.Text.UnicodeEncoding();

        public static MemoryStream ConvertToMemoryStream(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            byte[] strInBytes = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream(strInBytes);
            ms.Position = 0;
            return ms;
        }

        public static string LettersOrDigitOnly(this string str)
        {
            var onlyLetters = new String(str.Where(c => Char.IsLetterOrDigit(c) || c == '_').ToArray());
            return onlyLetters;
        }

        public static string DigitOnly(this string str)
        {
            var onlyDigits = new String(str.Where(c => Char.IsDigit(c)).ToArray());
            return onlyDigits;
        }

        public static bool IsDigitOnly(this string str)
        {
            var tempStr = str.Trim();
            return tempStr.Count(c => Char.IsDigit(c)) == tempStr.Length;
        }

        public static bool IsDecimal(this string str)
        {
            var tempStr = str.Trim();
            if (str.Count(c => c == '.') == 1)
                return tempStr.Count(c => Char.IsDigit(c) || c == '.') == tempStr.Length;
            else
            if (str.Count(c => c == ',') == 1)
                return tempStr.Count(c => Char.IsDigit(c) || c == ',') == tempStr.Length;
            else
                return tempStr.Count(c => Char.IsDigit(c)) == tempStr.Length;

        }

        public static string ToRandomString(this string s, int length, bool isOnlyAlphaNumeric = true, int minSpecialCharacters = 1)
        {
            if (isOnlyAlphaNumeric) minSpecialCharacters = 0;
            s = Membership.GeneratePassword(length, minSpecialCharacters);
            if (!isOnlyAlphaNumeric) return s;

            char[] msSpecialCharacters =
                "!@#$%^&*()_-+=[{]};:<>|./?".ToCharArray();
            string filler =
                Membership.GeneratePassword(length, 0);
            int fillerIndex = 0;
            int fillerBuffer = 0;

            while (s.IndexOfAny(msSpecialCharacters) > -1
                || s.Length < length)
            {
                s = s.RemoveCharacters(msSpecialCharacters);
                fillerBuffer = length - s.Length;
                if ((fillerBuffer + fillerIndex) > filler.Length)
                {  
                    filler = Membership.GeneratePassword(length, 0);
                    fillerIndex = 0;
                }
                s += filler.Substring(fillerIndex, fillerBuffer);
                fillerIndex += fillerBuffer;
            }
            return s;
        }

        public static string RemoveCharacters(this string s, char[] characters)
        {
            return new string(s.ToCharArray()
                .Where(c => !characters.Contains(c)).ToArray());
        }

        public static List<string> ConvertToList(this string s, char delimiter = '|')
        {
            if (!string.IsNullOrEmpty(s))
            {
                string[] namesArray = s.Split(delimiter);
                List<string> list = new List<string>(namesArray.Length);
                list.AddRange(namesArray);
                list.Reverse();
                return list;
            }
            return null;
        }

        public static string GenerateGUID(this string targetString)
        {
            targetString = Guid.NewGuid().ToString();
            return targetString.ToUpper();
        }

        public static string ConvertToBase64String(this string content)
        {
            try
            {
                Byte[] bytes = _encoding.GetBytes(content);
                var result = Convert.ToBase64String(bytes);
                return result;
            }
            catch (System.ArgumentNullException)
            {
                throw;
            }
            catch (System.Text.EncoderFallbackException)
            {
                throw;
            }
        }

        public static string ConvertFromBase64String(this string content)
        {
            try
            {
                Byte[] bytes = Convert.FromBase64String(content);
                var result = _encoding.GetString(bytes);
                return result;
            }
            catch (System.ArgumentNullException)
            {
                throw;
            }
            catch (System.FormatException)
            {
                throw;
            }
            catch (System.Text.EncoderFallbackException)
            {
                throw;
            }
            catch (System.Text.DecoderFallbackException)
            {
                throw;
            }
        }
    }
}
