namespace NDRS.MySqlServer;

public class PowerType
{
    
    public static readonly int CLIENT_LONG_PASSWORD = 0x0001; // new more secure passwords
    public static readonly int CLIENT_FOUND_ROWS = 0x0002; // Found instead of affected rows
    public static readonly int CLIENT_LONG_FLAG = 0x0004; // Get all column flags
    public static readonly int CLIENT_CONNECT_WITH_DB = 0x0008; // One can specify db on connect
    public static readonly int CLIENT_NO_SCHEMA = 0x0010; // Do not allow database.table.column
    public static readonly int CLIENT_COMPRESS = 0x0020; // Can use compression protocol
    public static readonly int CLIENT_ODBC = 0x0040; // Odbc client
    public static readonly int CLIENT_LOCAL_FILES = 0x0080; // Can use LOAD DATA LOCAL
    public static readonly int CLIENT_IGNORE_SPACE = 0x0100; // Ignore spaces before '('
    public static readonly int CLIENT_PROTOCOL_41 = 0x0200; // New 4.1 protocol
    public static readonly int CLIENT_INTERACTIVE = 0x0400; // This is an interactive client
    public static readonly int CLIENT_SSL = 0x0800; // Switch to SSL after handshake
    public static readonly int CLIENT_IGNORE_SIGPIPE = 0x1000; // IGNORE sigpipes
    public static readonly int CLIENT_TRANSACTIONS = 0x2000; // Client knows about transactions
    public static readonly int CLIENT_RESERVED = 0x4000; // Old flag for 4.1 protocol
    public static readonly int CLIENT_SECURE_CONNECTION = 0x8000; // New 4.1 authentication
    public static readonly int CLIENT_MULTI_STATEMENTS = 0x00010000; // Enable/disable multi-stmt support
    public static readonly int CLIENT_MULTI_RESULTS = 0x00020000; // Enable/disable multi-results
}