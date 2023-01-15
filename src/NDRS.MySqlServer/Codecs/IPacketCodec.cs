using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using NDRS.MySqlServer.Packets;

namespace NDRS.MySqlServer.Codecs;

public interface IPacketCodec
{
    /// <summary>
    /// 验证头部
    /// </summary>
    /// <param name="readableBytes"></param>
    /// <returns></returns>
    bool IsValidHeader(int readableBytes);
    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <param name="output"></param>
    void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output);

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="context"></param>
    /// <param name="message"></param>
    /// <param name="output"></param>
    void Encode(IChannelHandlerContext context, IMysqlPacket message, IByteBuffer output);
    
}