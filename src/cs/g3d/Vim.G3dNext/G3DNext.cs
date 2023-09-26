using System.IO;
using Vim.BFast;
using System.Linq;
using Vim.BFastNext;

namespace Vim.G3dNext
{
    /// <summary>
    /// A G3d is composed of a header and a collection of attributes containing descriptors and their data.
    /// </summary>
    public class G3DNext<TAttributeCollection> where TAttributeCollection : IAttributeCollection,  new()
    {
        /// <summary>
        /// The meta header of the G3d. Corresponds to the "meta" segment.
        /// </summary>
        public readonly MetaHeader MetaHeader;

        /// <summary>
        /// The attributes of the G3d.
        /// </summary>
        public readonly TAttributeCollection AttributeCollection;

        /// <summary>
        /// Constructor.
        /// </summary>
        public G3DNext(MetaHeader metaHeader, TAttributeCollection attributeCollection)
        {
            MetaHeader = metaHeader;
            AttributeCollection = attributeCollection;
        }

        public G3DNext(BFastNext.BFastNext bfast)
        {
            AttributeCollection = new TAttributeCollection();
            AttributeCollection.ReadAttributes(bfast);
            MetaHeader = MetaHeader.FromBytes(bfast.GetArray<byte>("meta"));
        }

        /// <summary>
        /// Constructor. Uses the default header.
        /// </summary>
        public G3DNext(TAttributeCollection attributeCollection) 
            : this(MetaHeader.Default, attributeCollection)
        { }

        /// <summary>
        /// Constructor. Uses the default header and instantiates an attribute collection.
        /// </summary>
        public G3DNext()
            : this(MetaHeader.Default, new TAttributeCollection())
        { }

        public static G3DNext<T> Empty<T>() where T : IAttributeCollection, new()
            => new G3DNext<T>();

        /// <summary>
        /// Reads the stream using the attribute collection's readers and outputs a G3d upon success.
        /// </summary>
        
        public static G3DNext<TAttributeCollection> ReadBFast(BFastNext.BFastNext bfast)
        {
            var attributeCollection = new TAttributeCollection();
            attributeCollection.ReadAttributes(bfast);
            var header = MetaHeader.FromBytes(bfast.GetArray<byte>("meta"));

            // Instantiate the object and return.
            return new G3DNext<TAttributeCollection>(header, attributeCollection);
        }

        public static G3DNext<TAttributeCollection> ReadBFast(string path)
        {
            return path.ReadBFast(ReadBFast);
        }

        public long GetSize() {
            return AttributeCollection.GetSize() + Constants.MetaHeaderSegmentNumBytes;
        }

        public BFastNext.BFastNext ToBFast()
        {
            var attributes = AttributeCollection.Attributes.Values.OrderBy(n => n.Name).ToArray(); // Order the attributes by name for consistency
            var bfast = new BFastNext.BFastNext();
            bfast.AddArray(Constants.MetaHeaderSegmentName, MetaHeader.ToBytes());
            foreach (var a in attributes)
            {
                a.AddTo(bfast);
            }
            return bfast;
        }

        /// <summary>
        /// Returns a byte array representing the G3d.
        /// </summary>
        public byte[] ToBytes()
        {
            using (var memoryStream = new MemoryStream())
            {
                var b = ToBFast();
                b.Write(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Validates the G3d.
        /// </summary>
        public void Validate()
            => AttributeCollection.Validate();

    }
}
