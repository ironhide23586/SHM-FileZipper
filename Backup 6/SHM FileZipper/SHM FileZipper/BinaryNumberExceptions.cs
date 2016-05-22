/*
 * Contains all the Exceptions Thrown by the BinaryNumber Class.
 * 
 * AUTHOR : SOUHAM BISWAS
 * 
 */
namespace SHM_FileZipper_Used_Classes
{
    /// <summary>
    /// Exception Thrown when BinaryNumber object is modified without initialization.
    /// </summary>
    public class BinaryObjectNotInitializedException : System.Exception
    {
        public BinaryObjectNotInitializedException() : base() { }
    }

    /// <summary>
    /// Exception thrown when a non-binary string is entered as binary data 
    /// into the BinaryNumber Object.
    /// </summary>
    public class InvalidBinaryStringException : System.Exception
    {
        public InvalidBinaryStringException() : base() { }
    }

    /// <summary>
    /// Exception thrown when anything other than a byte array is being stored in the
    /// BinaryNumber object.
    /// </summary>
    public class InvalidBinaryInputDataTypeException : System.Exception
    {
        public InvalidBinaryInputDataTypeException() : base() { }
    }

    /// <summary>
    /// Exception thrown when an illegal reference to a bit location in the binary Data
    /// contained in the BinaryNumber object is made. 
    /// </summary>
    public class IllegalBitReferenceIndexException : System.Exception
    {
        public IllegalBitReferenceIndexException() : base() { }
    }
}