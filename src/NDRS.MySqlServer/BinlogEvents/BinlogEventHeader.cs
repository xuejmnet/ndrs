namespace NDRS.MySqlServer.BinlogEvents;

public class BinlogEventHeader
{
    /// <summary>
    /// 事件头长度
    /// </summary>
    public const int EVENT_HEADER_LENGTH = 19;

    public long Timestamp { get; set; }
    public byte TypeCode { get; set; }
    public long ServerId { get; set; }
    public long EventLength { get; set; }
    public long NextPosition { get; set; }
    public int Flags { get; set; }

    public override string ToString()
    {
        return $"EventHeader [{nameof(Timestamp)}: {Timestamp}, {nameof(TypeCode)}: {TypeCode}, {nameof(ServerId)}: {ServerId}, {nameof(EventLength)}: {EventLength}, {nameof(NextPosition)}: {NextPosition}, {nameof(Flags)}: {Flags}]";
    }
}