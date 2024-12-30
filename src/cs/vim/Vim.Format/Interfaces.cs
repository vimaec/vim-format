using System.IO;
using Vim.Format.ObjectModel;
using Vim.G3d;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format
{
    public interface IVimModel
    {
        /// <summary>
        /// The schema version contained in the header of the VIM file.
        /// </summary>
        SerializableVersion SchemaVersion { get; }

        /// <summary>
        /// The header of the VIM file.
        /// </summary>
        SerializableHeader Header { get; }

        /// <summary>
        /// The serializable version of the VIM model.
        /// </summary>
        SerializableDocument SerializableDocument { get; }

        /// <summary>
        /// The VIM file path if it was loaded from a file on disk.
        /// </summary>
        string FilePath { get; } // not null if the VIM was loaded from a file on disk

        /// <summary>
        /// The string buffer for the Entities
        /// </summary>
        string[] StringBuffer { get; }

        /// <summary>
        /// The entities contained in the VIM.
        /// </summary>
        EntityTableSet Entities { get; }

        /// <summary>
        /// The collection of all instances in the VIM (i.e. the renderable geometry)
        /// </summary>
        IVimInstance[] Instances { get; }

        /// <summary>
        /// The world-space bounding box surrounding all the instances in the model.
        /// </summary>
        AABox BoundingBox { get; }

        /// <summary>
        /// Merges this VIM model with the other VIM model and returns a new one.
        /// </summary>
        IVimModel Merge(IVimModel other);

        /// <summary>
        /// Writes the VIM model to the given stream
        /// </summary>
        void Write(Stream stream);

        /// <summary>
        /// Writes the VIM model to the given file path.
        /// Deletes any existing file prior to writing.
        /// Creates the parent directory if it does not already exist.
        /// </summary>
        void Write(string filePath);
    }

    public interface IVimInstance
    {
        /// <summary>
        /// The index of the instance.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// The instance flags associated to this instance.
        /// </summary>
        InstanceFlags InstanceFlags { get; }

        /// <summary>
        /// The world transform of the instance.
        /// </summary>
        Matrix4x4 WorldTransform { get; }

        /// <summary>
        /// The mesh associated to an instance can be null.
        /// </summary>
        IVimMesh Mesh { get; }
        
        /// <summary>
        /// The Node entity related to this instance.
        /// There is a 1:1 relationship between instances and nodes.
        /// </summary>
        Node Node { get; }

        /// <summary>
        /// The world-space axis aligned bounding box of the instance.
        /// </summary>
        AABox BoundingBox { get; }
    }

    public interface IVimMesh
    {
        int Index { get; }
        IVimSubmesh[] Submeshes { get; }
        void Validate();

        // this method would be used in one file only. do we need it?
        // public IVimMesh Transform(Matrix4x4 mat) // research if this is used somewhere else
    }

    public interface IVimSubmesh
    {
        int Index { get; }
        IVimRenderMaterial Material { get; }
        Vector3[] Vertices { get; }
        int[] Indices { get; }
        void Validate();
    }

    public interface IVimRenderMaterial
    {
        int Index { get; }
        Vector4 Color { get; } // rgba
        float Glossiness { get; }
        float Smoothness { get; }
    }
}
