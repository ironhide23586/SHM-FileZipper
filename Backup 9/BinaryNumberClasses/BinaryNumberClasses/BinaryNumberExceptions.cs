/*
 * Contains all the Exceptions Thrown by the BinaryNumber Class.
 * 
 * AUTHOR : SOUHAM BISWAS
 * 
 */

namespace BinaryNumberClasses
{
    /// <summary>
    /// Exception Thrown when Binary object is modified without initialization.
    /// </summary>
    public class BinaryObjectNotInitializedException : System.Exception
    {
        public BinaryObjectNotInitializedException() : base() { }
    }

    /// <summary>
    /// Exception Thrown when start and stop indices for extraction of Byte stream from a given
    /// BinaryNumber object are invalid.
    /// </summary>
    public class IllegalExtractionParameters : System.Exception
    {
        public IllegalExtractionParameters() : base() { }
    }

    /// <summary>
    /// Exception thrown when a non-binary string is entered as binary data 
    /// into the Binary Object.
    /// </summary>
    public class InvalidBinaryStringException : System.Exception
    {
        public InvalidBinaryStringException() : base() { }
    }

    /// <summary>
    /// Exception thrown when illegal data is inserted into the
    /// Binary object.
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