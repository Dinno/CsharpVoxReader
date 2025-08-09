using System;
using System.IO;

namespace CsharpVoxReader.Chunks
{
    public class Size : Chunk
    {
        public const string ID = "SIZE";

        internal override string Id
        {
            get { return ID; }
        }

        internal override int Read(BinaryReader br, IVoxLoader loader)
        {
            int readSize = base.Read(br, loader);

            int x = br.ReadInt32();
            int z = br.ReadInt32();
            int y = br.ReadInt32();
            readSize += sizeof(int) * 3;

            string id = ReadChunkId(br);
            readSize += 4;
            if(id != Model.ID) throw new InvalidDataException($"Can't read VOX file : XYZI chunk expected (was {id})");

            Model model = CreateChunk(id) as Model;
            model.Init(x, y, z);
            readSize += model.Read(br, loader);

            loader.LoadModel(x, y, z, model.Indexes);
            return readSize;
        }
    }
}
