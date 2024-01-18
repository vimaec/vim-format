// AUTO-GENERATED FILE, DO NOT MODIFY.
// ReSharper disable All
using Vim.BFastNS;

namespace Vim.G3dNext.Attributes
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
            return Indices.SafeEquals(other.Indices) && 
 			Positions.SafeEquals(other.Positions) && 
 			InstanceTransforms.SafeEquals(other.InstanceTransforms) && 
 			InstanceParents.SafeEquals(other.InstanceParents) && 
 			InstanceFlags.SafeEquals(other.InstanceFlags) && 
 			InstanceMeshes.SafeEquals(other.InstanceMeshes) && 
 			MeshSubmeshOffsets.SafeEquals(other.MeshSubmeshOffsets) && 
 			SubmeshIndexOffsets.SafeEquals(other.SubmeshIndexOffsets) && 
 			SubmeshMaterials.SafeEquals(other.SubmeshMaterials) && 
 			MaterialColors.SafeEquals(other.MaterialColors) && 
 			MaterialGlossiness.SafeEquals(other.MaterialGlossiness) && 
 			MaterialSmoothness.SafeEquals(other.MaterialSmoothness) && 
 			ShapeVertices.SafeEquals(other.ShapeVertices) && 
 			ShapeVertexOffsets.SafeEquals(other.ShapeVertexOffsets) && 
 			ShapeColors.SafeEquals(other.ShapeColors) && 
 			ShapeWidths.SafeEquals(other.ShapeWidths);
        }

        public G3dVim Merge(G3dVim other)
        {
            return new G3dVim(
                Indices.MergeIndices(other.Indices, Positions?.Length ?? 0), 
 				Positions.MergeData(other.Positions), 
 				InstanceTransforms.MergeData(other.InstanceTransforms), 
 				InstanceParents.MergeIndices(other.InstanceParents, InstanceTransforms?.Length ?? 0), 
 				InstanceFlags.MergeData(other.InstanceFlags), 
 				InstanceMeshes.MergeIndices(other.InstanceMeshes, MeshSubmeshOffsets?.Length ?? 0), 
 				MeshSubmeshOffsets.MergeIndices(other.MeshSubmeshOffsets, SubmeshIndexOffsets?.Length ?? 0), 
 				SubmeshIndexOffsets.MergeIndices(other.SubmeshIndexOffsets, Indices?.Length ?? 0), 
 				SubmeshMaterials.MergeIndices(other.SubmeshMaterials, MaterialColors?.Length ?? 0), 
 				MaterialColors.MergeData(other.MaterialColors), 
 				MaterialGlossiness.MergeData(other.MaterialGlossiness), 
 				MaterialSmoothness.MergeData(other.MaterialSmoothness), 
 				ShapeVertices.MergeData(other.ShapeVertices), 
 				ShapeVertexOffsets.MergeIndices(other.ShapeVertexOffsets, ShapeVertices?.Length ?? 0), 
 				ShapeColors.MergeData(other.ShapeColors), 
 				ShapeWidths.MergeData(other.ShapeWidths)
            );
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
            Indices?.ValidateIndex(Positions, "Indices");
 			InstanceParents?.ValidateIndex(InstanceTransforms, "InstanceParents");
 			InstanceMeshes?.ValidateIndex(MeshSubmeshOffsets, "InstanceMeshes");
 			MeshSubmeshOffsets?.ValidateIndex(SubmeshIndexOffsets, "MeshSubmeshOffsets");
 			SubmeshIndexOffsets?.ValidateIndex(Indices, "SubmeshIndexOffsets");
 			SubmeshMaterials?.ValidateIndex(MaterialColors, "SubmeshMaterials");
 			ShapeVertexOffsets?.ValidateIndex(ShapeVertices, "ShapeVertexOffsets");
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
 			MeshSubmeshOffset = bfast.GetArray<System.Int32>("g3d:mesh:submeshOffset:0:int32:1");
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
 			bfast.SetArray<System.Int32>("g3d:mesh:submeshOffset:0:int32:1", MeshSubmeshOffset);
 			bfast.SetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1", SubmeshIndexOffsets);
 			bfast.SetArray<System.Int32>("g3d:submesh:vertexoffset:0:int32:1", SubmeshVertexOffsets);
 			bfast.SetArray<System.Int32>("g3d:submesh:material:0:int32:1", SubmeshMaterials);
 			bfast.SetArray<Vim.Math3d.Vector3>("g3d:vertex:position:0:float32:3", Positions);
 			bfast.SetArray<System.Int32>("g3d:corner:index:0:int32:1", Indices);

            return bfast;
        }

        public bool Equals(G3dChunk other )
        {
            return MeshOpaqueSubmeshCounts.SafeEquals(other.MeshOpaqueSubmeshCounts) && 
 			MeshSubmeshOffset.SafeEquals(other.MeshSubmeshOffset) && 
 			SubmeshIndexOffsets.SafeEquals(other.SubmeshIndexOffsets) && 
 			SubmeshVertexOffsets.SafeEquals(other.SubmeshVertexOffsets) && 
 			SubmeshMaterials.SafeEquals(other.SubmeshMaterials) && 
 			Positions.SafeEquals(other.Positions) && 
 			Indices.SafeEquals(other.Indices);
        }

        public G3dChunk Merge(G3dChunk other)
        {
            return new G3dChunk(
                MeshOpaqueSubmeshCounts.MergeData(other.MeshOpaqueSubmeshCounts), 
 				MeshSubmeshOffset.MergeIndices(other.MeshSubmeshOffset, Indices?.Length ?? 0), 
 				SubmeshIndexOffsets.MergeIndices(other.SubmeshIndexOffsets, Indices?.Length ?? 0), 
 				SubmeshVertexOffsets.MergeIndices(other.SubmeshVertexOffsets, Indices?.Length ?? 0), 
 				SubmeshMaterials.MergeData(other.SubmeshMaterials), 
 				Positions.MergeData(other.Positions), 
 				Indices.MergeIndices(other.Indices, Positions?.Length ?? 0)
            );
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
            MeshSubmeshOffset?.ValidateIndex(Indices, "MeshSubmeshOffset");
 			SubmeshIndexOffsets?.ValidateIndex(Indices, "SubmeshIndexOffsets");
 			SubmeshVertexOffsets?.ValidateIndex(Indices, "SubmeshVertexOffsets");
 			Indices?.ValidateIndex(Positions, "Indices");
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
            return MaterialColors.SafeEquals(other.MaterialColors) && 
 			MaterialGlossiness.SafeEquals(other.MaterialGlossiness) && 
 			MaterialSmoothness.SafeEquals(other.MaterialSmoothness);
        }

        public G3dMaterials Merge(G3dMaterials other)
        {
            return new G3dMaterials(
                MaterialColors.MergeData(other.MaterialColors), 
 				MaterialGlossiness.MergeData(other.MaterialGlossiness), 
 				MaterialSmoothness.MergeData(other.MaterialSmoothness)
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
            return ChunkCount.SafeEquals(other.ChunkCount) && 
 			InstanceMeshes.SafeEquals(other.InstanceMeshes) && 
 			InstanceTransformData.SafeEquals(other.InstanceTransformData) && 
 			InstanceNodes.SafeEquals(other.InstanceNodes) && 
 			InstanceGroups.SafeEquals(other.InstanceGroups) && 
 			InstanceTags.SafeEquals(other.InstanceTags) && 
 			InstanceFlags.SafeEquals(other.InstanceFlags) && 
 			InstanceMins.SafeEquals(other.InstanceMins) && 
 			InstanceMaxs.SafeEquals(other.InstanceMaxs) && 
 			MeshChunks.SafeEquals(other.MeshChunks) && 
 			MeshChunkIndices.SafeEquals(other.MeshChunkIndices) && 
 			MeshVertexCounts.SafeEquals(other.MeshVertexCounts) && 
 			MeshIndexCounts.SafeEquals(other.MeshIndexCounts) && 
 			MeshOpaqueVertexCounts.SafeEquals(other.MeshOpaqueVertexCounts) && 
 			MeshOpaqueIndexCounts.SafeEquals(other.MeshOpaqueIndexCounts);
        }

        public G3dScene Merge(G3dScene other)
        {
            return new G3dScene(
                ChunkCount.MergeData(other.ChunkCount), 
 				InstanceMeshes.MergeData(other.InstanceMeshes), 
 				InstanceTransformData.MergeData(other.InstanceTransformData), 
 				InstanceNodes.MergeData(other.InstanceNodes), 
 				InstanceGroups.MergeData(other.InstanceGroups), 
 				InstanceTags.MergeData(other.InstanceTags), 
 				InstanceFlags.MergeData(other.InstanceFlags), 
 				InstanceMins.MergeData(other.InstanceMins), 
 				InstanceMaxs.MergeData(other.InstanceMaxs), 
 				MeshChunks.MergeData(other.MeshChunks), 
 				MeshChunkIndices.MergeData(other.MeshChunkIndices), 
 				MeshVertexCounts.MergeData(other.MeshVertexCounts), 
 				MeshIndexCounts.MergeData(other.MeshIndexCounts), 
 				MeshOpaqueVertexCounts.MergeData(other.MeshOpaqueVertexCounts), 
 				MeshOpaqueIndexCounts.MergeData(other.MeshOpaqueIndexCounts)
            );
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
            
        }
    }

}
