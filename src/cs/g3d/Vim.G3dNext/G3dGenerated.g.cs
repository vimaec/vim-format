// AUTO-GENERATED FILE, DO NOT MODIFY.
// ReSharper disable All
using Vim.BFastNS;

namespace Vim.G3dNext
{
    // Please provide an explicit implementation in another partial class file.
    public partial class G3dVim : ISetup
    {
        public System.Int32[] Indices;
 		public Vim.Math3d.Vector3[] Positions;
 		public Vim.Math3d.Matrix4x4[] InstanceTransforms;
 		public System.Int32[] InstanceParents;
 		public System.UInt16[] InstanceFlags;
 		public System.Int32[] InstanceMeshes;
 		public System.Int32[] MeshSubmeshOffsets;
 		public System.Int32[] SubmeshIndexOffsets;
 		public System.Int32[] SubmeshMaterials;
 		public Vim.Math3d.Vector4[] MaterialColors;
 		public System.Single[] MaterialGlossiness;
 		public System.Single[] MaterialSmoothness;
 		public Vim.Math3d.Vector3[] ShapeVertices;
 		public System.Int32[] ShapeVertexOffsets;
 		public Vim.Math3d.Vector4[] ShapeColors;
 		public System.Single[] ShapeWidths;

        public G3dVim(
            System.Int32[] indices, 
 			Vim.Math3d.Vector3[] positions, 
 			Vim.Math3d.Matrix4x4[] instanceTransforms, 
 			System.Int32[] instanceParents, 
 			System.UInt16[] instanceFlags, 
 			System.Int32[] instanceMeshes, 
 			System.Int32[] meshSubmeshOffsets, 
 			System.Int32[] submeshIndexOffsets, 
 			System.Int32[] submeshMaterials, 
 			Vim.Math3d.Vector4[] materialColors, 
 			System.Single[] materialGlossiness, 
 			System.Single[] materialSmoothness, 
 			Vim.Math3d.Vector3[] shapeVertices, 
 			System.Int32[] shapeVertexOffsets, 
 			Vim.Math3d.Vector4[] shapeColors, 
 			System.Single[] shapeWidths
        )
        {
            Indices = indices;
 			Positions = positions;
 			InstanceTransforms = instanceTransforms;
 			InstanceParents = instanceParents;
 			InstanceFlags = instanceFlags;
 			InstanceMeshes = instanceMeshes;
 			MeshSubmeshOffsets = meshSubmeshOffsets;
 			SubmeshIndexOffsets = submeshIndexOffsets;
 			SubmeshMaterials = submeshMaterials;
 			MaterialColors = materialColors;
 			MaterialGlossiness = materialGlossiness;
 			MaterialSmoothness = materialSmoothness;
 			ShapeVertices = shapeVertices;
 			ShapeVertexOffsets = shapeVertexOffsets;
 			ShapeColors = shapeColors;
 			ShapeWidths = shapeWidths;

            (this as ISetup).Setup();
        }

        public G3dVim(BFast bfast)
        {
            Indices = bfast.GetArray<System.Int32>("g3d:corner:index:0:int32:1");
 			Positions = bfast.GetArray<Vim.Math3d.Vector3>("g3d:vertex:position:0:float32:3");
 			InstanceTransforms = bfast.GetArray<Vim.Math3d.Matrix4x4>("g3d:instance:transform:0:float32:16");
 			InstanceParents = bfast.GetArray<System.Int32>("g3d:instance:parent:0:int32:1");
 			InstanceFlags = bfast.GetArray<System.UInt16>("g3d:instance:flags:0:uint16:1");
 			InstanceMeshes = bfast.GetArray<System.Int32>("g3d:instance:mesh:0:int32:1");
 			MeshSubmeshOffsets = bfast.GetArray<System.Int32>("g3d:mesh:submeshoffset:0:int32:1");
 			SubmeshIndexOffsets = bfast.GetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1");
 			SubmeshMaterials = bfast.GetArray<System.Int32>("g3d:submesh:material:0:int32:1");
 			MaterialColors = bfast.GetArray<Vim.Math3d.Vector4>("g3d:material:color:0:float32:4");
 			MaterialGlossiness = bfast.GetArray<System.Single>("g3d:material:glossiness:0:float32:1");
 			MaterialSmoothness = bfast.GetArray<System.Single>("g3d:material:smoothness:0:float32:1");
 			ShapeVertices = bfast.GetArray<Vim.Math3d.Vector3>("g3d:shapevertex:position:0:float32:3");
 			ShapeVertexOffsets = bfast.GetArray<System.Int32>("g3d:shape:vertexoffset:0:int32:1");
 			ShapeColors = bfast.GetArray<Vim.Math3d.Vector4>("g3d:shape:color:0:float32:4");
 			ShapeWidths = bfast.GetArray<System.Single>("g3d:shape:width:0:float32:1");

            (this as ISetup).Setup();
        }

        public BFast ToBFast()
        {
            var bfast = new BFast();

            bfast.SetArray<System.Int32>("g3d:corner:index:0:int32:1", Indices);
 			bfast.SetArray<Vim.Math3d.Vector3>("g3d:vertex:position:0:float32:3", Positions);
 			bfast.SetArray<Vim.Math3d.Matrix4x4>("g3d:instance:transform:0:float32:16", InstanceTransforms);
 			bfast.SetArray<System.Int32>("g3d:instance:parent:0:int32:1", InstanceParents);
 			bfast.SetArray<System.UInt16>("g3d:instance:flags:0:uint16:1", InstanceFlags);
 			bfast.SetArray<System.Int32>("g3d:instance:mesh:0:int32:1", InstanceMeshes);
 			bfast.SetArray<System.Int32>("g3d:mesh:submeshoffset:0:int32:1", MeshSubmeshOffsets);
 			bfast.SetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1", SubmeshIndexOffsets);
 			bfast.SetArray<System.Int32>("g3d:submesh:material:0:int32:1", SubmeshMaterials);
 			bfast.SetArray<Vim.Math3d.Vector4>("g3d:material:color:0:float32:4", MaterialColors);
 			bfast.SetArray<System.Single>("g3d:material:glossiness:0:float32:1", MaterialGlossiness);
 			bfast.SetArray<System.Single>("g3d:material:smoothness:0:float32:1", MaterialSmoothness);
 			bfast.SetArray<Vim.Math3d.Vector3>("g3d:shapevertex:position:0:float32:3", ShapeVertices);
 			bfast.SetArray<System.Int32>("g3d:shape:vertexoffset:0:int32:1", ShapeVertexOffsets);
 			bfast.SetArray<Vim.Math3d.Vector4>("g3d:shape:color:0:float32:4", ShapeColors);
 			bfast.SetArray<System.Single>("g3d:shape:width:0:float32:1", ShapeWidths);

            return bfast;
        }

        public bool Equals(G3dVim other )
        {
            return BufferMethods.SafeEquals(Indices, other.Indices) && 
 			BufferMethods.SafeEquals(Positions, other.Positions) && 
 			BufferMethods.SafeEquals(InstanceTransforms, other.InstanceTransforms) && 
 			BufferMethods.SafeEquals(InstanceParents, other.InstanceParents) && 
 			BufferMethods.SafeEquals(InstanceFlags, other.InstanceFlags) && 
 			BufferMethods.SafeEquals(InstanceMeshes, other.InstanceMeshes) && 
 			BufferMethods.SafeEquals(MeshSubmeshOffsets, other.MeshSubmeshOffsets) && 
 			BufferMethods.SafeEquals(SubmeshIndexOffsets, other.SubmeshIndexOffsets) && 
 			BufferMethods.SafeEquals(SubmeshMaterials, other.SubmeshMaterials) && 
 			BufferMethods.SafeEquals(MaterialColors, other.MaterialColors) && 
 			BufferMethods.SafeEquals(MaterialGlossiness, other.MaterialGlossiness) && 
 			BufferMethods.SafeEquals(MaterialSmoothness, other.MaterialSmoothness) && 
 			BufferMethods.SafeEquals(ShapeVertices, other.ShapeVertices) && 
 			BufferMethods.SafeEquals(ShapeVertexOffsets, other.ShapeVertexOffsets) && 
 			BufferMethods.SafeEquals(ShapeColors, other.ShapeColors) && 
 			BufferMethods.SafeEquals(ShapeWidths, other.ShapeWidths);
        }

        public G3dVim Merge(G3dVim other)
        {
            return new G3dVim(
                BufferMethods.MergeIndex(Indices, other.Indices, Positions?.Length ?? 0), 
 				BufferMethods.MergeData(Positions, other.Positions), 
 				BufferMethods.MergeData(InstanceTransforms, other.InstanceTransforms), 
 				BufferMethods.MergeIndex(InstanceParents, other.InstanceParents, InstanceTransforms?.Length ?? 0), 
 				BufferMethods.MergeData(InstanceFlags, other.InstanceFlags), 
 				BufferMethods.MergeIndex(InstanceMeshes, other.InstanceMeshes, MeshSubmeshOffsets?.Length ?? 0), 
 				BufferMethods.MergeIndex(MeshSubmeshOffsets, other.MeshSubmeshOffsets, SubmeshIndexOffsets?.Length ?? 0), 
 				BufferMethods.MergeIndex(SubmeshIndexOffsets, other.SubmeshIndexOffsets, Indices?.Length ?? 0), 
 				BufferMethods.MergeIndex(SubmeshMaterials, other.SubmeshMaterials, MaterialColors?.Length ?? 0), 
 				BufferMethods.MergeData(MaterialColors, other.MaterialColors), 
 				BufferMethods.MergeData(MaterialGlossiness, other.MaterialGlossiness), 
 				BufferMethods.MergeData(MaterialSmoothness, other.MaterialSmoothness), 
 				BufferMethods.MergeData(ShapeVertices, other.ShapeVertices), 
 				BufferMethods.MergeIndex(ShapeVertexOffsets, other.ShapeVertexOffsets, ShapeVertices?.Length ?? 0), 
 				BufferMethods.MergeData(ShapeColors, other.ShapeColors), 
 				BufferMethods.MergeData(ShapeWidths, other.ShapeWidths)
            );
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
            BufferMethods.ValidateIndex(Indices, Positions, "Indices");
 			BufferMethods.ValidateIndex(InstanceParents, InstanceTransforms, "InstanceParents");
 			BufferMethods.ValidateIndex(InstanceMeshes, MeshSubmeshOffsets, "InstanceMeshes");
 			BufferMethods.ValidateIndex(MeshSubmeshOffsets, SubmeshIndexOffsets, "MeshSubmeshOffsets");
 			BufferMethods.ValidateIndex(SubmeshIndexOffsets, Indices, "SubmeshIndexOffsets");
 			BufferMethods.ValidateIndex(SubmeshMaterials, MaterialColors, "SubmeshMaterials");
 			BufferMethods.ValidateIndex(ShapeVertexOffsets, ShapeVertices, "ShapeVertexOffsets");
        }
    }

    // Please provide an explicit implementation in another partial class file.
    public partial class G3dChunk : ISetup
    {
        public System.Int32[] MeshOpaqueSubmeshCounts;
 		public System.Int32[] MeshSubmeshOffset;
 		public System.Int32[] SubmeshIndexOffsets;
 		public System.Int32[] SubmeshVertexOffsets;
 		public System.Int32[] SubmeshMaterials;
 		public Vim.Math3d.Vector3[] Positions;
 		public System.Int32[] Indices;

        public G3dChunk(
            System.Int32[] meshOpaqueSubmeshCounts, 
 			System.Int32[] meshSubmeshOffset, 
 			System.Int32[] submeshIndexOffsets, 
 			System.Int32[] submeshVertexOffsets, 
 			System.Int32[] submeshMaterials, 
 			Vim.Math3d.Vector3[] positions, 
 			System.Int32[] indices
        )
        {
            MeshOpaqueSubmeshCounts = meshOpaqueSubmeshCounts;
 			MeshSubmeshOffset = meshSubmeshOffset;
 			SubmeshIndexOffsets = submeshIndexOffsets;
 			SubmeshVertexOffsets = submeshVertexOffsets;
 			SubmeshMaterials = submeshMaterials;
 			Positions = positions;
 			Indices = indices;

            (this as ISetup).Setup();
        }

        public G3dChunk(BFast bfast)
        {
            MeshOpaqueSubmeshCounts = bfast.GetArray<System.Int32>("g3d:mesh:opaquesubmeshcount:0:int32:1");
 			MeshSubmeshOffset = bfast.GetArray<System.Int32>("g3d:mesh:submeshoffset:0:int32:1");
 			SubmeshIndexOffsets = bfast.GetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1");
 			SubmeshVertexOffsets = bfast.GetArray<System.Int32>("g3d:submesh:vertexoffset:0:int32:1");
 			SubmeshMaterials = bfast.GetArray<System.Int32>("g3d:submesh:material:0:int32:1");
 			Positions = bfast.GetArray<Vim.Math3d.Vector3>("g3d:vertex:position:0:float32:3");
 			Indices = bfast.GetArray<System.Int32>("g3d:corner:index:0:int32:1");

            (this as ISetup).Setup();
        }

        public BFast ToBFast()
        {
            var bfast = new BFast();

            bfast.SetArray<System.Int32>("g3d:mesh:opaquesubmeshcount:0:int32:1", MeshOpaqueSubmeshCounts);
 			bfast.SetArray<System.Int32>("g3d:mesh:submeshoffset:0:int32:1", MeshSubmeshOffset);
 			bfast.SetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1", SubmeshIndexOffsets);
 			bfast.SetArray<System.Int32>("g3d:submesh:vertexoffset:0:int32:1", SubmeshVertexOffsets);
 			bfast.SetArray<System.Int32>("g3d:submesh:material:0:int32:1", SubmeshMaterials);
 			bfast.SetArray<Vim.Math3d.Vector3>("g3d:vertex:position:0:float32:3", Positions);
 			bfast.SetArray<System.Int32>("g3d:corner:index:0:int32:1", Indices);

            return bfast;
        }

        public bool Equals(G3dChunk other )
        {
            return BufferMethods.SafeEquals(MeshOpaqueSubmeshCounts, other.MeshOpaqueSubmeshCounts) && 
 			BufferMethods.SafeEquals(MeshSubmeshOffset, other.MeshSubmeshOffset) && 
 			BufferMethods.SafeEquals(SubmeshIndexOffsets, other.SubmeshIndexOffsets) && 
 			BufferMethods.SafeEquals(SubmeshVertexOffsets, other.SubmeshVertexOffsets) && 
 			BufferMethods.SafeEquals(SubmeshMaterials, other.SubmeshMaterials) && 
 			BufferMethods.SafeEquals(Positions, other.Positions) && 
 			BufferMethods.SafeEquals(Indices, other.Indices);
        }

        public G3dChunk Merge(G3dChunk other)
        {
            return new G3dChunk(
                BufferMethods.MergeData(MeshOpaqueSubmeshCounts, other.MeshOpaqueSubmeshCounts), 
 				BufferMethods.MergeIndex(MeshSubmeshOffset, other.MeshSubmeshOffset, Indices?.Length ?? 0), 
 				BufferMethods.MergeIndex(SubmeshIndexOffsets, other.SubmeshIndexOffsets, Indices?.Length ?? 0), 
 				BufferMethods.MergeIndex(SubmeshVertexOffsets, other.SubmeshVertexOffsets, Indices?.Length ?? 0), 
 				BufferMethods.MergeData(SubmeshMaterials, other.SubmeshMaterials), 
 				BufferMethods.MergeData(Positions, other.Positions), 
 				BufferMethods.MergeIndex(Indices, other.Indices, Positions?.Length ?? 0)
            );
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
            BufferMethods.ValidateIndex(MeshSubmeshOffset, Indices, "MeshSubmeshOffset");
 			BufferMethods.ValidateIndex(SubmeshIndexOffsets, Indices, "SubmeshIndexOffsets");
 			BufferMethods.ValidateIndex(SubmeshVertexOffsets, Indices, "SubmeshVertexOffsets");
 			BufferMethods.ValidateIndex(Indices, Positions, "Indices");
        }
    }

    // Please provide an explicit implementation in another partial class file.
    public partial class G3dMaterials : ISetup
    {
        public Vim.Math3d.Vector4[] MaterialColors;
 		public System.Single[] MaterialGlossiness;
 		public System.Single[] MaterialSmoothness;

        public G3dMaterials(
            Vim.Math3d.Vector4[] materialColors, 
 			System.Single[] materialGlossiness, 
 			System.Single[] materialSmoothness
        )
        {
            MaterialColors = materialColors;
 			MaterialGlossiness = materialGlossiness;
 			MaterialSmoothness = materialSmoothness;

            (this as ISetup).Setup();
        }

        public G3dMaterials(BFast bfast)
        {
            MaterialColors = bfast.GetArray<Vim.Math3d.Vector4>("g3d:material:color:0:float32:4");
 			MaterialGlossiness = bfast.GetArray<System.Single>("g3d:material:glossiness:0:float32:1");
 			MaterialSmoothness = bfast.GetArray<System.Single>("g3d:material:smoothness:0:float32:1");

            (this as ISetup).Setup();
        }

        public BFast ToBFast()
        {
            var bfast = new BFast();

            bfast.SetArray<Vim.Math3d.Vector4>("g3d:material:color:0:float32:4", MaterialColors);
 			bfast.SetArray<System.Single>("g3d:material:glossiness:0:float32:1", MaterialGlossiness);
 			bfast.SetArray<System.Single>("g3d:material:smoothness:0:float32:1", MaterialSmoothness);

            return bfast;
        }

        public bool Equals(G3dMaterials other )
        {
            return BufferMethods.SafeEquals(MaterialColors, other.MaterialColors) && 
 			BufferMethods.SafeEquals(MaterialGlossiness, other.MaterialGlossiness) && 
 			BufferMethods.SafeEquals(MaterialSmoothness, other.MaterialSmoothness);
        }

        public G3dMaterials Merge(G3dMaterials other)
        {
            return new G3dMaterials(
                BufferMethods.MergeData(MaterialColors, other.MaterialColors), 
 				BufferMethods.MergeData(MaterialGlossiness, other.MaterialGlossiness), 
 				BufferMethods.MergeData(MaterialSmoothness, other.MaterialSmoothness)
            );
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
            
        }
    }

    // Please provide an explicit implementation in another partial class file.
    public partial class G3dScene : ISetup
    {
        public System.Int32[] ChunkCount;
 		public System.Int32[] InstanceMeshes;
 		public Vim.Math3d.Matrix4x4[] InstanceTransformData;
 		public System.Int32[] InstanceNodes;
 		public System.Int32[] InstanceGroups;
 		public System.Int64[] InstanceTags;
 		public System.UInt16[] InstanceFlags;
 		public Vim.Math3d.Vector3[] InstanceMins;
 		public Vim.Math3d.Vector3[] InstanceMaxs;
 		public System.Int32[] MeshChunks;
 		public System.Int32[] MeshChunkIndices;
 		public System.Int32[] MeshVertexCounts;
 		public System.Int32[] MeshIndexCounts;
 		public System.Int32[] MeshOpaqueVertexCounts;
 		public System.Int32[] MeshOpaqueIndexCounts;

        public G3dScene(
            System.Int32[] chunkCount, 
 			System.Int32[] instanceMeshes, 
 			Vim.Math3d.Matrix4x4[] instanceTransformData, 
 			System.Int32[] instanceNodes, 
 			System.Int32[] instanceGroups, 
 			System.Int64[] instanceTags, 
 			System.UInt16[] instanceFlags, 
 			Vim.Math3d.Vector3[] instanceMins, 
 			Vim.Math3d.Vector3[] instanceMaxs, 
 			System.Int32[] meshChunks, 
 			System.Int32[] meshChunkIndices, 
 			System.Int32[] meshVertexCounts, 
 			System.Int32[] meshIndexCounts, 
 			System.Int32[] meshOpaqueVertexCounts, 
 			System.Int32[] meshOpaqueIndexCounts
        )
        {
            ChunkCount = chunkCount;
 			InstanceMeshes = instanceMeshes;
 			InstanceTransformData = instanceTransformData;
 			InstanceNodes = instanceNodes;
 			InstanceGroups = instanceGroups;
 			InstanceTags = instanceTags;
 			InstanceFlags = instanceFlags;
 			InstanceMins = instanceMins;
 			InstanceMaxs = instanceMaxs;
 			MeshChunks = meshChunks;
 			MeshChunkIndices = meshChunkIndices;
 			MeshVertexCounts = meshVertexCounts;
 			MeshIndexCounts = meshIndexCounts;
 			MeshOpaqueVertexCounts = meshOpaqueVertexCounts;
 			MeshOpaqueIndexCounts = meshOpaqueIndexCounts;

            (this as ISetup).Setup();
        }

        public G3dScene(BFast bfast)
        {
            ChunkCount = bfast.GetArray<System.Int32>("g3d:chunk:count:0:int32:1");
 			InstanceMeshes = bfast.GetArray<System.Int32>("g3d:instance:mesh:0:int32:1");
 			InstanceTransformData = bfast.GetArray<Vim.Math3d.Matrix4x4>("g3d:instance:transform:0:float32:16");
 			InstanceNodes = bfast.GetArray<System.Int32>("g3d:instance:node:0:int32:1");
 			InstanceGroups = bfast.GetArray<System.Int32>("g3d:instance:group:0:int32:1");
 			InstanceTags = bfast.GetArray<System.Int64>("g3d:instance:tag:0:int64:1");
 			InstanceFlags = bfast.GetArray<System.UInt16>("g3d:instance:flags:0:uint16:1");
 			InstanceMins = bfast.GetArray<Vim.Math3d.Vector3>("g3d:instance:min:0:float32:3");
 			InstanceMaxs = bfast.GetArray<Vim.Math3d.Vector3>("g3d:instance:max:0:float32:3");
 			MeshChunks = bfast.GetArray<System.Int32>("g3d:mesh:chunk:0:int32:1");
 			MeshChunkIndices = bfast.GetArray<System.Int32>("g3d:mesh:chunkindex:0:int32:1");
 			MeshVertexCounts = bfast.GetArray<System.Int32>("g3d:mesh:vertexcount:0:int32:1");
 			MeshIndexCounts = bfast.GetArray<System.Int32>("g3d:mesh:indexcount:0:int32:1");
 			MeshOpaqueVertexCounts = bfast.GetArray<System.Int32>("g3d:mesh:opaquevertexcount:0:int32:1");
 			MeshOpaqueIndexCounts = bfast.GetArray<System.Int32>("g3d:mesh:opaqueindexcount:0:int32:1");

            (this as ISetup).Setup();
        }

        public BFast ToBFast()
        {
            var bfast = new BFast();

            bfast.SetArray<System.Int32>("g3d:chunk:count:0:int32:1", ChunkCount);
 			bfast.SetArray<System.Int32>("g3d:instance:mesh:0:int32:1", InstanceMeshes);
 			bfast.SetArray<Vim.Math3d.Matrix4x4>("g3d:instance:transform:0:float32:16", InstanceTransformData);
 			bfast.SetArray<System.Int32>("g3d:instance:node:0:int32:1", InstanceNodes);
 			bfast.SetArray<System.Int32>("g3d:instance:group:0:int32:1", InstanceGroups);
 			bfast.SetArray<System.Int64>("g3d:instance:tag:0:int64:1", InstanceTags);
 			bfast.SetArray<System.UInt16>("g3d:instance:flags:0:uint16:1", InstanceFlags);
 			bfast.SetArray<Vim.Math3d.Vector3>("g3d:instance:min:0:float32:3", InstanceMins);
 			bfast.SetArray<Vim.Math3d.Vector3>("g3d:instance:max:0:float32:3", InstanceMaxs);
 			bfast.SetArray<System.Int32>("g3d:mesh:chunk:0:int32:1", MeshChunks);
 			bfast.SetArray<System.Int32>("g3d:mesh:chunkindex:0:int32:1", MeshChunkIndices);
 			bfast.SetArray<System.Int32>("g3d:mesh:vertexcount:0:int32:1", MeshVertexCounts);
 			bfast.SetArray<System.Int32>("g3d:mesh:indexcount:0:int32:1", MeshIndexCounts);
 			bfast.SetArray<System.Int32>("g3d:mesh:opaquevertexcount:0:int32:1", MeshOpaqueVertexCounts);
 			bfast.SetArray<System.Int32>("g3d:mesh:opaqueindexcount:0:int32:1", MeshOpaqueIndexCounts);

            return bfast;
        }

        public bool Equals(G3dScene other )
        {
            return BufferMethods.SafeEquals(ChunkCount, other.ChunkCount) && 
 			BufferMethods.SafeEquals(InstanceMeshes, other.InstanceMeshes) && 
 			BufferMethods.SafeEquals(InstanceTransformData, other.InstanceTransformData) && 
 			BufferMethods.SafeEquals(InstanceNodes, other.InstanceNodes) && 
 			BufferMethods.SafeEquals(InstanceGroups, other.InstanceGroups) && 
 			BufferMethods.SafeEquals(InstanceTags, other.InstanceTags) && 
 			BufferMethods.SafeEquals(InstanceFlags, other.InstanceFlags) && 
 			BufferMethods.SafeEquals(InstanceMins, other.InstanceMins) && 
 			BufferMethods.SafeEquals(InstanceMaxs, other.InstanceMaxs) && 
 			BufferMethods.SafeEquals(MeshChunks, other.MeshChunks) && 
 			BufferMethods.SafeEquals(MeshChunkIndices, other.MeshChunkIndices) && 
 			BufferMethods.SafeEquals(MeshVertexCounts, other.MeshVertexCounts) && 
 			BufferMethods.SafeEquals(MeshIndexCounts, other.MeshIndexCounts) && 
 			BufferMethods.SafeEquals(MeshOpaqueVertexCounts, other.MeshOpaqueVertexCounts) && 
 			BufferMethods.SafeEquals(MeshOpaqueIndexCounts, other.MeshOpaqueIndexCounts);
        }

        public G3dScene Merge(G3dScene other)
        {
            return new G3dScene(
                BufferMethods.MergeData(ChunkCount, other.ChunkCount), 
 				BufferMethods.MergeData(InstanceMeshes, other.InstanceMeshes), 
 				BufferMethods.MergeData(InstanceTransformData, other.InstanceTransformData), 
 				BufferMethods.MergeData(InstanceNodes, other.InstanceNodes), 
 				BufferMethods.MergeData(InstanceGroups, other.InstanceGroups), 
 				BufferMethods.MergeData(InstanceTags, other.InstanceTags), 
 				BufferMethods.MergeData(InstanceFlags, other.InstanceFlags), 
 				BufferMethods.MergeData(InstanceMins, other.InstanceMins), 
 				BufferMethods.MergeData(InstanceMaxs, other.InstanceMaxs), 
 				BufferMethods.MergeData(MeshChunks, other.MeshChunks), 
 				BufferMethods.MergeData(MeshChunkIndices, other.MeshChunkIndices), 
 				BufferMethods.MergeData(MeshVertexCounts, other.MeshVertexCounts), 
 				BufferMethods.MergeData(MeshIndexCounts, other.MeshIndexCounts), 
 				BufferMethods.MergeData(MeshOpaqueVertexCounts, other.MeshOpaqueVertexCounts), 
 				BufferMethods.MergeData(MeshOpaqueIndexCounts, other.MeshOpaqueIndexCounts)
            );
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
            
        }
    }

}
