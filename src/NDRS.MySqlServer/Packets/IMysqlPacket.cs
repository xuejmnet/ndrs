namespace NDRS.MySqlServer.Packets;

public interface IMysqlPacket
{
    int SequenceId { get; }
    void WriteTo(MySqlPacketPayload payload);
}