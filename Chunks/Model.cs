using System;
using System.IO;

namespace CsharpVoxReader.Chunks
{
    public class Model : Chunk
    {
        public const string ID = "XYZI";

        private byte[,,] _indexes;

        internal override string Id
        {
            get { return ID; }
        }

        public byte[,,] Indexes
        {
            get { return _indexes; }
        }

        internal override int Read(BinaryReader br, IVoxLoader loader)
        {
            int readSize = base.Read(br, loader);

            int numVoxels = br.ReadInt32();
            readSize += sizeof(int);

            for (int i = 0; i < numVoxels; i++)
            {
                byte x = br.ReadByte();
                byte z = br.ReadByte();
                byte y = br.ReadByte();
                byte index = br.ReadByte();
                _indexes[x, y, z] = index;
                readSize += 4;
            }
            return readSize;
        }

        public void Init(int sizeX, int sizeY, int sizeZ)
        {
            _indexes = new byte[sizeX, sizeY, sizeZ];
        }
    }
}
