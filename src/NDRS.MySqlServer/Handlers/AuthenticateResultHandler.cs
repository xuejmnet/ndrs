using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NDRS.MySqlServer.Common;
using NDRS.MySqlServer.Loggers;
using NDRS.MySqlServer.Packets;
using NDRS.MySqlServer.Payload;

namespace NDRS.MySqlServer.Handlers;

public class AuthenticateResultHandler :SimpleChannelInboundHandler<MessagePackage>
{
    private static readonly ILogger<AuthenticateResultHandler> _logger =
        NDRSLoggerFactory.CreateLogger<AuthenticateResultHandler>();
    protected override void ChannelRead0(IChannelHandlerContext ctx, MessagePackage messagePackage)
    {
        var msg = (IByteBuffer)messagePackage.Content;
        var mark = msg.ReadByte();
        if (mark != 0)
        {
            _logger.LogError($"Authenticate fail:\n{ByteBufferUtil.PrettyHexDump(msg)}");
        }
        else
        {
            var requestBinlogDumpPacket = new RequestBinlogDumpPacket(Constants.SERVERID,"binlog.000006",8676);
            ctx.Channel.WriteAndFlushAsync(new MessagePackage(0,requestBinlogDumpPacket));
            _logger.LogInformation("Authenticate success");
        }

        ctx.Channel.Pipeline.Remove(this);
    }
}