using DotNetty.Buffers;

namespace NDRS.MySqlServer.Packets;

public interface IPacketPayload:IDisposable
{
    IByteBuffer GetByteBuffer();
}