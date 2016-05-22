using System;

/*
 * Contains definition of BinaryNumber Class.
 * 
 * AUTHOR : SOUHAM BISWAS
 * 
 */

namespace BinaryNumberClasses
{
    /// <summary>
    /// Encapsulates a Binary number of any length and stores as a byte array.
    /// </summary>
    public class BinaryNumber : BinaryStaticClass, ICloneable
    {
        #region Class Properties

        #region Public Properties

        #region Readonly Properties

        /// <summary>
        /// Number of Bytes occupied by stored Binary number.
        /// </summary>
        public uint NoOfBytes
        {
            get { return privateNoOfBytes; }
        }

        /// <summary>
        /// Number of bits occupied
        /// </summary>
        public uint NoOfBits
        {
            get { return 8 * privateNoOfBytes; }
        }

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
        /// This is set true only when the object of this class is initialized with binary data.
        /// </summary>
        public bool BinaryDataSetInitialized
        {
            get { return privateBinaryDataSetInitialized; }
        }

        #endregion

        #region Modifiable Properties

        /// <summary>
        /// Variable storing the binary data on which operations will be performed.
        /// </summary>
        public Byte[] ByteRepresentation
        {
            get { return privateByteRepresentation; }
            set { this.SetObject(value); }
        }

        /// <summary>
        /// Contains the string representation of the binary data, i.e as '0's and '1's.
        /// </summary>
        public string StringRepresentation
        {
            get
            {
                string outString = "";
                if (stringInput)
                {
                    for (uint i = this.privateNoOfBytes - 1; i >= 0; i--)
                    {
                        outString += ByteStringRepresentation(privateByteRepresentation[i]);
                        if (i == 0)
                            break;
                    }
                    return outString;
                }
                
                for (uint i = 0; i < this.NoOfBits; i++)
                {
                    outString += this.GetBitAtIndex(i);
                }
                return outString;
            }
            set { this.SetObject(value); }
        }

        #endregion

        #endregion

        #region Private Properties

        /// <summary>
        /// Denotes if the size of the object is fixed.
        /// </summary>
        private bool sizeConstrained;

        /// <summary>
        /// Set true only when object is initialized with binary data input as string.
        /// </summary>
        private bool stringInput;

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
        /// Constructor accepting size of the object as argument.
        /// This initializes the object with null values, but allocating the said amount of 
        /// space.
        /// </summary>
        /// <param name="size">Size of the BinaryNumber object to be constructed in Bytes.</param>
        public BinaryNumber(uint size)
        {
            privateByteRepresentation = new byte[size];
            sizeConstrained = true;
            updateAllPublicAttributes();
        }

        /// <summary>
        /// Contructor accepting binary number as a string argument.
        /// </summary>
        /// <param name="binaryNumberAsString">Binary Number to be considered.</param>
        public BinaryNumber(String binaryNumberAsString)
        {
            this.SetObject(binaryNumberAsString);
        }

        /// <summary>
        /// Constructor accepting a byte array as argument.
        /// </summary>
        /// <param name="byteArray">Input Binary Array.</param>
        public BinaryNumber(Byte[] byteArray)
        {
            this.SetObject(byteArray);
        }

        #endregion

        #region Public Methods

        #region Static Methods

        #region Non-Void Methods

        #endregion

        #region Void Methods



        #endregion

        #endregion

        #region Non-Static Methods

        #region Non-Void Methods

        /// <summary>
        /// Creates a copy of the object of this class.
        /// </summary>
        /// <returns>Copy of the object of this class.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Tells whether the input BinaryNumber object is identical to this BinaryNumber object.
        /// </summary>
        /// <param name="comparisonBinaryNumber">The BinaryNumber object to be compared with.</param>
        /// <returns>True if identical, else False.</returns>
        public bool IsEqual(BinaryNumber comparisonBinaryNumber)
        {
            if (this.NoOfBytes == comparisonBinaryNumber.NoOfBytes)
            {
                uint i = 0;
                foreach (byte b in comparisonBinaryNumber.ByteRepresentation)
                {
                    if (b != this.privateByteRepresentation[i++])
                        return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Extracts the byte stream from the specified start index till the stop index.
        /// </summary>
        /// <param name="startByteIndex">Index of the byte from which to start extraction.</param>
        /// <param name="endByteIndex">Index of the byte where to stop extraction.</param>
        /// <returns>A BinaryNumber object of the byte array extracted from this object
        /// from the specified start index to the end index.</returns>
        public BinaryNumber ExtractBytes(uint startByteIndex, uint endByteIndex)
        {
            if (startByteIndex < 0) 
                throw new IllegalBitReferenceIndexException();
            if (endByteIndex < startByteIndex)
                throw new IllegalExtractionParameters();
            if (endByteIndex > this.privateNoOfBytes)
                endByteIndex = this.privateNoOfBytes - 1;
            uint copyLength = endByteIndex - startByteIndex + 1;
            Byte[] extractedByteStream = new Byte[copyLength];
            try
            {
                Array.Copy(this.privateByteRepresentation, startByteIndex, extractedByteStream, 0, copyLength);
            }
            catch (ArgumentException)
            {
                Array.Copy(this.privateByteRepresentation, startByteIndex, extractedByteStream, 0, privateByteRepresentation.Length - startByteIndex);
            }
            BinaryNumber outBinaryNumber = new BinaryNumber(extractedByteStream);
            return outBinaryNumber;
        }

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
                return index / 8;
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
                return (byte)(index - (index / 8) * 8);
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
            return privateByteRepresentation[index / 8];
        }

        /// <summary>
        /// Returns the Bit ('0' or '1') stored in the object at the given index.
        /// </summary>
        /// <param name="index">Index of the position from which the Bit is to be extracted.</param>
        /// <returns>Bit value stored at the given index of the input binary data.</returns>
        public byte GetBitAtIndex(uint index)
        {
            if (!privateBinaryDataSetInitialized) //Checking if Object is initialized.
                throw new BinaryObjectNotInitializedException();
            if (index > ((privateNoOfBytes * 8) - 1)) //Checking if "index" is in range.
                throw new IllegalBitReferenceIndexException();
            return BitAtBitIndex(this.privateByteRepresentation, index);
        }

        #endregion

        #region Void Methods

        /// <summary>
        /// Sets all bytes to Zero.
        /// </summary>
        public void Clear()
        {
            Array.Clear(this.privateByteRepresentation, 0, this.privateByteRepresentation.Length);
        }

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
                if (BitAtBitIndex(this.privateByteRepresentation, index) == 1)
                    return;
                privateByteRepresentation[this.privateGetByteIndexAtBitIndex(index)] |= (byte)(1 << (7 - privateGetByteOffsetInArrayFromBitIndex(index)));
            }
            else
            {
                if (BitAtBitIndex(this.privateByteRepresentation, index) == 0)
                    return;
                privateByteRepresentation[this.privateGetByteIndexAtBitIndex(index)] &= (byte)~(1 << (7 - privateGetByteOffsetInArrayFromBitIndex(index)));
            }
        }

        #region SetObject Methods

        /// <summary>
        /// Sets the ByteRepresentation byte array according to the binary value denoted by the
        /// input String.
        /// </summary>
        /// <param name="binaryNumberAsString">Binary Number to be considered.</param>
        public void SetObject(string binaryNumberAsString)
        {
            stringInput = true;
            foreach (char c in binaryNumberAsString)
            {
                if (c == '1')
                    continue;
                if (c == '0')
                    continue;
                throw new InvalidBinaryStringException(); //Checking for non-binary characters.
            }
            if (!sizeConstrained)
                computeByteRepresentationLength(binaryNumberAsString); //Sets variable privateNoOfBytes.
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
            if (!sizeConstrained) //If object was not intialized with constructor accepting size of the object.
                privateByteRepresentation = new Byte[byteArray.Length];
            try
            {
                Array.Copy(byteArray, privateByteRepresentation, byteArray.Length);
            }
            catch (ArgumentException)
            {
                throw new IllegalBitReferenceIndexException();
            }
            updateAllPublicAttributes();
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Private Methods

        #region Non-Void Methods

        /// <summary>
        /// Private version of GetByteOffsetInArrayFromBitIndex which removes overhead of checking
        /// index reference consistency.
        /// </summary>
        /// <param name="index">Index of the bit in consideration.</param>
        /// <returns>Index of the bit referenced by the input Binary Digit index
        /// with respect to the byte of the byte array it is contained in.</returns>
        public byte privateGetByteOffsetInArrayFromBitIndex(uint index)
        {
            return (byte)(index - (index / 8) * 8);
        }

        /// <summary>
        /// Private method which removes overhead of checking index reference consistency.
        /// </summary>
        /// <param name="index">Index of Binary bit whose location is being considered.</param>
        /// <returns>Index of Byte in the ByteRepresentation Array containing 
        /// the bit at the specified Binary Digit index.
        /// <para>GetByteIndexAtBitIndex(17) will return the value '2'.</para></returns>
        public uint privateGetByteIndexAtBitIndex(uint index)
        {
            return index / 8;
        }

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
            ParseBinaryByteArrayToByteArray(binByteArrayTemp, outVal);
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
        private void computeByteRepresentationLength(String binaryNumAsString)
        {
            privateNoOfBytes = NoOfBytesToFitNoOfBits(binaryNumAsString.Length);
        }

        #endregion

        #endregion
    }
}