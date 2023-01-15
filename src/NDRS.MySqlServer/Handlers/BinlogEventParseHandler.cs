using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NDRS.MySqlServer.BinlogEvents;
using NDRS.MySqlServer.Loggers;
using NDRS.MySqlServer.Payload;
using NDRS.MySqlServer.Utils;

namespace NDRS.MySqlServer.Handlers;

public class BinlogEventParseHandler:SimpleChannelInboundHandler<MessagePackage>
{
    private static readonly ILogger<BinlogEventParseHandler> _logger =
        NDRSLoggerFactory.CreateLogger<BinlogEventParseHandler>();
    protected override void ChannelRead0(IChannelHandlerContext ctx, MessagePackage messagePackage)
    {
        var content = (IByteBuffer)messagePackage.Content;
        content.SkipBytes(1);
        var header = new BinlogEventHeader();
        header.Timestamp = ByteUtil.ReadInt(content, 4);
        header.TypeCode = (byte)ByteUtil.ReadInt(content, 1);
        header.ServerId = ByteUtil.ReadInt(content, 4);
        header.EventLength = ByteUtil.ReadInt(content, 4);
        header.NextPosition = ByteUtil.ReadInt(content, 4);
        header.Flags = ByteUtil.ReadInt(content, 2);
        _logger.LogInformation(header.ToString());
        
    }
}