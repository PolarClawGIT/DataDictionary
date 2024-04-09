// This NameSpace/Class is used for building a Sample XML Documentation file.
// The Symbol information can be gotten from the PDB file but how to do this is not known.
namespace SampleLibrary
{

    /// <summary>
    /// Sample Interface.
    /// </summary>
    public interface ISampleArray
    {

    }

    /// <summary>
    /// Sample Class
    /// </summary>
    public class SampleClass
    {
        /// <summary>
        /// Sample Array/Field.
        /// </summary>
        public Int32[] SampleArray = new Int32[10];

        /// <summary>
        /// Sample Property
        /// </summary>
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
        /// <param name="sampleParm01">Value 01</param>
        /// <param name="samplePar02">Value 02</param>
        public void SampleMethod(String sampleParm01, String samplePar02)
        { }

        /// <summary>
        /// Sample Function
        /// </summary>
        /// <param name="sampleParm01"></param>
        /// <returns></returns>
        public Int32 SampleFunction (String sampleParm01) { throw new NotImplementedException(); }

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

        public void HiddenMethod() { } // This does not show up in documentation file.
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
    public delegate void SampleDelegateNoParm();

}
