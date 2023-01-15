using System.Net;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;
using NDRS.MySqlServer;
using NDRS.MySqlServer.Codecs;
using NDRS.MySqlServer.Handlers;
using NDRS.MySqlServer.Loggers;

namespace NDRS.ConsoleStarter;

public class DefaultServerHost:IServiceHost
{
    
    private static readonly ILogger<DefaultServerHost> _logger =
        NDRSLoggerFactory.CreateLogger<DefaultServerHost>();

    private readonly BinlogChannelHandler _binlogChannelHandler;

    // 主工作线程组，设置为1个线程
    private MultithreadEventLoopGroup group;

    private Bootstrap bootstrap;
    public DefaultServerHost()
    {
        _binlogChannelHandler = new BinlogChannelHandler();
    }
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("----------开始启动----------");


        group =new MultithreadEventLoopGroup();
        bootstrap = new Bootstrap();
        try
        {
            bootstrap.Group(group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Option(ChannelOption.SoBacklog, 100) // 看最下面解释
                .Option(ChannelOption.SoKeepalive, true) //保持连接
                .Option(ChannelOption.ConnectTimeout, TimeSpan.FromSeconds(3)) //连接超时
                .Option(ChannelOption.RcvbufAllocator, new AdaptiveRecvByteBufAllocator(1024, 1024, 65536))
                .Handler(_binlogChannelHandler);

                // bootstrap绑定到指定端口的行为 就是服务端启动服务，同样的Serverbootstrap可以bind到多个端口
                await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse("115.239.188.150"), 7400));
                _logger.LogInformation($"----------l链接完成----------");
            }
            catch (Exception ex)
            {
                _logger.LogError($"----------启动异常:{ex}----------");
            }
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("----------开始停止----------");
            await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            _logger.LogInformation("----------已停止----------");
        }
        catch (Exception e)
        {
            _logger.LogInformation($"----------停止异常:{e}----------");
        }
    }
}