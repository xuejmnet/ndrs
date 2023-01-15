using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using NDRS.MySqlServer.Codecs;

namespace NDRS.MySqlServer.Handlers;

public class BinlogChannelHandler:ChannelInitializer<ISocketChannel>
{
    private readonly MessageEncoder _messageEncoder;
    private readonly IChannelHandler _connectorManagerHandler;

    public BinlogChannelHandler()
    {
        _messageEncoder = new MessageEncoder();
        _connectorManagerHandler = new ConnectorManagerHandler();
    }
    protected override void InitChannel(ISocketChannel channel)
    {
        var channelPipeline = channel.Pipeline;
        channelPipeline.AddLast(new MessageDecoder());
        channelPipeline.AddLast(_messageEncoder);
        // channelPipeline.AddLast(_connectorManagerHandler );
        channelPipeline.AddLast(new HandshakeHandler());
        channelPipeline.AddLast(new AuthenticateResultHandler());
        channelPipeline.AddLast(new BinlogEventParseHandler());
    }
}