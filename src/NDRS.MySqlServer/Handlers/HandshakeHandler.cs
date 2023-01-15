using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NDRS.MySqlServer.Loggers;
using NDRS.MySqlServer.Payload;
using NDRS.MySqlServer.Utils;

namespace NDRS.MySqlServer.Handlers;

public class HandshakeHandler:SimpleChannelInboundHandler<MessagePackage>
{
    private static readonly ILogger<HandshakeHandler> _logger =
        NDRSLoggerFactory.CreateLogger<HandshakeHandler>();
    protected override void ChannelRead0(IChannelHandlerContext ctx, MessagePackage mp)
    {
        _logger.LogInformation("handshake start");
        var msg = (IByteBuffer)mp.Content;
        var protocolVersion = msg.ReadByte();
        var serverVersion = ByteUtil.NullTerminatedString(msg);
        var threadId = ByteUtil.ReadInt4(msg);
        _logger.LogInformation($"protocolVersion = {protocolVersion},serverVersion = {serverVersion},threadId = {threadId}");
        var randomNumber1 = ByteUtil.NullTerminatedString(msg);
        msg.ReadBytes(2);
        var encode = msg.ReadByte();
        msg.ReadBytes(2);
        msg.ReadBytes(13);
        var randomNumber2 = ByteUtil.NullTerminatedString(msg);
        _logger.LogInformation($"randomNumber1 = {randomNumber1},encode = {encode},randomNumber2 = {randomNumber2}");
        _logger.LogInformation("handshake end");
        var authenticateData = new AuthenticateData(encode,randomNumber1+randomNumber2,"root","Bd88600818");
        ctx.Channel.WriteAndFlushAsync(new MessagePackage(1, authenticateData));
        ctx.Channel.Pipeline.Remove(this);
    }
}