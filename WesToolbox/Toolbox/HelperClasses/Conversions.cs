namespace WesToolbox.HelperClasses
{
    using System;
    using System.Text.RegularExpressions;

    public static class Conversions
    {
        #region Static Fields

        private static readonly Regex BinaryValidationPattern = new Regex(@"[01]+", RegexOptions.Compiled);

        private static readonly Regex HexValidationPattern = new Regex(
            @"0x[0-9abcdef]+", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Public Methods and Operators

        public static string ConvertBinaryToHexString(this string theBinaryString)
        {
            if (!BinaryValidationPattern.IsMatch(theBinaryString))
            {
                throw new ArgumentException("Invalid Binary String.");
            }

            throw new NotImplementedException();
        }

        public static int ConvertBinaryToInt(this string theBinaryString)
        {
            if (!BinaryValidationPattern.IsMatch(theBinaryString))
            {
                throw new ArgumentException("Invalid Binary String.");
            }

            throw new NotImplementedException();
        }

        public static string ConvertHexToBinaryString(this string theHexString)
        {
            if (!HexValidationPattern.IsMatch(theHexString))
            {
                throw new ArgumentException("Invalid Hex String.");
            }

            throw new NotImplementedException();
        }

        public static int ConvertHexToInt(this string theHexString)
        {
            if (!HexValidationPattern.IsMatch(theHexString))
            {
                throw new ArgumentException("Invalid Hex String.");
            }

            throw new NotImplementedException();
        }

        public static string ConvertToBinaryString(this int theInt)
        {
            throw new NotImplementedException();
        }

        public static string ConvertToHexString(this int theInt)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}