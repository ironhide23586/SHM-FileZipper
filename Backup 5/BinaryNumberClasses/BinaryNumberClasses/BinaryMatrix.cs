using System;

/*
 * Contains definition of BinaryMatrix Class.
 * 
 * AUTHOR : SOUHAM BISWAS
 * 
 */

namespace BinaryNumberClasses
{
    /// <summary>
    /// Class to encapsulate 2D matrix of Binary Numbers.
    /// </summary>
    public class BinaryMatrix : BinaryStaticClass, ICloneable
    {
        #region Class Properties

        #region Public Properties

        #region Readonly Properties

        /// <summary>
        /// Diagonal elements of the 2D Binary matrix.
        /// </summary>
        public BinaryNumber Diagonal
        {
            get { return this.GetMatrixDiagonal(); }
        }

        /// <summary>
        /// Opposite Diagonal elements of the 2D Binary matrix.
        /// </summary>
        public BinaryNumber ReverseDiagonal
        {
            get { return this.GetMatrixReverseDiagonal(); }
        }

        /// <summary>
        /// RawBinaryData (input binary data) split into partitions with size equal to the
        /// length of the square matrix side in which it is fitted.
        /// </summary>
        public BinaryNumber[] SplitRawBinaryData
        {
            get { return privateSplitInputBinaryData; }
        }

        /// <summary>
        /// Denotes if the object of this class has been initialized with data or not.
        /// </summary>
        public bool BinaryDataSetInitialized
        {
            get { return privateBinaryDataSetInitialized; }
        }

        /// <summary>
        /// When the byte array is arranged as a square matrix, this is the number of bytes in
        /// a side.
        /// </summary>
        public uint BinaryMatrixNoOfBytesInSide
        {
            get { return privateBinaryMatrixNoOfBytesInSide; }
        }

        /// <summary>
        /// When the byte array is arranged as a square matrix, this is the number of bits in
        /// a side.
        /// </summary>
        public uint BinaryMatrixNoOfBitsInSide
        {
            get { return privateBinaryMatrixNoOfBitsInSide; }
        }

        #endregion

        #region Modifiable Properties

        /// <summary>
        /// Central BinaryNumber object containing raw binary data.
        /// </summary>
        public BinaryNumber RawBinaryData
        {
            get { return inputBinaryDataOriginal; }
            set { this.SetObject(value); }
        }

        /// <summary>
        /// Binary data in the matrix formatted as a square.
        /// </summary>
        public string StringRepresentation
        {
            get
            {
                string outString = "";
                foreach (BinaryNumber bn in this.privateSplitInputBinaryData)
                {
                    outString += bn.StringRepresentation + "\n";
                }
                return outString;
            }
        }

        /// <summary>
        /// Stream of bytes containing the information stored in this matrix.
        /// </summary>
        public byte[] ByteRepresentation
        {
            get
            { return this.RawBinaryData.ByteRepresentation; }
            set
            { this.SetObject(value); }
        }

        #endregion

        #endregion

        #region Private Properties

        /// <summary>
        /// Set to true when object of this class is initialized with data.
        /// </summary>
        private bool privateBinaryDataSetInitialized;

        /// <summary>
        /// Central BinaryNumber object containing raw data from input object.
        /// </summary>
        private BinaryNumber inputBinaryDataOriginal;

        /// <summary>
        /// Central BinaryNumber object whose contents have been aligned to fit inside a 
        /// square binary matrix.
        /// </summary>
        private BinaryNumber inputBinaryDataOffsetted;

        /// <summary>
        /// When the byte array is arranged as a square matrix, this is the number of bytes in
        /// a side. This is an internal editable field.
        /// </summary>
        private uint privateBinaryMatrixNoOfBytesInSide;

        /// <summary>
        /// When the byte array is arranged as a square matrix, this is the number of bits in
        /// a side. This is an internal editable field.
        /// </summary>
        private uint privateBinaryMatrixNoOfBitsInSide;

        /// <summary>
        /// Internal member containing all the rows of the matrix.
        /// </summary>
        private BinaryNumber[] privateSplitInputBinaryData;

        #endregion

        #endregion

        #region Class Contructors

        /// <summary>
        /// Constructor accepting byte array as argument.
        /// </summary>
        /// <param name="inpByteArr">Input Byte Array</param>
        public BinaryMatrix(byte[] inpByteArr)
        {
            this.SetObject(inpByteArr);
        }

        /// <summary>
        /// Constructor accepting BinaryNumber object as argument.
        /// </summary>
        /// <param name="inpBinaryNumber">Input BinaryNumber object.</param>
        public BinaryMatrix(BinaryNumber inpBinaryNumber)
        {
            this.SetObject(inpBinaryNumber);
        }

        /// <summary>
        /// Constructor with no arguments.
        /// <para>NOTE : This object must be initialized before manipulating it, else
        /// BinaryObjectNotInitializedException will be thrown.</para>
        /// </summary>
        public BinaryMatrix() { }

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
        /// Returns a copy of the object of this class.
        /// </summary>
        /// <returns>Copy of the object of this class.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Gets the diagonal elements of the binary matrix.
        /// </summary>
        /// <returns>A BinaryNumber object made up of the diagonal bits.</returns>
        public BinaryNumber GetMatrixDiagonal()
        {
            byte[] tmpArr = new byte[this.privateBinaryMatrixNoOfBitsInSide], outArr = new byte[NoOfBytesToFitNoOfBits((int)this.privateBinaryMatrixNoOfBitsInSide)];
            for (uint i = 0; i < this.privateBinaryMatrixNoOfBitsInSide; i++)
            {
                tmpArr[i] = this.BitAt2DIndex(i, i);
            }
            Array.Reverse(tmpArr);
            ParseBinaryByteArrayToByteArray(tmpArr, outArr);
            Array.Reverse(outArr);
            return new BinaryNumber(outArr);
        }

        /// <summary>
        /// Gets the opposite (right to left) diagonal elements of the binary matrix.
        /// </summary>
        /// <returns>A BinaryNumber object made up of the diagonal bits.</returns>
        public BinaryNumber GetMatrixReverseDiagonal()
        {
            byte[] tmpArr = new byte[this.privateBinaryMatrixNoOfBitsInSide], outArr = new byte[NoOfBytesToFitNoOfBits((int)this.privateBinaryMatrixNoOfBitsInSide)];
            for (uint i = 0, j = this.privateBinaryMatrixNoOfBitsInSide - 1; i < this.privateBinaryMatrixNoOfBitsInSide; i++)
            {
                tmpArr[i] = this.BitAt2DIndex(i, j--);
            }
            Array.Reverse(tmpArr);
            ParseBinaryByteArrayToByteArray(tmpArr, outArr);
            Array.Reverse(outArr);
            return new BinaryNumber(outArr);
        }

        /// <summary>
        /// Gets the bit ('0' or '1') at given index of this object.
        /// </summary>
        /// <param name="rowIndex">Index of the row from which the bit is to be retrieved.</param>
        /// <param name="colIndex">Index of the column from which the bit is to be retrieved.</param>
        /// <returns>Bit stored at index (rowIndex, colIndex) of this object.</returns>
        public byte BitAt2DIndex(uint rowIndex, uint colIndex)
        {
            return privateSplitInputBinaryData[rowIndex].GetBitAtIndex(colIndex);
        }

        #endregion

        #region Void Methods

        /// <summary>
        /// Sets the bit ('0' or '1') at the given index of this object.
        /// </summary>
        /// <param name="rowIndex">Row index of the bit to be set.</param>
        /// <param name="colIndex">Column index of the bit to be set.</param>
        public void SetBitAt2DIndex(uint rowIndex, uint colIndex, byte bit)
        {
            privateSplitInputBinaryData[rowIndex].SetBitAtIndex(colIndex, bit);
        }

        /// <summary>
        /// Intializes all data in this object to '0';
        /// </summary>
        public void Clear()
        {
            this.inputBinaryDataOriginal.Clear();
            this.inputBinaryDataOffsetted.Clear();
            for (uint i = 0; i < privateSplitInputBinaryData.Length; i++)
            {
                privateSplitInputBinaryData[i].Clear();
            }
        }

        /// <summary>
        /// Method to set central object of this class.
        /// </summary>
        /// <param name="inpBinaryNumber">Input BinaryNumber object.</param>
        public void SetObject(BinaryNumber inpBinaryNumber)
        {
            inputBinaryDataOriginal = (BinaryNumber)inpBinaryNumber.Clone();
            setObjectFinalize();
        }

        /// <summary>
        /// Method to set central object of this class.
        /// </summary>
        /// <param name="inpByteArr">Input byte array.</param>
        public void SetObject(byte[] inpByteArr)
        {
            inputBinaryDataOriginal = new BinaryNumber(inpByteArr);
            setObjectFinalize();
        }

        #endregion

        #endregion

        #endregion

        #region Private Methods

        #region Non-Void Methods

        /// <summary>
        /// Computes the number of bits to offset the contents of the input raw binary data,
        /// So as to fit them into a square binary matrix.
        /// </summary>
        /// <returns>The number of bits (offset) by which the original data must be shifted.</returns>
        private uint compute2DBinaryOffset()
        {
            uint r = (uint)(System.Math.Ceiling(System.Math.Sqrt(this.inputBinaryDataOriginal.NoOfBits) / 8));
            return 64 * r * r - this.inputBinaryDataOriginal.NoOfBits;
        }

        #endregion

        #region Void Methods

        /// <summary>
        /// Initializes the SplitRawBinaryData property of this object.
        /// </summary>
        private void computeSplitRawBinaryData()
        {
            privateSplitInputBinaryData = new BinaryNumber[privateBinaryMatrixNoOfBitsInSide]; //Array, whose each element represents each row of the binary matrix.
            uint startByteIndex = 0, endByteIndex = privateBinaryMatrixNoOfBytesInSide - 1;
            for (uint i = 0; i < this.privateBinaryMatrixNoOfBitsInSide; i++)
            {
                privateSplitInputBinaryData[i] = new BinaryNumber(this.privateBinaryMatrixNoOfBytesInSide);
                if (startByteIndex > inputBinaryDataOffsetted.NoOfBytes - 1) //Boundary Condition
                    break;
                byte[] tmp = inputBinaryDataOffsetted.ExtractBytes(startByteIndex, endByteIndex).ByteRepresentation;
                privateSplitInputBinaryData[i].ByteRepresentation = tmp;
                //if (tmp.Length > 1)
                //    Array.Reverse(privateSplitInputBinaryData[i].ByteRepresentation);
                startByteIndex = endByteIndex + 1;
                endByteIndex = startByteIndex + privateBinaryMatrixNoOfBytesInSide - 1;
            }
        }

        /// <summary>
        /// Final method called at the end of every SetObject() function which updates the necessary
        /// properties pertaining to the input binary raw data of the object of this class.
        /// </summary>
        private void setObjectFinalize()
        {
            this.computeBinaryMatrixNoOfBytesInSide(); //Calculate Square matrix side length
            this.initializeInputBinaryDataOffsetted(); //Initialize offsetted binary data
            this.computeSplitRawBinaryData(); //Initialize square matrix
            this.privateBinaryDataSetInitialized = true; //All data values set. Setting flag.
        }

        /// <summary>
        /// Gets the length of the side of the square binary matrix in bytes.
        /// </summary>
        private void computeBinaryMatrixNoOfBytesInSide()
        {
            this.privateBinaryMatrixNoOfBitsInSide = (uint)System.Math.Sqrt(this.inputBinaryDataOriginal.NoOfBits + compute2DBinaryOffset());
            this.privateBinaryMatrixNoOfBytesInSide = this.privateBinaryMatrixNoOfBitsInSide / 8;
        }

        /// <summary>
        /// Initializes the contents of InputBinaryDataOffsetted field by accordingly shifting
        /// the contents.
        /// </summary>
        private void initializeInputBinaryDataOffsetted()
        {
            inputBinaryDataOffsetted = new BinaryNumber(this.inputBinaryDataOriginal.NoOfBytes + compute2DBinaryOffset() / 8);
            byte[] tmp = new byte[inputBinaryDataOriginal.NoOfBytes];
            inputBinaryDataOriginal.ExtractBytes(0, inputBinaryDataOriginal.NoOfBytes - 1).ByteRepresentation.CopyTo(tmp, 0);
            inputBinaryDataOffsetted.ByteRepresentation = tmp;
        }

        #endregion

        #endregion
    }
}