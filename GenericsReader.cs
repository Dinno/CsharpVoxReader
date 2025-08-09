using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace CsharpVoxReader
{
  public class GenericsReader {

    public static byte[] ReadByteArray(BinaryReader br, ref int readsize) {
      int numChars = br.ReadInt32();
      readsize += sizeof(int) + numChars;

      return br.ReadBytes(numChars);
    }

    public static Dictionary<string, byte[]> ReadDict(BinaryReader br, ref int readsize) {
      Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();

      int numElements = br.ReadInt32();
      readsize += sizeof(int);

      for (int i=0; i < numElements; i++) {
        string key = Encoding.UTF8.GetString(ReadByteArray(br, ref readsize));
        byte[] value = ReadByteArray(br, ref readsize);

        result[key] = value;
      }

      return result;
    }

    public static int[] ReadRotation(BinaryReader br, ref int readsize) {
      byte rot = br.ReadByte();
      readsize += 1;

      int r0V = ((rot & 8) == 0)?1:-1;
      int r1V = ((rot & 16) == 0)?1:-1;
      int r2V = ((rot & 32) == 0)?1:-1;

      int r0I = rot & 3;
      int r1I = (rot & 12) >> 2;
      /*
      Truth table for the third index
        r0| 0 | 1 | 2 |
      r1--+---+---+---+
       0  | X | 2 | 1 |
      ----+---+---+---+
       1  | 2 | X | 0 |
      ----+---+---+---+
       2  | 1 | 0 | X |
      ----+---+---+---+

      Derived function
      f(r0, r1) = 3 - r0 - r1
      */
      int r2I = 3 - r0I - r1I;

      int[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

      result[r0I] = r0V;
      result[r1I + 3] = r1V;
      result[r2I + 6] = r2V;

      return result;
    }

  }
}
