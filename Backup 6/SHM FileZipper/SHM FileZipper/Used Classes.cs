using System;

/* All Classes used by the SHM FileZipper 
 * 
 * AUTHOR : SOUHAM BISWAS
 * 
 */

namespace SHM_FileZipper_Used_Classes
{
    /// <summary>
    /// Encapsulates a Binary number of any length and stores as a byte array.
    /// </summary>
    public class BinaryNumber
    {

        #region Class Property Definitions

        #region Public Properties

        /// <summary>
        /// Number of bits in the binary data set to '1'.
        /// </summary>
        public uint NoOfOnes //Calculates itself.
        {
            get
            {
                if (privateBinaryDataSetInitialized)
                    return this.getNoOfOnes();
                else
                    throw new BinaryObjectNotInitializedException();
            }
        }

        /// <summary>
        /// Number of Bytes occupied by stored Binary number.
        /// </summary>
        public uint NoOfBytes
        {
            get
            {
                return privateNoOfBytes;
            }
        }

        /// <summary>
        /// Number of bits occupied
        /// </summary>
        public uint NoOfBits
        {
            get
            {
                return byteLength * privateNoOfBytes;
            }
        }

        /// <summary>
        /// Variable storing the binary data on which operations will be performed.
        /// </summary>
        public Byte[] ByteRepresentation
        {
            get
            {
                return privateByteRepresentation;
            }

            set
            {
                this.SetObject(value);
            }
        }


        /// <summary>
        /// This is set true only when the object of this class is initialized with binary data.
        /// </summary>
        public bool BinaryDataSetInitialized
        {
            get
            {
                return privateBinaryDataSetInitialized;
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Variable storing the binary data on which operations will be performed.
        /// </summary>
        private Byte[] privateByteRepresentation;

        /// <summary>
        /// Private variable containing size of binary data stored in object.
        /// </summary>
        private uint privateNoOfBytes;

        /// <summary>
        /// Set to true only when the object of this class is initialized with binary data.
        /// </summary>
        private bool privateBinaryDataSetInitialized;

        /// <summary>
        /// Number of bits in a "Byte".
        /// </summary>
        private byte byteLength = 8;

        #endregion

        #endregion

        #region Class Constructors

        /// <summary>
        /// Constructor to accept no arguments to initialize the object.
        /// <para>
        /// NOTE : It is compulsory to initialize the object by using the
        /// SetObject() method atleast once before working with this object.
        /// </para>
        /// <para>
        /// Avoiding so, will cause it to throw a BinaryObjectNotInitializedException
        /// whenever an operation will be performed on it.
        /// </para>
        /// </summary>
        public BinaryNumber() { }

        /// <summary>
        /// Contructor accepting binary number as a string argument.
        /// </summary>
        /// <param name="binaryNumberAsString">Binary Number to be considered.</param>
        public BinaryNumber(String binaryNumberAsString)
        {
            this.SetObject(binaryNumberAsString);
        }

        /// <summary>
        /// Constructor accepting a byte Array as Binary Data.
        /// </summary>
        /// <param name="byteArray"></param>
        public BinaryNumber(Byte[] byteArray)
        {
            this.SetObject(byteArray);
        }

        #endregion

        #region Public Methods

        #region Static Methods

        #region Non-Void Methods

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

        #endregion

        #region Void Methods



        #endregion

        #endregion

        #region Non-Static Methods

        #region Non-Void Methods

        /// <summary>
        /// Gets index of Byte containing the bit at the specified Binary Digit index.
        /// </summary>
        /// <param name="index">Index of Binary bit whose location is being considered.</param>
        /// <returns>Index of Byte in the ByteRepresentation Array containing 
        /// the bit at the specified Binary Digit index.
        /// <para>GetByteIndexAtBitIndex(17) will return the value '2'.</para></returns>
        public uint GetByteIndexAtBitIndex(uint index)
        {
            if (index < this.NoOfBits)
                return index / byteLength;
            throw new IllegalBitReferenceIndexException();
        }

        /// <summary>
        /// Gets the index of the bit referenced by the input Binary Digit index
        /// with respect to the byte of the byte array it is contained in.
        /// </summary>
        /// <param name="index">Index of the bit in consideration.</param>
        /// <returns>Index of the bit referenced by the input Binary Digit index
        /// with respect to the byte of the byte array it is contained in.</returns>
        public byte GetByteOffsetInArrayFromBitIndex(uint index)
        {
            if (index < this.NoOfBits)
                return (byte)(index - (index / byteLength) * byteLength);
            throw new IllegalBitReferenceIndexException();
        }

        /// <summary>
        /// Gets the byte corresponding to the bit stored at the specified Binary index.
        /// </summary>
        /// <param name="index">Index of the bit, the byte containing which is to be returned</param>
        /// <returns>The Byte which contains the bit at the specified index.
        /// <para>
        /// Example : GetCorrespondingByteAtBitIndex(9) will return contents of ByteRepresentation[1].
        /// </para>
        /// </returns>
        public byte GetCorrespondingByteAtBitIndex(uint index)
        {
            if (index >= NoOfBits)
                throw new IllegalBitReferenceIndexException();
            return privateByteRepresentation[index / byteLength];
        }

        /// <summary>
        /// Gets whether the bit at given Binary Bit index is set.
        /// </summary>
        /// <param name="index">Index of the bit in consideration.</param>
        /// <returns>True if bit at index is set to '1',
        /// <para>Otherwise, returns a '0'.</para></returns>
        public bool BitAtBitIndexSet(uint index)
        {
            if (index >= this.NoOfBits)
                throw new IllegalBitReferenceIndexException();
            if ((privateByteRepresentation[index / byteLength] & (1 << (byte)(index - (index / byteLength) * byteLength))) > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Returns the Bit ('0' or '1') stored in the object at the given index.
        /// </summary>
        /// <param name="index">Index of the position from which the Bit is to be extracted.</param>
        /// <returns>Bit value stored at the given index of the input binary data.</returns>
        public byte BitAtIndex(uint index)
        {
            if (!privateBinaryDataSetInitialized) //Checking if Object is initialized.
                throw new BinaryObjectNotInitializedException();
            if (index > ((privateNoOfBytes * byteLength) - 1)) //Checking if "index" is in range.
                throw new IllegalBitReferenceIndexException();
            if (BitAtBitIndexSet(index))
                return 1;
            return 0;
        }

        #endregion

        #region Void Methods

        /// <summary>
        /// Sets the Bit at the given index to the one specified.
        /// </summary>
        /// <param name="index">Index of the Bit to be altered.</param>
        /// <param name="bit">'0' or '1', whicever bit is to be stored at the location</param>
        public void SetBitAtIndex(uint index, byte bit)
        {
            if ((bit != 0x00) && (bit != 0x01))
                throw new InvalidBinaryInputDataTypeException();
            if (index >= this.NoOfBits)
                throw new IllegalBitReferenceIndexException();
            if (bit == 0x01)
            {
                if (this.BitAtBitIndexSet(index))
                    return;
                privateByteRepresentation[this.GetByteIndexAtBitIndex(index)] |= (byte)(1 << GetByteOffsetInArrayFromBitIndex(index));
            }
            else
            {
                if (!this.BitAtBitIndexSet(index))
                    return;
                privateByteRepresentation[this.GetByteIndexAtBitIndex(index)] &= (byte)~(1 << GetByteOffsetInArrayFromBitIndex(index));
            }
        }

        /// <summary>
        /// Sets the ByteRepresentation byte array according to the binary value denoted by the
        /// input String.
        /// </summary>
        /// <param name="binaryNumberAsString">Binary Number to be considered.</param>
        public void SetObject(string binaryNumberAsString)
        {
            foreach (char c in binaryNumberAsString)
            {
                if (c == '1')
                    continue;
                if (c == '0')
                    continue;
                throw new InvalidBinaryStringException(); //Checking for non-binary characters.
            }
            ComputeByteRepresentationLength(binaryNumberAsString); //Sets variable privateNoOfByes.
            privateByteRepresentation = computeByteRepresentation(binaryNumberAsString); //Parses Binary string to byte array.
            privateBinaryDataSetInitialized = true;
        }

        /// <summary>
        /// Sets the ByteRepresentation Byte Array Property of the object.
        /// </summary>
        /// <param name="byteArray">Byte Array containing the binary data for the ByteRepresentation Property
        /// of the BinaryNumber Object.</param>
        public void SetObject(Byte[] byteArray)
        {
            if (byteArray.GetType().ToString() == "System.Byte[]")
            {
                privateByteRepresentation = new Byte[byteArray.Length];
                Array.Copy(byteArray, privateByteRepresentation, byteArray.Length);
                Array.Reverse(privateByteRepresentation);
                updateAllPublicAttributes();
            }
            else
                throw new InvalidBinaryInputDataTypeException();
        }

        #endregion

        #endregion

        #endregion

        #region Private Methods

        #region Non-Void Methods

        /// <summary>
        /// Calculates number of bits in input binary data set to '1'.
        /// </summary>
        /// <returns>Number of bits in input binary data set to '1'.</returns>
        private uint getNoOfOnes()
        {
            uint outVal = 0;
            foreach (Byte b in this.privateByteRepresentation)
            {
                outVal += GetNoOfOnesInByte(b);
            }
            return outVal;
        }

        /// <summary>
        /// Computes the value of ByteRepresentation byte array of this class.
        /// </summary>
        /// <param name="binString">Binary number to be utilised.</param>
        /// <returns>Byte Array containing the binary digits as a minimum fit.</returns>
        private Byte[] computeByteRepresentation(string binString)
        {
            Byte[] outVal = new Byte[this.privateNoOfBytes];
            Byte[] binByteArrayTemp = ReturnBinaryByteArray(binString);
            int byteLengthLocalInt = (int)byteLength, k;
            for (int i = 0; i < outVal.Length; i++)
            {
                for (k = 0; k < byteLengthLocalInt; k++) //Iteration to process each byte.
                {
                    int a = byteLengthLocalInt * i;
                    a += k;
                    try
                    {
                        int b = binByteArrayTemp[a];
                        b <<= k;
                        outVal[i] |= (byte)(binByteArrayTemp[(byteLengthLocalInt * i) + k] << k);
                    }
                    catch (IndexOutOfRangeException) { }
                }
            }
            return outVal;
        }

        #endregion

        #region Void Methods

        /// <summary>
        /// Updates the values of all Public and Private attributes of the object of this class
        /// corresponding to the contents of the ByteRepresentation Array.
        /// </summary>
        private void updateAllPublicAttributes()
        {
            privateNoOfBytes = (uint)privateByteRepresentation.Length;
            privateBinaryDataSetInitialized = true;
        }

        /// <summary>
        /// Computes value of NoOfBytes Property, which stores the size of the 
        /// ByteRepresentation array.
        /// </summary>
        /// <param name="binaryNumAsString">Binary Number to be considered.</param>
        private void ComputeByteRepresentationLength(String binaryNumAsString)
        {
            Double tmpDoubleBinStringLength = Convert.ToDouble(binaryNumAsString.Length);
            Double noOfBytesApprox = tmpDoubleBinStringLength / Convert.ToDouble(byteLength);
            privateNoOfBytes = (uint)noOfBytesApprox;
            if (tmpDoubleBinStringLength != noOfBytesApprox)
                privateNoOfBytes++;
        }

        #endregion

        #endregion

    }
}