using System;
using System.IO;
using System.Collections.Generic;

namespace CsharpVoxReader.Chunks
{
    public class GroupNode : Chunk
    {
        public const string ID = "nGRP";

        internal override string Id
        {
            get { return ID; }
        }

        internal override int Read(BinaryReader br, IVoxLoader loader)
        {
            int readSize = base.Read(br, loader);

            int id = br.ReadInt32();
            Dictionary<string, byte[]> attributes = GenericsReader.ReadDict(br, ref readSize);

            int numChildrenNodes = br.ReadInt32();
            int[] childrenIds = new int[numChildrenNodes];
            readSize += sizeof(int) * (numChildrenNodes + 2);

            for (int cnum=0; cnum < numChildrenNodes; cnum++) {
              childrenIds[cnum] = br.ReadInt32();
            }

            loader.NewGroupNode(id, attributes, childrenIds);
            return readSize;
        }
    }
}
