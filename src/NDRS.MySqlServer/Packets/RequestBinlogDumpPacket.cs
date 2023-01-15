using System.Text;
using DotNetty.Buffers;
using NDRS.MySqlServer.Common;
using NDRS.MySqlServer.Payload;
using NDRS.MySqlServer.Utils;

namespace NDRS.MySqlServer.Packets;

public class RequestBinlogDumpPacket:IMessage
{
    public long ServerId { get; }
    public string BinlogFileName { get; }
    public long BinlogPosition { get; }

    public RequestBinlogDumpPacket(long serverId,string binlogFileName,long binlogPosition)
    {
        ServerId = serverId;
        BinlogFileName = binlogFileName;
        BinlogPosition = binlogPosition;
    }
    public byte[] ToByteArray()
    {
        var positionBytes = ByteUtil.WriteLong(BinlogPosition,4);
        var flagBytes = ByteUtil.WriteInt(0,2);
        var serverIdBytes = ByteUtil.WriteLong(ServerId,4);
        var binlogFileNameBytes = Encoding.UTF8.GetBytes(BinlogFileName);
        
        var byteBuffer = Unpooled.Buffer();
        byteBuffer.WriteByte(CommandType.COM_BINLOG_DUMP);
        byteBuffer.WriteBytes(positionBytes);
        byteBuffer.WriteBytes(flagBytes);
        byteBuffer.WriteBytes(serverIdBytes);
        byteBuffer.WriteBytes(binlogFileNameBytes);
        return byteBuffer.Array;
    }
}