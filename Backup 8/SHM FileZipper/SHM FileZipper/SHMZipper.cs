using BinaryNumberClasses;
using System;
using System.Threading;

namespace SHM_FileZipper
{
    class SHMZipper
    {
        /// <summary>
        /// Central BinaryMatrix object of the file to be converted.
        /// </summary>
        public BinaryMatrix FileMatrix
        {
            get { return privateFileMatrix; }
        }

        /// <summary>
        /// Diagonal elements of the binary file 2D matrix
        /// </summary>
        public BinaryNumber FileDiagonalElements
        {
            get { return privateFileDiagonalElements; }
        }

        /// <summary>
        /// File loaded onto this object represented as a byte array.
        /// </summary>
        public byte[] FileAsByteArray
        {
            get { return this.privateFileAsByteArray; }
            set { this.SetObject(value); }
        }

        /// <summary>
        /// Array storing number of '1's in each ROW of the file binary matrix.
        /// </summary>
        public uint[] VerticalNoOfOnesCount
        {
            get { return privateVerticalNoOfOnesCountStatic; }
        }

        /// <summary>
        /// Array storing number of '1's in each COLUMN of the file binary matrix.
        /// </summary>
        public uint[] HorizontalNoOfOnesCount
        {
            get { return privateHorizontalNoOfOnesCountStatic; }
        }

        /// <summary>
        /// Path of the file to be processed;
        /// </summary>
        public string FilePath
        {
            get { return this.privateFilePath; }
            set { this.SetObject(value); }
        }

        public enum ProcessMode { COMPRESS, DECOMPRESS };
        
        /// <summary>
        /// Denotes whether the file loaded is to be compressed or decompressed.
        /// </summary>
        public ProcessMode ZipMode = ProcessMode.COMPRESS;

        /// <summary>
        /// Byte Array containing the compressed data.
        /// <para>NOTE : This is initialized only when the Compress() function
        /// is called explicitly.</para>
        /// </summary>
        public byte[] CompressedData
        {
            get { return privateCompressedData; }
        }

        /// <summary>
        /// Denotes if all internal threads have completed or not.
        /// </summary>
        public bool AllThreadsComplete
        {
            get
            {
                //if ((verticalNoOfOnesInitializer.ThreadState == ThreadState.Stopped) && (horizontalNoOfOnesInitializer.ThreadState == ThreadState.Stopped) && (fileDiagonalElementsInitializer.ThreadState == ThreadState.Stopped))
                if ((noOfOnesInitializer.ThreadState == ThreadState.Stopped) && (fileDiagonalElementsInitializer.ThreadState == ThreadState.Stopped))
                    return true;
                return false;
            }
        }


        #region Thread Definitions

        private Thread verticalNoOfOnesInitializer;

        private Thread horizontalNoOfOnesInitializer;

        private Thread noOfOnesInitializer;

        private Thread fileDiagonalElementsInitializer;

        #endregion

        /// <summary>
        /// Internal string holding location of file.
        /// </summary>
        private string privateFilePath;

        /// <summary>
        /// Internal field containing the diagonal elements of the binary
        /// file matrix.
        /// </summary>
        private BinaryNumber privateFileDiagonalElements;

        /// <summary>
        /// Internal byte array holding file data as byte array.
        /// </summary>
        private byte[] privateFileAsByteArray;

        /// <summary>
        /// Contains the size of the square binary file matrix in bits.
        /// </summary>
        private uint privateFileMatrixNoOfBitsInSide;

        /// <summary>
        /// Internally modifiable Central copy of the BinaryMatrix object.
        /// </summary>
        private BinaryMatrix privateFileMatrix;

        /// <summary>
        /// Internal byte array containing compressed data.
        /// </summary>
        private byte[] privateCompressedData;

        /// <summary>
        /// Array storing number of '1's in each ROW of the file binary matrix.
        /// <para>This property contains the original values pertaining to the input file
        /// and is NOT ALTERED.</para>
        /// </summary>
        private uint[] privateVerticalNoOfOnesCountStatic;

        /// <summary>
        /// Array storing number of '1's in each ROW of the file binary matrix.
        /// <para>This property contains the original values pertaining to the input file
        /// but is ALTERED as the file is processed.</para>
        /// </summary>
        private uint[] privateVerticalNoOfOnesCountDynamic;

        /// <summary>
        /// Array storing number of '1's in each COLUMN of the file binary matrix.
        /// <para>This property contains the original values pertaining to the input file
        /// and is NOT ALTERED.</para>
        /// </summary>
        private uint[] privateHorizontalNoOfOnesCountStatic;

        /// <summary>
        /// Array storing number of '1's in each COLUMN of the file binary matrix.
        /// <para>This property contains the original values pertaining to the input file
        /// but is ALTERED as the file is processed.</para>
        /// </summary>
        private uint[] privateHorizontalNoOfOnesCountDynamic;

        /// <summary>
        /// Constructor accepting path of the file, as one argument and whether to compress
        /// or decompress the file as another argument.
        /// </summary>
        /// <param name="inputFile">Path of the file to be processed.</param>
        /// <param name="zMode">To denote whether to compress or decompress the file.</param>
        public SHMZipper(string inputFilePath, ProcessMode zMode)
        {
            this.ZipMode = zMode;
            this.SetObject(inputFilePath);
        }

        /// <summary>
        /// Constructor accepting only path of the file to be compressed as argument.
        /// <para>NOTE : By default the file process mode is set to "COMPRESS"</para>
        /// </summary>
        /// <param name="inputFile">Path of the file to be processed.</param>
        public SHMZipper(string inputFilePath)
        {
            this.SetObject(inputFilePath);
        }

        /// <summary>
        /// Constructor accepting a raw byte array for this object.
        /// </summary>
        /// <param name="fileAsByteArray">Input Byte Array.</param>
        public SHMZipper(byte[] fileAsByteArray)
        {
            this.SetObject(fileAsByteArray);
        }

        /// <summary>
        /// Empty constructor.
        /// <para>NOTE : It is important to initialize this object atleast once before 
        /// performing any compression.</para>
        /// </summary>
        public SHMZipper() { }

        /// <summary>
        /// Method to initialize this object with a file
        /// </summary>
        /// <param name="inputFilePath">Path of the file.</param>
        public void SetObject(string inputFilePath)
        {
            privateFilePath = inputFilePath;
            byte[] inputFile = System.IO.File.ReadAllBytes(inputFilePath);
            acceptByteArray(inputFile);
        }

        /// <summary>
        /// Method to initialize this object with a byte array.
        /// </summary>
        /// <param name="inputFileAsByteArray">Input byte array.</param>
        public void SetObject(byte[] inputFileAsByteArray)
        {
            acceptByteArray(inputFileAsByteArray);
        }

        /// <summary>
        /// SHM-Compresses or SHM-Decompresses the file with this object is initialized.
        /// </summary>
        public void ProcessFile()
        {
            if (this.ZipMode == ProcessMode.COMPRESS)
                this.compress();
            else
                this.decompress();
        }

        /// <summary>
        /// Gets the bit of the file at the specified 2D index from the binary file matrix.
        /// </summary>
        /// <param name="rowIndex">Row Index from which to get the bit.</param>
        /// <param name="colIndex">Column Index from which to get the bit.</param>
        /// <returns>Bit value ('0' or '1') which is stored at the specified index.</returns>
        public byte GetFileBitAt2DIndex(uint rowIndex, uint colIndex)
        {
            return BinaryStaticClass.BitAtBitIndex(privateFileMatrix.SplitRawBinaryData[rowIndex].ByteRepresentation, colIndex);
        }

        /// <summary>
        /// Method to perform SHM-File Compression.
        /// </summary>
        private void compress()
        {
        }

        /// <summary>
        /// Method to perform SHM-File Decompression.
        /// </summary>
        private void decompress()
        {
        }

        /// <summary>
        /// Initializes the thread objects in this class.
        /// </summary>
        private void initializeThreadObjects()
        {
            horizontalNoOfOnesInitializer = new Thread(() => initializePrivateHorizontalNoOfOnesCount());
            horizontalNoOfOnesInitializer.Priority = ThreadPriority.Highest;
            verticalNoOfOnesInitializer = new Thread(() => initializePrivateVerticalNoOfOnesCount());
            verticalNoOfOnesInitializer.Priority = ThreadPriority.Highest;
            noOfOnesInitializer = new Thread(() => initializeNoOfOnesArrays());
            noOfOnesInitializer.Priority = ThreadPriority.Highest;
            fileDiagonalElementsInitializer = new Thread(() => initializePrivateFileDiagonalElements());
            fileDiagonalElementsInitializer.Priority = ThreadPriority.Highest;
        }

        /// <summary>
        /// Method to process input byte array of file into this object.
        /// </summary>
        /// <param name="byteArray">Input byte array.</param>
        private void acceptByteArray(byte[] byteArray)
        {
            this.privateFileAsByteArray = (byte[])byteArray.Clone();
            Array.Reverse(byteArray);
            privateFileMatrix = new BinaryMatrix(byteArray);
            this.privateFileMatrixNoOfBitsInSide = this.privateFileMatrix.BinaryMatrixNoOfBitsInSide; //To remove overhead of multiplication by 8 everytime this property is referred.
            //initializeThreadObjects();
            //noOfOnesInitializer.Start();
            //fileDiagonalElementsInitializer.Start();
            initializeNoOfOnesArrays();
            initializePrivateFileDiagonalElements();
        }

        /// <summary>
        /// Initializes the horizontal and vertical NoOfOnes arrays.
        /// </summary>
        private void initializeNoOfOnesArrays()
        {
            //horizontalNoOfOnesInitializer.Start();
            //verticalNoOfOnesInitializer.Start();
            //while (!((horizontalNoOfOnesInitializer.ThreadState == ThreadState.Stopped) && (verticalNoOfOnesInitializer.ThreadState == ThreadState.Stopped))) { }
            initializePrivateVerticalNoOfOnesCount();
            initializePrivateHorizontalNoOfOnesCount();
        }

        /// <summary>
        /// Intializes the arrays pertaining to vertical No. of ones count, both static and dynamic.
        /// </summary>
        private void initializePrivateVerticalNoOfOnesCount()
        {
            uint rowIndex = 0;
            privateVerticalNoOfOnesCountStatic = new uint[this.privateFileMatrixNoOfBitsInSide];
            privateVerticalNoOfOnesCountDynamic = new uint[this.privateFileMatrixNoOfBitsInSide];
            foreach (BinaryNumber rowElement in privateFileMatrix.SplitRawBinaryData)
            {
                this.privateVerticalNoOfOnesCountStatic[rowIndex] = rowElement.NoOfOnes;
                this.privateVerticalNoOfOnesCountDynamic[rowIndex] = this.privateVerticalNoOfOnesCountStatic[rowIndex];
                rowIndex++;
            }
        }

        /// <summary>
        /// Intializes the arrays pertaining to horizontal No. of ones count, both static and dynamic.
        /// </summary>
        private void initializePrivateHorizontalNoOfOnesCount()
        {
            uint colIndex = 0, rowIndex;
            privateHorizontalNoOfOnesCountStatic = new uint[this.privateFileMatrixNoOfBitsInSide];
            privateHorizontalNoOfOnesCountDynamic = new uint[this.privateFileMatrixNoOfBitsInSide];
            for (; colIndex < this.privateFileMatrixNoOfBitsInSide; colIndex++)
            {
                privateHorizontalNoOfOnesCountStatic[colIndex] = 0;
                privateHorizontalNoOfOnesCountDynamic[colIndex] = 0;
                for (rowIndex = 0; rowIndex < this.privateFileMatrixNoOfBitsInSide; rowIndex++)
                {
                    if (GetFileBitAt2DIndex(rowIndex, colIndex) == 1)
                    {
                        privateHorizontalNoOfOnesCountStatic[colIndex]++;
                        privateHorizontalNoOfOnesCountDynamic[colIndex]++;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the field holding the diagonal elements of the file.
        /// </summary>
        private void initializePrivateFileDiagonalElements()
        {
            string diagonalBinaryString = "";
            for (uint i = 0; i < privateFileMatrixNoOfBitsInSide; i++)
            {
                diagonalBinaryString += this.GetFileBitAt2DIndex(i, i);
            }
            privateFileDiagonalElements = new BinaryNumber(diagonalBinaryString);
        }
    }
}
