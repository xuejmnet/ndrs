using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NDRS.MySqlServer.Loggers;
using NDRS.MySqlServer.Packets;

namespace NDRS.MySqlServer.Codecs;

/// <summary>
/// 消息包编码器
/// </summary>
public sealed class MessagePacketEncoder:MessageToByteEncoder<IMysqlPacket>
{
    private readonly ILogger<MessagePacketEncoder> _logger= NDRSLoggerFactory.CreateLogger<MessagePacketEncoder>();
    private readonly IPacketCodec _packetCodec;

    public MessagePacketEncoder(IPacketCodec packetCodec)
    {
        _packetCodec = packetCodec;
    }
    public override bool IsSharable => true;
    protected override void Encode(IChannelHandlerContext context, IMysqlPacket message, IByteBuffer output)
    {
        _packetCodec.Encode(context,message,output);
        if (true)
        {
            _logger.LogDebug($"write to client {context.Channel.Id.AsShortText()} : \n{ByteBufferUtil.PrettyHexDump(output)}");
        }
    }
}