using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NDRS.MySqlServer.Loggers;
using NDRS.MySqlServer.Payload;

namespace NDRS.MySqlServer.Codecs;

public class MessageEncoder:MessageToByteEncoder<MessagePackage>
{
    private static readonly ILogger<MessageEncoder> _logger =
        NDRSLoggerFactory.CreateLogger<MessageEncoder>();
    public override bool IsSharable => true;

    protected override void Encode(IChannelHandlerContext context, MessagePackage message, IByteBuffer output)
    {
       message.EncodeAsByteBuf(output);
       
       _logger.LogDebug($"write to client {context.Channel.Id.AsShortText()} : \n{ByteBufferUtil.PrettyHexDump(output)}");
    }
}