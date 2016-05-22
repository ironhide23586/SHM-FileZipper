using System;

/*
 * Contains definition of BinaryStaticClass Class.
 * 
 * AUTHOR : SOUHAM BISWAS
 * 
 */

namespace BinaryNumberClasses
{
    /// <summary>
    /// Class containing all static methods utilized by the other classes in this assembly.
    /// </summary>
    public abstract class BinaryStaticClass
    {
        #region Non-Void Methods

        /// <summary>
        /// Calculates the perfect square which is greater than or equal to the input number.
        /// </summary>
        /// <param name="a">Number whose nearest perfect square is to be calculated.</param>
        /// <returns>Perfect square which is greater than or equal to the input number.</returns>
        public static uint NearestPerfectSquare(uint a)
        {
            double tmp = System.Math.Sqrt(a), k = System.Math.Truncate(tmp);
            if (tmp > k)
                k++;
            return (uint)(k * k);
        }

        /// <summary>
        /// Gets the number of bits in a Byte set to '1'.
        /// </summary>
        /// <param name="b">Byte to be considered.</param>
        /// <returns>Number of bits in 'b' set to '1'.</returns>
        public static byte GetNoOfOnesInByte(Byte b)
        {
            Byte outVal = 0;
            for (byte i = 0; i < 8; i++)
            {
                if ((b & (1 << i)) >= 1)
                    outVal++;
            }
            return outVal;
        }

        /// <summary>
        /// Returns a byte array in which each cell holds a '0' or a '1'.
        /// </summary>
        /// <param name="binString">Binary Number to be operated upon.</param>
        /// <returns>Byte array in which each cell holds a '0' or a '1'.</returns>
        public static Byte[] ReturnBinaryByteArray(string binString)
        {
            Byte[] outVal = new Byte[binString.Length];
            for (int i = binString.Length - 1; i >= 0; i--)
            {
                if (binString[binString.Length - 1 - i] == '1')
                    outVal[i] = 0x01;
            }
            return outVal;
        }

        /// <summary>
        /// Gets the bit ('0' or '1') at the specified bit index from the byte array.
        /// </summary>
        /// <param name="byteArr">Input byte array.</param>
        /// <param name="index">Bit index
        /// <para>Can range from 0 to (byteArr.Length * 8 - 1)</para></param>
        /// <returns>Bit value ('0' or '1') at the specified bit index from the byte array.</returns>
        public static byte BitAtBitIndex(byte[] byteArr, uint index)
        {
            if ((byteArr[index / 8] & (1 << (byte)((index / 8 + 1) * 8 - (index + 1)))) > 0)
                return 1;
            return 0;
        }

        /// <summary>
        /// Converts a given byte into it's equivalent string representation.
        /// </summary>
        /// <param name="inByte">Byte to be converted.</param>
        /// <returns>String of 0's and 1's representing the byte.</returns>
        public static string ByteStringRepresentation(byte inByte)
        {
            string outString = "";
            for (byte i = 7; i >= 0; i--)
            {
                if (i == 255)
                    break;
                outString += (inByte & (1 << i)) >> i;
            }
            return outString;
        }

        /// <summary>
        /// Minimum number of bytes needed to fit the said number of bits.
        /// </summary>
        /// <param name="noOfBits">Number of binary bits to be fit.</param>
        /// <returns>Memory space in bytes which can accomodate the input number of bits.</returns>
        public static uint NoOfBytesToFitNoOfBits(int noOfBits)
        {
            return (uint)System.Math.Ceiling((double)noOfBits / 8);
        }

        #endregion

        #region Void Methods

        /// <summary>
        /// Parses the input binary byte array (Byte array where each cell holds a '0' or '1') into a byte array
        /// where each bit has been concatenated and the resulting array has been accordingly resized.
        /// </summary>
        /// <param name="binaryByteArray">Input binary byte array.</param>
        /// <param name="outByteArray">Byte array to contain the parsed data.</param>
        public static void ParseBinaryByteArrayToByteArray(byte[] binaryByteArray, byte[] outByteArray)
        {
            //Byte[] binByteArrayTemp = ReturnBinaryByteArray(binString);
            //outByteArray = new byte[NoOfBytesToFitNoOfBits(binaryByteArray.Length)];
            int k;
            for (int i = 0; i < outByteArray.Length; i++) //Iteration to process each byte.
            {
                for (k = 0; k < 8; k++) //Iteration to process each bit.
                {
                    try
                    {
                        outByteArray[i] |= (byte)(binaryByteArray[(8 * i) + k] << k);
                    }
                    catch (IndexOutOfRangeException) { }
                }
            }
        }

        #endregion
    }
}