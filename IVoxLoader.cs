using System;
using System.Collections.Generic;

namespace CsharpVoxReader
{
    public interface IVoxLoader
    {
        void LoadModel(int sizeX, int sizeY, int sizeZ, byte[,,] data);
        void LoadPalette(uint[] palette);
        void SetModelCount(int count);
        void SetMaterialOld(int paletteId, Chunks.MaterialOld.MaterialTypes type, float weight, Chunks.MaterialOld.PropertyBits property, float normalized);
        // VOX Extensions
        void NewTransformNode(int id, int childNodeId, int layerId, string name, Dictionary<string, byte[]>[] framesAttributes);
        void NewGroupNode(int id, Dictionary<string, byte[]> attributes, int[] childrenIds);
        void NewShapeNode(int id, Dictionary<string, byte[]> attributes, int[] modelIds, Dictionary<string, byte[]>[] modelsAttributes);
        void NewMaterial(int id, Dictionary<string, byte[]> attributes);
        void NewLayer(int id, string name, Dictionary<string, byte[]> attributes);
    }
}
