using DotNetty.Buffers;
using NDRS.MySqlServer.Utils;

namespace NDRS.MySqlServer.Payload;

public class MessagePackage
{
    /// <summary>
    /// 16MB
    /// </summary>
    public const int MAX_PACKET_LENGTH = 0xFFFFFF;
    /// <summary>
    /// 
    /// </summary>
    public const int PAYLOAD_LENGTH = 3;
    /// <summary>
    /// 序号长度
    /// </summary>
    public const int SEQUENCE_LENGTH = 1;
    /// <summary>
    /// 最小二进制长度
    /// </summary>
    public const int READABLE_BYTES_MIN_LENGTH = PAYLOAD_LENGTH+SEQUENCE_LENGTH;
    public MessageHeader Header { get; }
    public object Content { get; }

    public MessagePackage(MessageHeader header,object content)
    {
        Header = header;
        Content = content;
    }

    public MessagePackage(int sequenceId,object content)
    {
        Header = new MessageHeader(0,sequenceId);
        Content = content;
    }

    public void EncodeAsByteBuf(IByteBuffer output)
    {
        var content = (IMessage)Content;
        var dataBytes = content.ToByteArray();
        //先占4位 前3位长度 第4位sequenceId
        output.WriteInt(0);
        // ByteUtil.WriteInt3(output,dataBytes.Length);
        // output.WriteByte((byte)Header.SequenceId);
        output.WriteBytes(dataBytes);
        UpdateMessageHeader(output, Header.SequenceId);
    }
    private void UpdateMessageHeader(IByteBuffer byteBuffer, int sequenceId)
    {
        byteBuffer.SetMediumLE(0, byteBuffer.ReadableBytes - PAYLOAD_LENGTH - SEQUENCE_LENGTH);
        byteBuffer.SetByte(3, sequenceId);
    }
}