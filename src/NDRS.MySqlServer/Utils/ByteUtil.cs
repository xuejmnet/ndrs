using System.Text;
using DotNetty.Buffers;

namespace NDRS.MySqlServer.Utils;

public class ByteUtil
{
    private static readonly Char END_CHAR = '\0';
    public static string NullTerminatedString(IByteBuffer msg)
    {
        byte[] result = new byte[msg.BytesBefore((byte) 0)];
        msg.ReadBytes(result);
        msg.SkipBytes(1);
        return Encoding.UTF8.GetString(result);
        // msg.MarkReaderIndex();
        // int length = 0;
        // while (END_CHAR != msg.ReadByte())
        // {
        //     length++;
        // }
        // msg.ResetReaderIndex();
        // var bytes = new byte[length];
        // msg.ReadBytes(bytes);
        // return Encoding.UTF8.GetString(bytes);
    }

    public static int ReadInt(IByteBuffer src, int bits) {
        int result = 0;
        for (int i = 0; i < bits; ++i) {
            result |= (src.ReadUnsignedShort() << (8 * i));
        }
        return result;
    }
    public static int ReadInt3(IByteBuffer src) {
        return src.ReadMediumLE() & 0xffffff;
    }
    public static int ReadInt4(IByteBuffer src) {
        return src.ReadIntLE();
    }
    public static byte[] WriteInt(int value, int length) {
        byte[] result = new byte[length];
        for (int i = 0; i < length; i++) {
            result[i] = (byte) ((value >> (8 * i)) & 0x000000FF);
        }
        return result;
    }
    
    public static void WriteInt3(IByteBuffer buffer,int value) {
        buffer.WriteMediumLE(value);
    }
    public static void WriteInt4(IByteBuffer buffer,int value) {
        buffer.WriteIntLE(value);
    }
    public void WriteStringNul(IByteBuffer buffer,string value) {
        buffer.WriteBytes(Encoding.UTF8.GetBytes(value));
        buffer.WriteByte(0);
    }
    

    public static byte[] WriteLong(long value, int length) {
        byte[] result = new byte[length];
        for (int i = 0; i < length; i++) {
            result[i] = (byte) ((value >> (8 * i)) & 0x000000FF);
        }
        return result;
    }
    
    public static byte[] PasswordCompatibleWithMySQL411(String password, String salt)
    {
        byte[] passwordHash = Sha1Helper.Hash2Bytes(password);
        var saltBytes = Encoding.UTF8.GetBytes(salt);
        var doubleSha1Password = Sha1Helper.Hash2Bytes(passwordHash);
        var concatBytes = Concat(saltBytes,doubleSha1Password);
        var shar1ConcatBytes = Sha1Helper.Hash2Bytes(concatBytes);
        return Xor(passwordHash, shar1ConcatBytes);
    }
    
    private static byte[] Concat(byte[] a, byte[] b) {
        byte[] r = new byte[a.Length + b.Length];
        Array.Copy(a,0,r,0,a.Length);
        Array.Copy(b,0,r,a.Length,b.Length);
        return r;
    }

    private static byte[] Xor(byte[] input,byte[] secret)
    {
        var result = new byte[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            result[i] = (byte)(input[i] ^ secret[i]);
        }

        return result;
    }
    
}