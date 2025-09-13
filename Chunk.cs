using System;
using System.IO;

namespace CsharpVoxReader
{
    public abstract class Chunk
    {
        private int _size;
        private int _childrenSize;

        internal abstract string Id { get; }

        internal int Size
        {
            get { return _size; }
        }

        internal int ChildrenSize
        {
            get { return _childrenSize; }
        }

        internal static string ReadChunkId(BinaryReader br)
        {
            if (br == null) throw new ArgumentNullException(nameof(br));

            char[] id = br.ReadChars(4);
            return new string(id);
        }

        internal virtual int Read(BinaryReader br, IVoxLoader loader)
        {
            if (br == null) throw new ArgumentNullException(nameof(br));
            if (loader == null) throw new ArgumentNullException(nameof(loader));

            _size = br.ReadInt32();
            _childrenSize = br.ReadInt32();

            return sizeof(int) * 2;
        }

        internal static Chunk CreateChunk(string id)
        {
            switch (id)
            {
                case Chunks.Main.ID : return new Chunks.Main();
                case Chunks.Size.ID : return new Chunks.Size();
                case Chunks.Model.ID : return new Chunks.Model();
                case Chunks.Palette.ID : return new Chunks.Palette();
                case Chunks.Pack.ID: return new Chunks.Pack();
                case Chunks.MaterialOld.ID: return new Chunks.MaterialOld();
                case Chunks.TransformNode.ID: return new Chunks.TransformNode();
                case Chunks.GroupNode.ID: return new Chunks.GroupNode();
                case Chunks.ShapeNode.ID: return new Chunks.ShapeNode();
                case Chunks.Material.ID: return new Chunks.Material();
                case Chunks.Layer.ID: return new Chunks.Layer();
                default: return new Chunks.Unknown(id);
            }
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
