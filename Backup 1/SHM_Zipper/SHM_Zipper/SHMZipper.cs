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
        private uint[] privateVerticalNoOfOnesToBeFilled;

        /// <summary>
        /// Array storing number of '1's in each COLUMN of the file binary matrix.
        /// <para>This property contains the original values pertaining to the input file
        /// and is NOT ALTERED.</para>
        /// </summary>
        private uint[] privateHorizontalNoOfOnesCountStatic;

        /// <summary>
        /// Number of ones yet to be filled in each column of the binary working matrix.
        /// </summary>
        private uint[] privateHorizontalNoOfOnesToBeFilled;

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
        /// Resets all variables being employed for compression/decompression.
        /// </summary>
        private void resetDynamicComponents()
        {
            resetWorkingMatrix();
            resetHorizontalNoOfOnesCount();
            resetVerticalNoOfOnesCount();
        }

        #endregion

        #region NoOfOnes Initialization Methods

        /// <summary>
        /// Initializes the horizontal and vertical NoOfOnes arrays.
        /// </summary>
        private void initializeNoOfOnesArrays()
        {
            System.Threading.Thread verticalNoOfOnesInitializer = new System.Threading.Thread(() => initializePrivateVerticalNoOfOnesCount());
            System.Threading.Thread horizontalNoOfOnesInitializer = new System.Threading.Thread(() => initializePrivateHorizontalNoOfOnesCount());
            verticalNoOfOnesInitializer.Start();
            horizontalNoOfOnesInitializer.Start();
            while (!((verticalNoOfOnesInitializer.ThreadState == System.Threading.ThreadState.Stopped) && (horizontalNoOfOnesInitializer.ThreadState == System.Threading.ThreadState.Stopped))) { }
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
            //while (!((horizontalNoOfOnesPartitionComputer1.ThreadState == System.Threading.ThreadState.Stopped) && (horizontalNoOfOnesPartitionComputer2.ThreadState == System.Threading.ThreadState.Stopped))) { }
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
            if ((privateVerticalNoOfOnesToBeFilled[row] > 0) && (privateHorizontalNoOfOnesToBeFilled[col] > 0))
                return 1;
            return 0;
        }

        /// <summary>
        /// Sets the predicted bit in working matrix and updates the NoOfOneCount dynamic array.
        /// </summary>
        /// <param name="row">Target row index.</param>
        /// <param name="col">Target column index.</param>
        private void setPredictedBitInWorkingMatrixAt2DIndex(uint row, uint col)
        {
            byte tmp = predictBitAt2DIndex(row, col);
            //workingMatrix.SplitRawBinaryData[row].SetBitAtIndex(col, tmp);
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

        

        /// <summary>
        /// Populates working matrix with known data.
        /// </summary>
        public void initializeWorkingMatrix(bool dSelected, bool revDSelected, bool rSelected, bool cSelected, uint selRow, uint selCol)
        {
            byte bitAtIndex;

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
                workingMatrix.SplitRawBinaryData[selRow].ByteRepresentation = this.privateFileMatrix.SplitRawBinaryData[selRow].ByteRepresentation; //TO BE REMOVED
                for (uint i = 0; i < this.privateFileMatrixNoOfBitsInSide; i++)
                {
                    if (getFileBitAt2DIndex(selRow, i) == 1)
                        privateHorizontalNoOfOnesToBeFilled[i]--;
                }
            }

            if (cSelected)
            {
                for (uint i = 0; i < this.privateFileMatrixNoOfBitsInSide; i++) //Cycles through rows.
                {
                    byte tmp = getFileBitAt2DIndex(i, selCol);
                    workingMatrix.SplitRawBinaryData[i].SetBitAtIndex(selCol, tmp); //TO BE REMOVED
                    if (tmp == 1) //To decrement the vertical NoOfOnes Count.
                        privateVerticalNoOfOnesToBeFilled[i]--;
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
                    if (((rSelected) && (i == selRow)) || ((cSelected) && (i == selCol)))
                        continue;
                    if (this.privateFileMatrix.BitAt2DIndex(i, i) == 1)
                        updateNoOfOnes(i, i);
                }
            }

            if (revDSelected)
            {
                for (uint i = 0, j = this.privateFileMatrixNoOfBitsInSide - 1; i < this.privateFileMatrixNoOfBitsInSide; i++)
                {
                    if (((rSelected) && (i == selRow)) || ((cSelected) && (j == selCol)))
                    {
                        j--;
                        continue;
                    }

                    if ((this.privateFileMatrix.BitAt2DIndex(i, j) == 1) && (((dSelected) && (i != j)) || (!dSelected)))
                        updateNoOfOnes(i, j);
                    //{
                    //    if (((dSelected) && (i != j)) || (!dSelected))
                    //        updateNoOfOnes(i, j);
                        //else if (!dSelected)
                        //    updateNoOfOnes(i, j);
                    //}
                    //if ((this.privateFileMatrix.BitAt2DIndex(i, j) == 1) && ((dSelected) && (i != j)))
                    //    updateNoOfOnes(i, j);
                    j--;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void updateNoOfOnes(uint row, uint col)
        {
            this.privateVerticalNoOfOnesToBeFilled[row]--;
            this.privateHorizontalNoOfOnesToBeFilled[col]--;
        }

        uint currentWrongGroupSize = 0, currentRightGroupSize = 0; 

        public uint wrongCount = 0, wrongGroups = 0, maxWrongGroupSize = 0, maxRightGroupSize = 0;
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
            //Console.WriteLine("-----------------------------------------------------------------");
            initializeWorkingMatrix(dSelected, revDSelected, rSelected, cSelected, selRow, selCol);
            
            //Console.Write("Initial Horizontal NoOfOnes to be filled -> ");
            //foreach (uint i in privateHorizontalNoOfOnesToBeFilled)
            //    Console.Write(i + " ");
            //Console.WriteLine();
            //foreach (uint i in privateVerticalNoOfOnesToBeFilled)
            //    Console.WriteLine(i);
            bool prevBitRight = true;
            uint currentWrongGroupSize = 0, currentRightGroupSize = 0; //tmpI, tmpJ;
            for (uint i = 0, j; i < this.privateFileMatrixNoOfBitsInSide; i++) //Cycling through rows
            {
                //if ((rSelected) && (selRow == i)) //If row is already given.
                //    continue;
                for (j = 0; j < this.privateFileMatrixNoOfBitsInSide; j++) //Cycling through columns
                {
                    predictAndPutBitAtIndex(dSelected, revDSelected, rSelected, cSelected, selRow, selCol, i, j);
                    //if ((cSelected) && (selCol == j)) //If column is already given.
                    //    continue;
                    //if ((dSelected) && (i == j))
                    //    continue;
                    //if ((revDSelected) && (j == (this.privateFileMatrixNoOfBitsInSide - 1 - i)))
                    //    continue;
                    //tmpI = i;
                    //tmpJ = j;

                    //if (!predictedBitEqualToActualBit(i, j))
                    //{
                    //    if (prevBitRight)
                    //        wrongGroups++;
                    //    //Console.WriteLine("----->" + currentRightGroupSize);
                    //    if (maxRightGroupSize < currentRightGroupSize)
                    //        maxRightGroupSize = currentRightGroupSize;
                    //    currentRightGroupSize = 0;
                    //    currentWrongGroupSize++;
                        
                    //    //Console.WriteLine("dDeselected = " + dSelected + "\nrevDSelected = " + revDSelected + "\nrSelected = " + rSelected + "\ncSelected = " + cSelected + "\nselRow = " + selRow + "\nselCol = " + selCol + "\n" +  workingMatrix.StringRepresentation);
                    //    //Console.WriteLine("i = " + i + "\nj = " + j + "\n\n");
                    //    //Console.WriteLine("-----------------------------------------------------------------");
                    //    //return false;
                    //    wrongCount++;
                    //    prevBitRight = false;
                    //}
                    //else
                    //{
                    //    if (currentWrongGroupSize > maxWrongGroupSize)
                    //        maxWrongGroupSize = currentWrongGroupSize;
                    //    currentWrongGroupSize = 0;
                    //    currentRightGroupSize++;
                    //    prevBitRight = true;
                    //}
                    //setPredictedBitInWorkingMatrixAt2DIndex(i, j);
                    //Console.WriteLine(workingMatrix.StringRepresentation);
                    //Console.Write("Horizontal NoOfOnes to be filled -> ");
                    //foreach (uint p in privateHorizontalNoOfOnesToBeFilled)
                    //    Console.Write(p + " ");
                    //Console.WriteLine();
                    
                    //foreach (uint p in privateVerticalNoOfOnesToBeFilled)
                    //    Console.WriteLine(p);

                    //Console.WriteLine("i = " + i + "\nj = " + j + "\n");
                    //if (predictBitAt2DIndex(i, j) == 1)
                    //    updateNoOfOnes(i, j);
                }
            }
            return true;
        }

        public void predictAndPutBitAtIndex(bool dSelected, bool revDSelected, bool rSelected, bool cSelected, uint selRow, uint selCol, uint i, uint j)
        {
            bool prevBitRight = true;
            //uint currentWrongGroupSize = 0, currentRightGroupSize = 0;
            if ((rSelected) && (selRow == i)) //If row is already given.
                return;
            if ((cSelected) && (selCol == j)) //If column is already given.
                return;
            if ((dSelected) && (i == j))
                return;
            if ((revDSelected) && (j == (this.privateFileMatrixNoOfBitsInSide - 1 - i)))
                return;
            if (!predictedBitEqualToActualBit(i, j))
            {
                if (prevBitRight)
                    wrongGroups++;
                //Console.WriteLine("----->" + currentRightGroupSize);
                if (maxRightGroupSize < currentRightGroupSize)
                    maxRightGroupSize = currentRightGroupSize;
                currentRightGroupSize = 0;
                currentWrongGroupSize++;

                //Console.WriteLine("dDeselected = " + dSelected + "\nrevDSelected = " + revDSelected + "\nrSelected = " + rSelected + "\ncSelected = " + cSelected + "\nselRow = " + selRow + "\nselCol = " + selCol + "\n" +  workingMatrix.StringRepresentation);
                //Console.WriteLine("i = " + i + "\nj = " + j + "\n\n");
                //Console.WriteLine("-----------------------------------------------------------------");
                //return false;
                wrongCount++;
                prevBitRight = false;
            }
            else
            {
                if (currentWrongGroupSize > maxWrongGroupSize)
                    maxWrongGroupSize = currentWrongGroupSize;
                currentWrongGroupSize = 0;
                currentRightGroupSize++;
                prevBitRight = true;
            }
            setPredictedBitInWorkingMatrixAt2DIndex(i, j);
        }

        #endregion

        #endregion
    }
}