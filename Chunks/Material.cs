using System;
using System.IO;
using System.Collections.Generic;

namespace CsharpVoxReader.Chunks
{
    public class Material : Chunk
    {
        public const string ID = "MATL";

        internal override string Id
        {
            get { return ID; }
        }

        internal override int Read(BinaryReader br, IVoxLoader loader)
        {
            int readSize = base.Read(br, loader);

            int id = br.ReadInt32();
            Dictionary<string, byte[]> attributes = GenericsReader.ReadDict(br, ref readSize);
            readSize += sizeof(int);

            loader.NewMaterial(id, attributes);
            return readSize;
        }
    }
}
