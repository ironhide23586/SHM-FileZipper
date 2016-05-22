using BinaryNumberClasses;
using System;
//using System.Threading;

namespace SHM_Zipper
{
    /// <summary>
    /// Class to encapsulate the SHM-File Zipping functionalities.
    /// </summary>
    public class SHMZipper
    {

        #region Class Properties

        #region Public Properties

        #region Readonly Properties

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
        /// Byte Array containing the compressed data.
        /// <para>NOTE : This is initialized only when the Compress() function
        /// is called explicitly.</para>
        /// </summary>
        public byte[] CompressedData
        {
            get { return privateCompressedData; }
        }

        #endregion

        #region Modifiable Properties

        /// <summary>
        /// File loaded onto this object represented as a byte array.
        /// </summary>
        public byte[] OriginalFileAsByteArray
        {
            get { return this.privateFileAsByteArray; }
            set { this.SetObject(value); }
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

        #endregion

        #endregion

        #region Private Properties

        #region Binary Properties

        /// <summary>
        /// Internal field containing the diagonal elements of the binary
        /// file matrix.
        /// </summary>
        private BinaryNumber privateFileDiagonalElements;

        /// <summary>
        /// Internally modifiable Central copy of the BinaryMatrix object.
        /// </summary>
        private BinaryMatrix privateFileMatrix;

        /// <summary>
        /// BinaryMatrix Object utilized for compression/decompression.
        /// </summary>
        public BinaryMatrix workingMatrix;

        /// <summary>
        /// Each entry of this matrix vaguely represents the probability of occurence of a '1' at the corresponding index of the 
        /// binary file matrix.
        /// </summary>
        public uint[,] probabilityMatrix;

        /// <summary>
        /// Represents the locations of the error bits. The error bit locations are marked by a '1'.
        /// </summary>
        public BinaryMatrix workingMatrix2;

        #endregion

        //#region Compression Logic fields

        ///// <summary>
        ///// Variables to denote which part of the matrix is assumed to be known.
        ///// </summary>
        //private bool rSelected, cSelected, dSelected, revDSelected;

        ///// <summary>
        ///// Indices of the row and column assumed to be known.
        ///// </summary>
        //private uint selRow, selCol;

        //#endregion

        /// <summary>
        /// Internal string holding location of file.
        /// </summary>
        private string privateFilePath;

        /// <summary>
        /// Internal byte array holding file data as byte array.
        /// </summary>
        private byte[] privateFileAsByteArray;

        /// <summary>
        /// Contains the size of the square binary file matrix in bits.
        /// </summary>
        private uint privateFileMatrixNoOfBitsInSide;

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
        /// Number of ones yet to be filled in each row of the binary working matrix.
        /// </summary>
        public uint[] privateVerticalNoOfOnesToBeFilled;

        /// <summary>
        /// Array storing number of '1's in each COLUMN of the file binary matrix.
        /// <para>This property contains the original values pertaining to the input file
        /// and is NOT ALTERED.</para>
        /// </summary>
        private uint[] privateHorizontalNoOfOnesCountStatic;

        /// <summary>
        /// Number of ones yet to be filled in each column of the binary working matrix.
        /// </summary>
        public uint[] privateHorizontalNoOfOnesToBeFilled;

        /// <summary>
        /// Array storing number of '1's in each REVERSE DIAGONAL of the file binary matrix.
        /// <para>This property contains the original values pertaining to the input file
        /// and is NOT ALTERED.</para>
        /// </summary>
        private uint[] privateRevDiagNoOfOnesCountStatic;

        /// <summary>
        /// Number of ones yet to be filled in each reverse diagonal of the binary working matrix.
        /// </summary>
        public uint[] privateRevDiagNoOfOnesToBeFilled;

        /// <summary>
        /// Array storing number of '1's in each DIAGONAL of the file binary matrix.
        /// <para>This property contains the original values pertaining to the input file
        /// and is NOT ALTERED.</para>
        /// </summary>
        private uint[] privateDiagNoOfOnesCountStatic;

        /// <summary>
        /// Number of ones yet to be filled in each diagonal of the binary working matrix.
        /// </summary>
        public uint[] privateDiagNoOfOnesToBeFilled;

        #endregion

        #endregion

        #region Class Constructors

        /// <summary>
        /// Constructor accepting path of the file, as one argument and whether to compress
        /// or decompress the file as another argument.
        /// </summary>
        /// <param name="inputFilePath">Path of the file to be processed.</param>
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
        /// <param name="inputFilePath">Path of the file to be processed.</param>
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

        #endregion

        #region Public Methods

        #region Void Methods

        #region SetObject Methods

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

        #endregion

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

        #endregion

        #endregion

        #region Private Methods

        #region Compression and Decompression methods

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

        #endregion

        /// <summary>
        /// Method to process input byte array of file into this object.
        /// <para>Called everytime this class is loaded with data.</para>
        /// </summary>
        /// <param name="byteArray">Input byte array.</param>
        private void acceptByteArray(byte[] byteArray)
        {
            privateFileAsByteArray = new byte[byteArray.Length];
            workingMatrix = new BinaryMatrix(privateFileAsByteArray);

            workingMatrix2 = new BinaryMatrix(privateFileAsByteArray);
            
            Array.Copy(byteArray, privateFileAsByteArray, byteArray.Length);
            privateFileMatrix = new BinaryMatrix(byteArray);
            this.privateFileMatrixNoOfBitsInSide = this.privateFileMatrix.BinaryMatrixNoOfBitsInSide; //To remove overhead of multiplication by 8 everytime this property is referred.
            initializeNoOfOnesArrays();
            privateFileDiagonalElements = (BinaryNumber)this.privateFileMatrix.Diagonal.Clone();
        }

        #region Dynamic Member Resetting Methods

        /// <summary>
        /// Clears the working matrix of all data.
        /// </summary>
        private void resetWorkingMatrix()
        {
            this.workingMatrix.Clear();
        }

        /// <summary>
        /// Resets the dynamic array(being used for compression/decompression) containing the number of '1's in each row to the original
        /// values as present in the input file.
        /// </summary>
        private void resetVerticalNoOfOnesCount()
        {
            Array.Copy(privateVerticalNoOfOnesCountStatic, privateVerticalNoOfOnesToBeFilled, privateVerticalNoOfOnesCountStatic.Length);
        }

        /// <summary>
        /// Resets the dynamic array(being used for compression/decompression) containing the number of '1's in each column to the original
        /// values as present in the input file.
        /// </summary>
        private void resetHorizontalNoOfOnesCount()
        {
            Array.Copy(privateHorizontalNoOfOnesCountStatic, privateHorizontalNoOfOnesToBeFilled, privateHorizontalNoOfOnesCountStatic.Length);
        }

        /// <summary>
        /// Resets the dynamic array(being used for compression/decompression) containing the number of '1's in each column to the original
        /// values as present in the input file.
        /// </summary>
        private void resetRevDiagNoOfOnesCount()
        {
            Array.Copy(privateRevDiagNoOfOnesCountStatic, privateRevDiagNoOfOnesToBeFilled, privateRevDiagNoOfOnesCountStatic.Length);
        }

        /// <summary>
        /// Resets the dynamic array(being used for compression/decompression) containing the number of '1's in each column to the original
        /// values as present in the input file.
        /// </summary>
        private void resetDiagNoOfOnesCount()
        {
            Array.Copy(privateDiagNoOfOnesCountStatic, privateDiagNoOfOnesToBeFilled, privateDiagNoOfOnesCountStatic.Length);
        }

        /// <summary>
        /// Resets all variables being employed for compression/decompression.
        /// </summary>
        private void resetDynamicComponents()
        {
            resetWorkingMatrix();
            resetHorizontalNoOfOnesCount();
            resetVerticalNoOfOnesCount();
            resetRevDiagNoOfOnesCount();
            resetDiagNoOfOnesCount();
        }

        #endregion

        #region NoOfOnes Initialization Methods

        /// <summary>
        /// Initializes the horizontal and vertical NoOfOnes arrays.
        /// </summary>
        private void initializeNoOfOnesArrays()
        {
            System.Threading.Thread revDiagNoOfOnesInitiializer = new System.Threading.Thread(() => initializePrivateRevDiagNoOfOnesCount());
            System.Threading.Thread diagNoOfOnesInitializer = new System.Threading.Thread (() => intializePrivateDiagNoOfOnesCount());
            System.Threading.Thread verticalNoOfOnesInitializer = new System.Threading.Thread(() => initializePrivateVerticalNoOfOnesCount());
            System.Threading.Thread horizontalNoOfOnesInitializer = new System.Threading.Thread(() => initializePrivateHorizontalNoOfOnesCount());
            verticalNoOfOnesInitializer.Start();
            horizontalNoOfOnesInitializer.Start();
            revDiagNoOfOnesInitiializer.Start();
            diagNoOfOnesInitializer.Start();
            while (!((verticalNoOfOnesInitializer.ThreadState == System.Threading.ThreadState.Stopped) && (horizontalNoOfOnesInitializer.ThreadState == System.Threading.ThreadState.Stopped) && (revDiagNoOfOnesInitiializer.ThreadState == System.Threading.ThreadState.Stopped) && (diagNoOfOnesInitializer.ThreadState == System.Threading.ThreadState.Stopped))) { }
        }

        /// <summary>
        /// Intializes the arrays pertaining to vertical No. of ones count, both static and dynamic.
        /// </summary>
        private void initializePrivateVerticalNoOfOnesCount()
        {
            uint rowIndex = 0;
            privateVerticalNoOfOnesCountStatic = new uint[this.privateFileMatrixNoOfBitsInSide];
            privateVerticalNoOfOnesToBeFilled = new uint[this.privateFileMatrixNoOfBitsInSide];
            foreach (BinaryNumber rowElement in privateFileMatrix.SplitRawBinaryData)
            {
                this.privateVerticalNoOfOnesCountStatic[rowIndex] = rowElement.NoOfOnes;
                this.privateVerticalNoOfOnesToBeFilled[rowIndex] = this.privateVerticalNoOfOnesCountStatic[rowIndex];
                rowIndex++;
            }
        }

        /// <summary>
        /// Intializes the arrays pertaining to horizontal No. of ones count, both static and dynamic.
        /// </summary>
        private void initializePrivateHorizontalNoOfOnesCount()
        {
            privateHorizontalNoOfOnesCountStatic = new uint[this.privateFileMatrixNoOfBitsInSide];
            privateHorizontalNoOfOnesToBeFilled = new uint[this.privateFileMatrixNoOfBitsInSide];
            uint unitSize = this.privateFileMatrixNoOfBitsInSide / 2;
            System.Threading.Thread horizontalNoOfOnesPartitionComputer1 = new System.Threading.Thread(() => processHorizontalNoOfOnes(0, unitSize));
            System.Threading.Thread horizontalNoOfOnesPartitionComputer2 = new System.Threading.Thread(() => processHorizontalNoOfOnes(unitSize, this.privateFileMatrixNoOfBitsInSide));
            horizontalNoOfOnesPartitionComputer1.Start();
            horizontalNoOfOnesPartitionComputer2.Start();
            horizontalNoOfOnesPartitionComputer1.Join();
            horizontalNoOfOnesPartitionComputer2.Join();
        }

        /// <summary>
        /// Initializes the arrays pertaining to the reverse diagonal No. of ones count, both static and dynamic.
        /// </summary>
        private void initializePrivateRevDiagNoOfOnesCount()
        {
            privateRevDiagNoOfOnesCountStatic = new uint[(this.privateFileMatrixNoOfBitsInSide * 2) - 1];
            privateRevDiagNoOfOnesToBeFilled = new uint[(this.privateFileMatrixNoOfBitsInSide * 2) - 1];
            uint j = 0, i, tmp, tmp2;
            for (; j < this.privateFileMatrixNoOfBitsInSide; j++) //Cycling through each diagonal, j -> Column index of starting of diagonal start point.
            {
                tmp = j;
                privateRevDiagNoOfOnesCountStatic[j] = 0;
                privateRevDiagNoOfOnesToBeFilled[j] = 0;
                for (i = 0; i <= j; i++) //Row cycling -> i
                {
                    if (this.getFileBitAt2DIndex(i, tmp--) == 1)
                    {
                        privateRevDiagNoOfOnesCountStatic[j]++;
                        privateRevDiagNoOfOnesToBeFilled[j]++;
                    }
                }
            }
            tmp = j;
            for (i = 1; i < this.privateFileMatrixNoOfBitsInSide; i++) // i -> Row index of diagonal start point.
            {
                privateRevDiagNoOfOnesCountStatic[tmp] = 0;
                privateRevDiagNoOfOnesToBeFilled[tmp] = 0;
                tmp2 = i;
                for (j = this.privateFileMatrixNoOfBitsInSide - 1; j >= i; j--)
                {
                    if (this.getFileBitAt2DIndex(tmp2++, j) == 1)
                    {
                        privateRevDiagNoOfOnesCountStatic[tmp]++;
                        privateRevDiagNoOfOnesToBeFilled[tmp]++;
                    }
                }
                tmp++;
            }
        }

        /// <summary>
        /// Initializes the arrays pertaining to the diagonal No. of ones count, both static and dynamic.
        /// </summary>
        private void intializePrivateDiagNoOfOnesCount()
        {
            privateDiagNoOfOnesCountStatic = new uint[(this.privateFileMatrixNoOfBitsInSide * 2) - 1];
            privateDiagNoOfOnesToBeFilled = new uint[(this.privateFileMatrixNoOfBitsInSide * 2) - 1];
            uint j = 0, i = 0, tmp, tmp2;
            for (; i < this.privateFileMatrixNoOfBitsInSide; i++) //Cycling through each diagonal, i -> Row index of starting of diagonal start point.
            {
                tmp = i;
                privateDiagNoOfOnesCountStatic[i] = 0;
                privateDiagNoOfOnesToBeFilled[i] = 0;
                for (j = this.privateFileMatrixNoOfBitsInSide - 1; j >= (this.privateFileMatrixNoOfBitsInSide - i - 1); j--) //Column cycling -> j
                {
                    if ((tmp > this.privateFileMatrixNoOfBitsInSide) || (j > this.privateFileMatrixNoOfBitsInSide))
                        break;
                    if (this.getFileBitAt2DIndex(tmp--, j) == 1)
                    {
                        privateDiagNoOfOnesCountStatic[i]++;
                        privateDiagNoOfOnesToBeFilled[i]++;
                    }
                }
            }
            tmp = i;
            for (j = this.privateFileMatrixNoOfBitsInSide - 2; j > 0; j--) // j -> Column index of diagonal start point.
            {
                privateDiagNoOfOnesCountStatic[tmp] = 0;
                privateDiagNoOfOnesToBeFilled[tmp] = 0;
                tmp2 = j;
                for (i = this.privateFileMatrixNoOfBitsInSide - 1; i >= (this.privateFileMatrixNoOfBitsInSide - j - 1); i--)
                {
                    if (this.getFileBitAt2DIndex(i, tmp2--) == 1)
                    {
                        privateDiagNoOfOnesCountStatic[tmp]++;
                        privateDiagNoOfOnesToBeFilled[tmp]++;
                    }
                }
                tmp++;
            }
        }

        /// <summary>
        /// Populates the array containing number of '1's in each column within the input indices.
        /// </summary>
        /// <param name="startCol">Index of starting column.</param>
        /// <param name="limitingCol">Index of ending column.</param>
        private void processHorizontalNoOfOnes(uint startCol, uint limitingCol)
        {
            uint colIndex = startCol, rowIndex;
            for (; colIndex < limitingCol; colIndex++)
            {
                privateHorizontalNoOfOnesCountStatic[colIndex] = 0;
                privateHorizontalNoOfOnesToBeFilled[colIndex] = 0;
                for (rowIndex = 0; rowIndex < this.privateFileMatrixNoOfBitsInSide; rowIndex++)
                {
                    if (getFileBitAt2DIndex(rowIndex, colIndex) == 1)
                    {
                        privateHorizontalNoOfOnesCountStatic[colIndex]++;
                        privateHorizontalNoOfOnesToBeFilled[colIndex]++;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets the bit of the file at the specified 2D index from the binary file matrix.
        /// </summary>
        /// <param name="rowIndex">Row Index from which to get the bit.</param>
        /// <param name="colIndex">Column Index from which to get the bit.</param>
        /// <returns>Bit value ('0' or '1') which is stored at the specified index.</returns>
        private byte getFileBitAt2DIndex(uint rowIndex, uint colIndex)
        {
            return this.privateFileMatrix.BitAt2DIndex(rowIndex, colIndex);
        }

        #region Compression Logic Methods

        /// <summary>
        /// Predicts the bit at given index based on input parameters (Used mainly during decompression).
        /// </summary>
        /// <param name="row">Row index of the bit to be predicted.</param>
        /// <param name="col">Column index of the bit to be predicted.</param>
        /// <returns>Predicted bit ('0' or '1') at the specified index.</returns>
        private byte predictBitAt2DIndex(uint row, uint col)
        {
            if ((privateVerticalNoOfOnesToBeFilled[row] > 0) && (privateHorizontalNoOfOnesToBeFilled[col] > 0) && (privateRevDiagNoOfOnesToBeFilled[getRevDiagIndexVector(row, col)] > 0) && (privateDiagNoOfOnesToBeFilled[getDiagIndexVector(row, col)] > 0))
                return 1;
            return 0;
        }

        /// <summary>
        /// Maps the 2D index of a bit to the corresponding index of the no. of ones in the pertaining reverse diagonal.
        /// </summary>
        /// <param name="row">Row index of the bit in the file matrix.</param>
        /// <param name="col">Column index of the bit in the file matrix.</param>
        /// <returns>Index of the array element containing the no. of ones in the reverse diagonal pertaining to the bit at the given index.</returns>
        private uint getRevDiagIndexVector(uint row, uint col)
        {
            return row + col;
        }

        /// <summary>
        /// Maps the 2D index of a bit to the corresponding index of the no. of ones in the pertaining diagonal.
        /// </summary>
        /// <param name="row">Row index of the bit in the file matrix.</param>
        /// <param name="col">Column index of the bit in the file matrix.</param>
        /// <returns>Index of the array element containing the no. of ones in the diagonal pertaining to the bit at the given index.</returns>
        private uint getDiagIndexVector(uint row, uint col)
        {
            return row - col + this.privateFileMatrixNoOfBitsInSide - 1;
        }

        /// <summary>
        /// Sets the predicted bit in working matrix and updates the NoOfOneCount dynamic array.
        /// </summary>
        /// <param name="row">Target row index.</param>
        /// <param name="col">Target column index.</param>
        private void setPredictedBitInWorkingMatrixAt2DIndex(uint row, uint col)
        {
            byte tmp = predictBitAt2DIndex(row, col);
            this.workingMatrix.SetBitAt2DIndex(row, col, tmp);
            if (tmp == 1)
                updateNoOfOnes(row, col);
        }

        /// <summary>
        /// Denotes whether bit predicted at given index is correct or not.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool predictedBitEqualToActualBit(uint row, uint col)
        {
            if (predictBitAt2DIndex(row, col) == this.privateFileMatrix.BitAt2DIndex(row, col))
                return true;
            return false;
        }

        private void updateDiagNoOfOnesArrays(uint row, uint col)
        {
            privateDiagNoOfOnesToBeFilled[getDiagIndexVector(row, col)]--;
            privateRevDiagNoOfOnesToBeFilled[getRevDiagIndexVector(row, col)]--;
        }

        private void updateProbabilityMatrixAt2DIndex(uint i, uint j)
        {
            probabilityMatrix[i, j] = privateHorizontalNoOfOnesToBeFilled[j] + privateVerticalNoOfOnesToBeFilled[i] + privateRevDiagNoOfOnesToBeFilled[getRevDiagIndexVector(i, j)] + privateDiagNoOfOnesToBeFilled[getDiagIndexVector(i, j)];
        }

        /// <summary>
        /// Populates the probability matrix.
        /// </summary>
        public void initializeProbabilityMatrix()
        {
            probabilityMatrix = new uint[this.privateFileMatrixNoOfBitsInSide, this.privateFileMatrixNoOfBitsInSide];
            uint i, j;
            System.Threading.Thread t1 = new System.Threading.Thread(() => probMatrixComputer(0, this.privateFileMatrixNoOfBitsInSide / 2));
            System.Threading.Thread t2 = new System.Threading.Thread(() => probMatrixComputer(this.privateFileMatrixNoOfBitsInSide / 2, this.privateFileMatrixNoOfBitsInSide));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
        }

        /// <summary>
        /// Worker thread to compute members of the probablity matrix.
        /// </summary>
        /// <param name="startIndex">Starting index of the row.</param>
        /// <param name="endIndex">End index of row.</param>
        private void probMatrixComputer(uint startIndex, uint endIndex)
        {
            for (uint i = startIndex, j; i < endIndex; i++)
            {
                for (j = 0; j < this.privateFileMatrixNoOfBitsInSide; j++)
                {
                    probabilityMatrix[i, j] = privateHorizontalNoOfOnesToBeFilled[j] + privateVerticalNoOfOnesToBeFilled[i] + privateRevDiagNoOfOnesToBeFilled[getRevDiagIndexVector(i, j)] + privateDiagNoOfOnesToBeFilled[getDiagIndexVector(i, j)];
                }
            }
        }

        /// <summary>
        /// Populates working matrix with known data.
        /// </summary>
        public void initializeWorkingMatrix(bool dSelected, bool revDSelected, bool rSelected, bool cSelected, uint selRow, uint selCol)
        {
            resetDynamicComponents(); //TO BE REMOVED

            #region Code to be removed later

            //if (dSelected)
            //{
            //    for (uint i = 0; i < this.privateFileMatrixNoOfBitsInSide; i++)
            //    {
            //        bitAtIndex = this.privateFileMatrix.BitAt2DIndex(i, i);
            //        workingMatrix.SplitRawBinaryData[i].SetBitAtIndex(i, bitAtIndex);
            //        //if (bitAtIndex == 1)
            //        //    updateNoOfOnes(i, i);
            //    }
            //}

            //if (revDSelected)
            //{
            //    for (uint i = 0, j = this.privateFileMatrixNoOfBitsInSide - 1; i < this.privateFileMatrixNoOfBitsInSide; i++)
            //    {
            //        bitAtIndex = this.privateFileMatrix.BitAt2DIndex(i, j);
            //        workingMatrix.SplitRawBinaryData[i].SetBitAtIndex(j, bitAtIndex);
            //        //if (bitAtIndex == 1)
            //        //    updateNoOfOnes(i, j);
            //        j--;
            //    }
            //}

            #endregion

            if (rSelected)
            {
                workingMatrix.SplitRawBinaryData[selRow].ByteRepresentation = this.privateFileMatrix.SplitRawBinaryData[selRow].ByteRepresentation; //TO BE REMOVED ;; Updating working matrix.
                for (uint i = 0; i < this.privateFileMatrixNoOfBitsInSide; i++)
                {
                    if (getFileBitAt2DIndex(selRow, i) == 1)
                    {
                        privateHorizontalNoOfOnesToBeFilled[i]--;
                        updateDiagNoOfOnesArrays(selRow, i);
                    }
                }
            }

            if (cSelected)
            {
                for (uint i = 0; i < this.privateFileMatrixNoOfBitsInSide; i++) //Cycles through rows.
                {
                    byte tmp = getFileBitAt2DIndex(i, selCol);
                    workingMatrix.SetBitAt2DIndex(i, selCol, tmp); //TO BE REMOVED ;; Updating working matrix.
                    if (tmp == 1) //To decrement the vertical NoOfOnes Count.
                    {
                        privateVerticalNoOfOnesToBeFilled[i]--;
                        if (((i != selRow) && (rSelected)) || (!rSelected)) //Checking if bit has already been accounted for.
                            updateDiagNoOfOnesArrays(i, selCol);
                    }
                }
            }

            if (rSelected)
                privateVerticalNoOfOnesToBeFilled[selRow] = 0;

            if (cSelected)
                privateHorizontalNoOfOnesToBeFilled[selCol] = 0;

            if (dSelected)
            {
                for (uint i = 0; i < this.privateFileMatrixNoOfBitsInSide; i++)
                {
                    if (((rSelected) && (i == selRow)) || ((cSelected) && (i == selCol))) //Checking if bit has been already accounted for.
                        continue;
                    if (this.privateFileMatrix.BitAt2DIndex(i, i) == 1)
                    {
                        updateNoOfOnes(i, i);
                        this.workingMatrix.SetBitAt2DIndex(i, i, 1); //TO BE REMOVED ;; Updating working matrix.
                    }
                }
            }

            if (revDSelected)
            {
                for (uint i = 0, j = this.privateFileMatrixNoOfBitsInSide - 1; i < this.privateFileMatrixNoOfBitsInSide; i++)
                {
                    if (((rSelected) && (i == selRow)) || ((cSelected) && (j == selCol))) //Checking if bit has already been accounted for.
                    {
                        j--;
                        continue;
                    }

                    if ((this.privateFileMatrix.BitAt2DIndex(i, j) == 1) && (((dSelected) && (i != j)) || (!dSelected)))
                    {
                        updateNoOfOnes(i, j);
                        this.workingMatrix.SetBitAt2DIndex(i, j, 1); //TO BE REMOVED ;; Updating working matrix.
                    }
                    j--;
                }
            }
        }

        /// <summary>
        /// Updates all the arrays holding the number of ones pertaining to the bit at given index.
        /// </summary>
        /// <param name="row">Row index of bit in consideration.</param>
        /// <param name="col">Column index of bit in consideration.</param>
        private void updateNoOfOnes(uint row, uint col)
        {
            this.privateVerticalNoOfOnesToBeFilled[row]--;
            this.privateHorizontalNoOfOnesToBeFilled[col]--;
            this.privateDiagNoOfOnesToBeFilled[getDiagIndexVector(row, col)]--;
            this.privateRevDiagNoOfOnesToBeFilled[getRevDiagIndexVector(row, col)]--;
        }

        public void setProbabilityMatrixDiagonalToZero()
        {
            uint i = 0;
            for (; i < this.privateFileMatrixNoOfBitsInSide; i++)
            {
                probabilityMatrix[i, i] = 0;
            }
        }

        public uint wrongCount = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dSelected"></param>
        /// <param name="revDSelected"></param>
        /// <param name="rSelected"></param>
        /// <param name="cSelected"></param>
        /// <param name="selRow"></param>
        /// <param name="selCol"></param>
        /// <returns></returns>
        public bool processWorkingMatrix(bool dSelected, bool revDSelected, bool rSelected, bool cSelected, uint selRow, uint selCol)
        {
            initializeWorkingMatrix(dSelected, revDSelected, rSelected, cSelected, selRow, selCol);
            initializeProbabilityMatrix();


            wrongCount = 0;

            fillWorkingMatrixUsingProbability(dSelected, revDSelected, rSelected, cSelected, selRow, selCol); //YET TO BE TESTED.
            //for (uint i = 0, j; i < this.privateFileMatrixNoOfBitsInSide; i++) //Cycling through rows
            //{
            //    for (j = 0; j < this.privateFileMatrixNoOfBitsInSide; j++) //Cycling through columns
            //    {
            //        predictAndPutBitAtIndex(dSelected, revDSelected, rSelected, cSelected, selRow, selCol, i, j);
            //    }
            //}

            avgWGrpSz /= wGrpCnt;
            return true;
        }

        /// <summary>
        /// Fills the working matrix by traversing it using probability heuristic.
        /// </summary>
        private void fillWorkingMatrixUsingProbability(bool dSelected, bool revDSelected, bool rSelected, bool cSelected, uint selRow, uint selCol)
        {
            uint maxProbRow = 0, maxProbCol = 0, maxProbVal = 1;
            while (true)
            {
                getMaxProbabilityIndex(ref maxProbRow, ref maxProbCol, ref maxProbVal);
                if (maxProbVal == 0)
                    break;
                predictAndPutBitAtIndex(dSelected, revDSelected, rSelected, cSelected, selRow, selCol, maxProbRow, maxProbCol);
                probabilityMatrix[maxProbRow, maxProbCol] = 0;
            }
        }

        /// <summary>
        /// Gets index of cell of probability matrix having maximum value.
        /// </summary>
        /// <param name="i">Contains row index of max. probability cell.</param>
        /// <param name="j">Contains column index of max. probability cell.</param>
        /// <param name="prob">Contains value stored in triangulated cell.</param>
        private void getMaxProbabilityIndex(ref uint i, ref uint j, ref uint maxProbabilityVal)
        {
            uint[] row = new uint[4];
            uint[] col = new uint[4];
            uint[] prob = new uint[4];
            populateMaxProbabilityComparerArrays(row, col, prob);
            uint maxProb = prob[0], maxProbIndex = 0;
            for (uint k = 1; k < 4; k++)
            {
                if (prob[k] > maxProb)
                {
                    maxProb = prob[k];
                    maxProbIndex = k;
                }
            }
            maxProbabilityVal = maxProb;
            i = row[maxProbIndex];
            j = col[maxProbIndex];
        }

        /// <summary>
        /// Method to get the highest 4 probability values from the 4 divisions of the probability matrix.
        /// </summary>
        /// <param name="row">Array to store the row indices of the max probability cells.</param>
        /// <param name="col">Array to store the column indices of the max probability cells</param>
        /// <param name="prob">Array to store the values of the probability for further comparison.</param>
        private void populateMaxProbabilityComparerArrays(uint[] row, uint[] col, uint[] prob)
        {
            uint unitSize = this.privateFileMatrixNoOfBitsInSide / 2;
            System.Threading.Thread t1 = new System.Threading.Thread(() => greatestProbability(0, unitSize, 0, unitSize, ref prob[0], ref row[0], ref col[0]));
            System.Threading.Thread t2 = new System.Threading.Thread(() => greatestProbability(0, unitSize, unitSize, this.privateFileMatrixNoOfBitsInSide, ref prob[1], ref row[1], ref col[1]));
            System.Threading.Thread t3 = new System.Threading.Thread(() => greatestProbability(unitSize, this.privateFileMatrixNoOfBitsInSide, 0, unitSize, ref prob[2], ref row[2], ref col[2]));
            System.Threading.Thread t4 = new System.Threading.Thread(() => greatestProbability(unitSize, this.privateFileMatrixNoOfBitsInSide, unitSize, this.privateFileMatrixNoOfBitsInSide, ref prob[3], ref row[3], ref col[3]));
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
        }

        /// <summary>
        /// Worker thread to compute greatest probability values from a given section of the probability matrix.
        /// </summary>
        /// <param name="lRow">Lower Row bound.</param>
        /// <param name="hRow">Upper Row bound.</param>
        /// <param name="lCol">Lower Column bound.</param>
        /// <param name="hCol">Upper column bound.</param>
        /// <param name="probability">Varible to store highest probability.</param>
        /// <param name="row">Variable to store row index of the cell having highest probability.</param>
        /// <param name="col">Variable to store column index of the cell having highest probability.</param>
        private void greatestProbability(uint lRow, uint hRow, uint lCol, uint hCol, ref uint probability, ref uint row, ref uint col)
        {
            probability = 0;
            for (uint i = lRow, j; i < hRow; i++)
            {
                for (j = lCol; j < hCol; j++)
                {
                    if (probabilityMatrix[i, j] > probability)
                    {
                        probability = probabilityMatrix[i, j];
                        row = i;
                        col = j;
                    }
                }
            }
        }

        public uint minRow = 4294967295, maxRow = 0, minCol = 4294967295, maxCol = 0, wrongBitDistance = 0, traversalCounter = 0, prevLoc = 0;

        public bool prevBitWrong;
        public uint wGrpSzTmp = 0, wGrpSz = 0;
        public double avgWGrpSz, wGrpCnt = 0;


        public void predictAndPutBitAtIndex(bool dSelected, bool revDSelected, bool rSelected, bool cSelected, uint selRow, uint selCol, uint i, uint j)
        {
            traversalCounter++;
            if ((rSelected) && (selRow == i)) //If row is already given.
                return;
            if ((cSelected) && (selCol == j)) //If column is already given.
                return;
            if ((dSelected) && (i == j)) //If diagonal given.
                return;
            if ((revDSelected) && (j == (this.privateFileMatrixNoOfBitsInSide - 1 - i))) //If reverse diagonal given.
                return;

            if (!predictedBitEqualToActualBit(i, j))
            {
                wrongCount++;
                workingMatrix2.SetBitAt2DIndex(i, j, 1);

                if ((traversalCounter - prevLoc) > wrongBitDistance)
                    wrongBitDistance = traversalCounter - prevLoc;
                prevLoc = traversalCounter;

                if (prevBitWrong || (wGrpSzTmp == 0))
                    wGrpSzTmp++;
                prevBitWrong = true;

                if (i < minRow)
                    minRow = i;
                if (i > maxRow)
                    maxRow = i;
                if (j < minCol)
                    minCol = j;
                if (j > maxCol)
                    maxCol = j;
                //Console.WriteLine("Wrong bit generated at location (" + i + ", " + j + ")");
            }
            else
            {
                setPredictedBitInWorkingMatrixAt2DIndex(i, j);

                if (prevBitWrong)
                {
                    ++wGrpCnt;
                    avgWGrpSz += (double)wGrpSzTmp;
                }

                if (wGrpSzTmp > wGrpSz)
                    wGrpSz = wGrpSzTmp;
                wGrpSzTmp = 0;
                prevBitWrong = false;
            }
            //setPredictedBitInWorkingMatrixAt2DIndex(i, j);
        }

        #endregion

        #endregion
    }
}