using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NDRS.MySqlServer.Loggers;
using NDRS.MySqlServer.Payload;
using NDRS.MySqlServer.Utils;

namespace NDRS.MySqlServer.Codecs;

public class MessageDecoder:ByteToMessageDecoder
{
    private static readonly ILogger<MessageDecoder> _logger =
        NDRSLoggerFactory.CreateLogger<MessageDecoder>();
    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        if (input.ReadableBytes < 4)
        {
            return;
        }
        
        _logger.LogDebug($"read from client {context.Channel.Id.AsShortText()} : \n{ByteBufferUtil.PrettyHexDump(input)}");
        input.MarkReaderIndex();
        var packageLength = ByteUtil.ReadInt3(input);
        var sequenceId = input.ReadByte();
        _logger.LogInformation($"packageLength = {packageLength},sequenceId = {sequenceId}");
        var header = new MessageHeader(packageLength,sequenceId);
        if (input.ReadableBytes < packageLength)
        {
            input.ResetReaderIndex();
            return;
        }

        var byteBuffer = Unpooled.Buffer(packageLength);
        byteBuffer.WriteBytes(input.ReadBytes(packageLength));
        var messagePackage = new MessagePackage(header,byteBuffer);
        output.Add(messagePackage);
    }
}