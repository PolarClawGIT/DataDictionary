// This NameSpace/Class is used for building a Sample XML Documentation file.
// The Symbol information can be gotten from the PDB file but how to do this is not known.
namespace SampleLibrary
{

    /// <summary>
    /// Sample Interface.
    /// </summary>
    public interface ISampleArray
    {
        /// <summary>
        /// Sample Property
        /// </summary>
        Int32 SampleProperty { get; set; }
    }

    /// <summary>
    /// Sample Class
    /// </summary>
    public class SampleClass: ISampleArray
    {
        /// <summary>
        /// Sample Array/Field.
        /// </summary>
        public Int32[] SampleArray = new Int32[10];

        /// <inheritdoc/>
        public Int32 SampleProperty { get; set; }

        /// <summary>
        /// Sample Indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public String this[Int32 index] { get { return String.Empty; } }

        /// <summary>
        /// Sample Method
        /// </summary>
        /// <param name="samplePar01">Value 01</param>
        /// <param name="samplePar02">Value 02</param>
        public void SampleMethod(String samplePar01, String samplePar02)
        { }

        /// <summary>
        /// Sample Function
        /// </summary>
        /// <param name="samplePar01"></param>
        /// <returns></returns>
        public Int32 SampleFunction (String samplePar01) { throw new NotImplementedException(); }

        /// <summary>
        /// Sample Event
        /// </summary>
        public event EventHandler<String>? SampleEvent;

        /// <summary>
        /// Sample generic method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void SampleGenericMethod<T>(T data) { }

        /// <summary>
        /// Sample Class Delegate Function
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public delegate Int32 SampleClassDelegate(String value);

        /// <summary>
        /// Internal Class
        /// </summary>
        public class InternalClass { }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void HiddenMethod() { } // This does not show up in documentation file.
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// Sample Generic Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SampleGeneric<T> : SampleClass 
        where T: class, new()
    { }

    // Delegates look line Types in the Doc file. At this point I cannot tell the difference.

    /// <summary>
    /// Sample NameSpace Delegate method
    /// </summary>
    /// <param name="sampleParm"></param>
    public delegate void SampleDelegate(String sampleParm);

    /// <summary>
    /// Sample NameSpace Delegate Function
    /// </summary>
    /// <returns></returns>
    public delegate Int32 SampleFunctionDelegate();

    /// <summary>
    /// Sample NameSpace Delegate method with No Parameters
    /// </summary>
    public delegate void SampleDelegateNoPar();

}
