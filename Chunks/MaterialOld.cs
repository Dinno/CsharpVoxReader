using System;

namespace CsharpVoxReader.Chunks
{
    public class MaterialOld : Chunk
    {
        public const string ID = "MATT";

        internal override string Id
        {
            get { return ID; }
        }

        public enum MaterialTypes
        {
            Diffuse = 0,
            Metal = 1,
            Glass = 2,
            Emissive = 3
        };

        [Flags]
        public enum PropertyBits : uint
        {
            Plastic         = 1 << 0,
            Roughness       = 1 << 1,
            Specular        = 1 << 2,
            Ior             = 1 << 3,
            Attenuation     = 1 << 4,
            Power           = 1 << 5,
            Glow            = 1 << 6,
            IsTotalPower    = 1 << 7
        };

        internal override int Read(System.IO.BinaryReader br, IVoxLoader loader)
        {
            int readSize = base.Read(br, loader);

            int paletteId = br.ReadInt32();
            int type = br.ReadInt32();
            float weight = br.ReadSingle();
            uint property = br.ReadUInt32();
            float normalized = br.ReadSingle();
            readSize += sizeof(int) * 2 + sizeof(uint) + sizeof(float) * 2;

            loader.SetMaterialOld(paletteId, (MaterialTypes)type, weight, (PropertyBits)property, normalized);

            return readSize;
        }
    }
}