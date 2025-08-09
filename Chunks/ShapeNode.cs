using System;
using System.IO;
using System.Collections.Generic;

namespace CsharpVoxReader.Chunks
{
    public class ShapeNode : Chunk
    {
        public const string ID = "nSHP";

        internal override string Id
        {
            get { return ID; }
        }

        internal override int Read(BinaryReader br, IVoxLoader loader)
        {
            int readSize = base.Read(br, loader);

            int id = br.ReadInt32();
            Dictionary<string, byte[]> attributes = GenericsReader.ReadDict(br, ref readSize);

            int numModels = br.ReadInt32();
            readSize += sizeof(int) * 2;

            int[] modelIds = new int[numModels];
            Dictionary<string, byte[]>[] modelsAttributes = new Dictionary<string, byte[]>[numModels];

            for (int mnum=0; mnum < numModels; mnum++) {
              modelIds[mnum] = br.ReadInt32();
              modelsAttributes[mnum] = GenericsReader.ReadDict(br, ref readSize);
              readSize += sizeof(int);
            }

            loader.NewShapeNode(id, attributes, modelIds, modelsAttributes);
            return readSize;
        }
    }
}
