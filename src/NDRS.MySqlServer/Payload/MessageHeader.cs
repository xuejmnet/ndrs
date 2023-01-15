namespace NDRS.MySqlServer.Payload;

public class MessageHeader
{
    /// <summary>
    /// 内容长度
    /// </summary>
    public int PackageLength { get; }
    /// <summary>
    /// 序号
    /// </summary>
    public int SequenceId { get; }

    public MessageHeader(int packageLength,int sequenceId)
    {
        PackageLength = packageLength;
        SequenceId = sequenceId;
    }
}