namespace NDRS.MySqlServer.Payload;

public interface IMessage
{
    byte[] ToByteArray();
}