using BinaryNumberClasses;
using SHM_Zipper;
//using SHM_FileZipper_Used_Classes;
using System;
using System.IO;
using System.Threading;

namespace SHM_FileZipper
{
    class Program
    {
        static SHMZipper sz;
        static uint lowestWrongCount = 4294967295;

        static void procRow(uint start, uint end)
        {
            uint wCount;
            for (uint i = start; i < end; i++) //Row
            {
                if (sz.processWorkingMatrix(false, false, true, false, i, 0))
                {
                    //if (i == 0)
                    //    lowestWrongCount = sz.wrongCount;
                    //while (solnMethodBusy) { }
                    //solnFoundMethod(sz.wrongCount);
                    //return;
                    wCount = sz.wrongCount;
                    Console.WriteLine(wCount);
                    if (wCount == 0)
                        Console.Read();
                    if (lowestWrongCount > wCount)
                        lowestWrongCount = wCount;
                    sz.wrongCount = 0;
                }
            }
        }

        static void calculate(SHMZipper sz)
        {
            //Console.WriteLine(lowestWrongCount);
            uint i, j;
            Console.WriteLine("Row -");
            uint mid = sz.FileMatrix.BinaryMatrixNoOfBitsInSide / 2;
            //Row
            //Thread t1 = new Thread(() => procRow(0, mid)); 
            //Thread t2 = new Thread(() => procRow(mid, sz.FileMatrix.BinaryMatrixNoOfBitsInSide));
            //t1.Start();
            //t2.Start();
            //t1.Join();
            //t2.Join();

            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Row
            //{
            //    if (sz.processWorkingMatrix(false, false, true, false, i, 0))
            //    {
            //        //if (i == 0)
            //        //    lowestWrongCount = sz.wrongCount;
            //        solnFoundMethod(sz.wrongCount);
            //        //return;
            //    }
            //}
            //Console.WriteLine("\nCol -");
            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Col
            //{
            //    if (sz.processWorkingMatrix(false, false, false, true, 0, i))
            //    {
            //        solnFoundMethod(sz.wrongCount);
            //        //return;
            //    }
            //}
            //Console.WriteLine("\nDiag -");
            //if (sz.processWorkingMatrix(true, false, false, false, 0, 0)) //Diag
            //{
            //    solnFoundMethod(sz.wrongCount);
            //    //return;
            //}
            //Console.WriteLine("\nRev Diag -");
            //if (sz.processWorkingMatrix(false, true, false, false, 0, 0)) //Rev Diag
            //{
            //    solnFoundMethod(sz.wrongCount);
            //    //return;
            //}
            //Console.WriteLine("\nBoth Diag -");
            //if (sz.processWorkingMatrix(true, true, false, false, 0, 0)) //Diag + Rev Diag
            //{
            //    solnFoundMethod(sz.wrongCount);
            //    //return;
            //}
            //Console.WriteLine("\nRow + Col -");
            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Row + Col
            //{
            //    for (j = 0; j < sz.FileMatrix.SplitRawBinaryData.Length; j++)
            //    {
            //        if (sz.processWorkingMatrix(false, false, true, true, i, j))
            //        {
            //            solnFoundMethod(sz.wrongCount);
            //            //return;
            //        }
            //    }
            //}
            //Console.WriteLine("\nDiag + Row -");
            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Diag + row 
            //{
            //    if (sz.processWorkingMatrix(true, false, true, false, i, 0))
            //    {
            //        solnFoundMethod(sz.wrongCount);
            //        //return;
            //    }
            //}
            //Console.WriteLine("\nRev Diag + row -");
            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Rev diag + row
            //{
            //    if (sz.processWorkingMatrix(false, true, true, false, i, 0))
            //    {
            //        solnFoundMethod(sz.wrongCount);
            //        //return;
            //    }
            //}
            //Console.WriteLine("\nDiag + Col -");
            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Diag + col
            //{
            //    if (sz.processWorkingMatrix(true, false, false, true, 0, i))
            //    {
            //        solnFoundMethod(sz.wrongCount);
            //        //return;
            //    }
            //}
            //Console.WriteLine("\nRev Diag + Col -");
            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Rev Diag + col
            //{
            //    if (sz.processWorkingMatrix(false, true, false, true, 0, i))
            //    {
            //        solnFoundMethod(sz.wrongCount);
            //        //return;
            //    }
            //}
            //Console.WriteLine("\nDiag + Rev Diag + Row -");
            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Diag + Rev Diag + Row
            //{
            //    if (sz.processWorkingMatrix(true, true, true, false, i, 0))
            //    {
            //        solnFoundMethod(sz.wrongCount);
            //        //return;
            //    }
            //}
            //Console.WriteLine("\nDiag + Rev Diag + Col -");
            //for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Diag + Rev Diag + Col
            //{
            //    if (sz.processWorkingMatrix(true, true, false, true, 0, i))
            //    {
            //        solnFoundMethod(sz.wrongCount);
            //        //return;
            //    }
            //}
            Console.WriteLine("\nDiag + Rev Diag + Row + Col -");
            for (i = 0; i < sz.FileMatrix.SplitRawBinaryData.Length; i++) //Diag + Rev Diag + Row + Col
            {
                for (j = 0; j < sz.FileMatrix.SplitRawBinaryData.Length; j++)
                {
                    if (sz.processWorkingMatrix(true, true, true, true, i, j))
                    {
                        solnFoundMethod(sz.wrongCount);
                        //return;
                    }
                }
            }
        }

        static void solnFoundMethod(uint wCount)
        {
            //Console.WriteLine("YAYYYYYY!");
            Console.WriteLine(wCount);
            if (wCount == 0)
                Console.Read();
            if (lowestWrongCount > wCount)
                lowestWrongCount = wCount;
            //sz.wrongCount = 0;
            //sz.wrongGroups = 0;
            //sz.maxWrongGroupSize = 0;
            //sz.maxRightGroupSize = 0;
        }

        static void displayProcessStats(SHMZipper sz)
        {
            Console.WriteLine("MinRow = {0}, MaxRow = {1}, MinCol = {2}, MaxCol = {3}", sz.minRow, sz.maxRow, sz.minCol, sz.maxCol);
            Console.WriteLine("Wrong bit area size (in KB) = " + (((sz.maxRow - sz.minRow) * (sz.maxCol - sz.minCol)) / 8192));
            Console.WriteLine("\nWrong bits = " + sz.wrongCount + "\nMax distance between wrong bits = " + sz.wrongBitDistance + "\nWrong group max size = " + sz.wGrpSz + "\nNo. of Wrong groups = " + (sz.wGrpCnt + 1) + "\nAverage Wrong Group Size = " + sz.avgWGrpSz + "\n");
        }

        static uint processCount = 0;

        static void processDaBadBwoy(SHMZipper sz)
        {
            processCount++;
            sz.processWorkingMatrix(true, false, false, false, 0, 0);
            //Console.WriteLine("\n" + sz.wrongCount);
            displayProcessStats(sz);
            Console.WriteLine("No. of times file been processed = " + processCount + "\n------------------------\n");
            if (processCount == 24)
                Console.Beep(250, 2000);
            if (sz.wrongCount > 0)
            {
                SHMZipper sz2 = new SHMZipper(sz.workingMatrix2.getByteRepresentationFromMatrix());
                processDaBadBwoy(sz2);
            }
            //Console.WriteLine("No. of times file been processed = " + processCount + "\n---------------------------");
        }

        static void Main(string[] args)
        {
            //compute2DBinaryOffset(2285176);
            //FileStream fs = new FileStream("out3.txt", FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs);
            //sw.AutoFlush = true;
            //Console.SetOut(sw);
            //Console.SetError(sw);
            //byte[] h = { 128, 131, 3, 8 , 165, 45, 89, 255};
            //byte[] h = { 176, 66, 152, 24, 207, 41, 66, 0 };
            byte[] h = File.ReadAllBytes("Black Label.mp3");
            //byte[] h = File.ReadAllBytes("Stef.mp3");
            //byte[] h = File.ReadAllBytes("Stef.mp3");
            //byte[] h = { 255 };
            byte[] l = { 5, 9, 7, 6 };
            byte[] lol = { 0X6A, 0X43, 0XF6, 0X91, 0X52, 0XEA, 0X88, 0X09 };
            
            sz = new SHMZipper(h);
            sz.initializeWorkingMatrix(true, false, false, false, 0, 0);
            //sz.processWorkingMatrix(true, false, false, false, 0, 0);
            //Console.WriteLine(sz.workingMatrix.StringRepresentation);
            sz.initializeProbabilityMatrix();
            sz.setProbabilityMatrixDiagonalToZero();
            for (uint i = 0; i < sz.HorizontalNoOfOnesCount.Length; i++)
            {
                for (uint j = 0; j < sz.HorizontalNoOfOnesCount.Length; j++)
                {
                    if (sz.probabilityMatrix[i, j] < 10)
                        Console.Write("0");
                    Console.Write(sz.probabilityMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            //processDaBadBwoy(sz);
            //Console.WriteLine(sz.FileMatrix.StringRepresentation);
            //sz.processWorkingMatrix(true, false, false, false, 0, 0);
            //Console.WriteLine(sz.workingMatrix2.StringRepresentation);
            //displayProcessStats(sz);

            //SHMZipper sz2 = new SHMZipper(sz.workingMatrix2.getByteRepresentationFromMatrix());
            //Console.WriteLine(sz2.FileMatrix.StringRepresentation);
            //sz2.processWorkingMatrix(true, false, false, false, 0, 0);
            //Console.WriteLine(sz2.workingMatrix2.StringRepresentation);
            //displayProcessStats(sz2);

            //Console.WriteLine(sz2.FileMatrix.StringRepresentation);
            //foreach (uint uk in sz.privateHorizontalNoOfOnesToBeFilled)
                //Console.WriteLine(uk);
            //Console.WriteLine();
            //Console.WriteLine("MinRow = {0}, MaxRow = {1}, MinCol = {2}, MaxCol = {3}", sz.minRow, sz.maxRow, sz.minCol, sz.maxCol);
            //foreach (uint uk in sz.privateVerticalNoOfOnesToBeFilled)
                //Console.WriteLine(uk);
            //Console.WriteLine("\nWrong bits = " + sz.wrongCount + "\nMax distance between wrong bits = " + sz.wrongBitDistance + "\nWrong group max size = " + sz.wGrpSz + "\nNo. of Wrong groups = " + (sz.wGrpCnt + 1) + "\nAverage Wrong Group Size = " + sz.avgWGrpSz);

            //Console.WriteLine("\n" + Array. sz.FileMatrix. + "\n\n" + sz.workingMatrix.StringRepresentation);
            uint k = 0;
            //foreach (byte b in sz.FileMatrix.ByteRepresentation)
            //{
            //    Console.WriteLine(b + " " + sz.workingMatrix.getByteRepresentationFromMatrix()[k++]);
            //}

            //File.WriteAllBytes("lol.mp3", sz.workingMatrix.getByteRepresentationFromMatrix());
            //System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            //st.Start();
            //sz = new SHMZipper(h);
            //st.Stop();
            //Console.WriteLine(st.ElapsedTicks);
            //Console.Write(sz.FileMatrix.StringRepresentation);
            //Console.WriteLine();
            //foreach (uint k in sz.privateDiagNoOfOnesCountStatic)
            //    Console.WriteLine(k);
            //Console.WriteLine();
            //foreach (uint k in sz.privateRevDiagNoOfOnesCountStatic)
            //    Console.WriteLine(k);
            //lol(h);
            //byte[] h = { 181, 118 };
            //Console.WriteLine("The number of processors " + "on this computer is {0}.", Environment.ProcessorCount);
            //calculate(sz);
            //Console.WriteLine(lowestWrongCount);
            Console.Read();
            //Console.WriteLine(sz.workingMatrix.StringRepresentation);
            //int k = 0;
            //Console.WriteLine(BinaryStaticClass.ByteStringRepresentation(145));
            //BinaryNumber bn1 = new BinaryNumber(h);
            //bn1.ByteRepresentation = l;
            //BinaryMatrix bm = new BinaryMatrix(h);
            //Console.WriteLine(bm.StringRepresentation);
            ////bm.ByteRepresentation = l;
            //Console.WriteLine(bm.StringRepresentation);
            //bm.Clear();
            //Console.WriteLine();
            //Console.WriteLine(bm.StringRepresentation);
            //for (uint i = 0; i < bn1.NoOfBits; i++)
            //    Console.Write(bn1.GetBitAtIndex(i));
            //BinaryMatrix bm = new BinaryMatrix(File.ReadAllBytes("Black Label.mp3"));
            //BinaryMatrix bm = new BinaryMatrix(h);
            //SHMZipper sz = new SHMZipper(h);
            
            //foreach (BinaryNumber bn in bm.SplitRawBinaryData)
            //{
            //    Console.WriteLine(bn.StringRepresentation);
            //}
            //Console.WriteLine();
            //for (uint i = 0; i < bm.BinaryMatrixNoOfBitsInSide; i++)
            //{
            //    for (uint j = 0; j < bm.BinaryMatrixNoOfBitsInSide; j++)
            //    {
            //        Console.Write(bm.BitAt2DIndex(i, j));
            //    }
            //    Console.WriteLine();
            //}
            //FileStream fs = File.Open("lol1.mpp3", FileMode.Create);
            //foreach (BinaryNumber bn in bm.SplitRawBinaryData)
            //{
            //    fs.Write(bn.ByteRepresentation, 0, (int)bn.NoOfBytes);
            //}
            //BinaryNumber bn = new BinaryNumber("100110101011011010");
            //BinaryNumber bn = new BinaryNumber(h);
            //BinaryNumber bn = new BinaryNumber(1);
            //bn.StringRepresentation = "10111011";
            //bn.ByteRepresentation = h;
            //Console.WriteLine(bn.StringRepresentation);
            //Console.WriteLine(bn.ExtractBytes(2, 2).StringRepresentation);
            //for (uint i = 0; i < bn.NoOfBits; i++)
            //{
            //    Console.Write(bn.GetBitAtIndex(i));
            //}
            //Console.WriteLine();
            //bn.SetBitAtIndex(15, 1);
            //for (uint i = 0; i < bn.NoOfBits; i++)
            //{
            //    bn.SetBitAtIndex(i, 0);
            //}
            
            //Console.WriteLine(bn.StringRepresentation);
            //Console.WriteLine(bn.GetByteOffsetInArrayFromBitIndex(9));
            //Console.Write(BinaryStaticClass.BitAtBitIndex(bn.ByteRepresentation, 8));
            //for (uint i = 0; i < bn.NoOfBits; i++)
            //{
            //    Console.Write(bn.GetBitAtIndex(i));
            //    if ((i + 1) % 8 == 0)
            //        Console.WriteLine();
            //}
            //BinaryNumber b1 = new BinaryNumber(h);
            //for (uint i = 0, j = 0; i < h.Length * 8; i++)
            //{
            //    for (int k = 0; k < 8; k++)
            //        Console.Write(BinaryStaticClass.BitAtBitIndex(h, j++));
            //    Console.WriteLine();
            //}
            //BinaryMatrix bm = new BinaryMatrix(h);
            //BinaryNumber bn = new BinaryNumber("010110111");
            //BinaryNumber bn2 = new BinaryNumber("010110110");
            //bool lol = bn.IsEqual(bn2);
            //foreach (BinaryNumber bn in bm.SplitRawBinaryData)
            //    Console.WriteLine(bn.StringRepresentation);
            //Console.WriteLine();
            //for (uint i = 0; i < bm.BinaryMatrixNoOfBitsInSide; i++)
            //{
            //    for (uint j = 0; j < bm.BinaryMatrixNoOfBitsInSide; j++)
            //        Console.Write(bm.BitAt2DIndex(i, j));
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
            //BinaryNumber k = bm.GetMatrixDiagonal();
            //byte[] file = System.IO.File.ReadAllBytes("Stef.mp3");
            //byte[] l = new byte[999999999];
            //File.WriteAllBytes("l.mp3", l);
            //Array.Reverse(file);
            //b1.ByteRepresentation = file;
            //System.IO.File.WriteAllBytes("lol.mp3", b1.ByteRepresentation); 
            //BinaryMatrix bm = new BinaryMatrix(file);
            //System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            //st.Start();
            //SHMZipper scompStef = new SHMZipper("Black Label.mp3");
            //SHMZipper s = new SHMZipper(h);
            //for (uint i = 0; i < s.FileMatrix.BinaryMatrixNoOfBitsInSide; i++)
            //{
            //    for (uint j = 0; j < s.FileMatrix.BinaryMatrixNoOfBitsInSide; j++)
            //        Console.Write(s.GetFileBitAt2DIndex(i, j));
            //    Console.WriteLine();
            //}
            //while (!scompStef.AllThreadsComplete) { }
            //st.Stop();
            //Console.WriteLine(st.ElapsedTicks);
            //foreach (BinaryNumber bn in scompStef.FileMatrix.SplitRawBinaryData)
            //{
            //    Console.WriteLine(bn.StringRepresentation);
            //}
            //Console.WriteLine();
            //for (uint i = 0; i < scompStef.FileMatrix.BinaryMatrixNoOfBitsInSide; i++)
            //{
            //    for (uint j = 0; j < scompStef.FileMatrix.BinaryMatrixNoOfBitsInSide; j++)
            //        Console.Write(scompStef.GetFileBitAt2DIndex(i, j));
            //    Console.WriteLine();
            //}
            //SHMZipper scompBL = new SHMZipper("Black Label.mp3");
            //FileStream fs = System.IO.File.Open("lol2.mp3", System.IO.FileMode.Create);
            //foreach (BinaryNumber bn in scompBL.FileMatrix.SplitRawBinaryData)
            //{
            //    fs.Write(bn.ByteRepresentation, 0, (int)bn.NoOfBytes);
            //}
            //fs.Close();
            //bm.RawBinaryData = b1;
            //BinaryNumber b2 = b1.ExtractBytes(6, 8);
            //Console.Read();
        }
    }
}
