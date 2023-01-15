using System.Text;
using DotNetty.Buffers;
using NDRS.MySqlServer.Payload;
using NDRS.MySqlServer.Utils;

namespace NDRS.MySqlServer;

public class AuthenticateData:IMessage
{
    public byte Encode { get; }
    public string RandomNumber { get; }
    public string UserName { get; }
    public string Password { get; }

    public AuthenticateData(byte encode,string randomNumber,string userName,string password)
    {
        Encode = encode;
        RandomNumber = randomNumber;
        UserName = userName;
        Password = password;
    }

    public byte[] ToByteArray()
    {
        int clientPower = PowerType.CLIENT_LONG_FLAG | PowerType.CLIENT_PROTOCOL_41
                                                     | PowerType.CLIENT_SECURE_CONNECTION;
        
        byte[] clientPowerBytes = ByteUtil.WriteInt(clientPower, 4);
        int maxLen = 0;
        byte[] maxLenBytes = ByteUtil.WriteInt(maxLen, 4);
        byte[] encodeBytes = ByteUtil.WriteInt(Encode, 1);
        byte[] zeroBytes = ByteUtil.WriteInt(0, 23);

        byte[] userNameBytes = Encoding.UTF8.GetBytes(UserName+"");
        byte[] passwordBytes = "".Equals(Password) ? new byte[0]
            : ByteUtil.PasswordCompatibleWithMySQL411(Password, RandomNumber);

        var byteBuf = Unpooled.Buffer();
        // byteBuf.WriteBytes(clientPowerBytes);
        // byteBuf.WriteBytes(maxLenBytes);
        // byteBuf.WriteBytes(encodeBytes);
        // byteBuf.WriteBytes(zeroBytes);
        // byteBuf.WriteBytes(userNameBytes);
        // byteBuf.WriteByte((byte) passwordBytes.Length);
        // byteBuf.WriteBytes(passwordBytes);
        
        byteBuf.WriteIntLE(clientPower);
        byteBuf.WriteIntLE(maxLen);
        byteBuf.WriteByte(Encode);
        byteBuf.WriteBytes(zeroBytes);
        byteBuf.WriteBytes(userNameBytes);
        byteBuf.WriteByte(0);
        byteBuf.WriteByte((byte) passwordBytes.Length);
        byteBuf.WriteBytes(passwordBytes);
        return byteBuf.Array;
    }
}