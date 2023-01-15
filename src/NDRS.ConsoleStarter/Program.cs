using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NDRS.ConsoleStarter;
using NDRS.MySqlServer.Loggers;
using NDRS.MySqlServer.Utils;

// IConfiguration _configuration =
//     new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
//         .Build();

ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddSimpleConsole(c => c.TimestampFormat = "[yyyy-MM-dd HH:mm:ss]")
        .AddFilter(level => level >= LogLevel.Debug);
});

Console.WriteLine(@$"
   _  __  _____  ___    _____
  / |/ / / ___/ / _ \  / ___/
 /    / / /__  / // / / /__  
/_/|_/  \___/ /____/  \___/  
.Net Core Distributed Connector                            
-------------------------------------------------------------------
Author           :  xuejiaming
Version          :  0.0.1
Github Repository:  https://github.com/xuejmnet/NCDC
Gitee Repository :  https://gitee.com/xuejm/NCDC
-------------------------------------------------------------------
Start Time:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");


NDRSLoggerFactory.DefaultFactory = _loggerFactory;

var defaultServerHost = new DefaultServerHost();
await defaultServerHost.StartAsync();
while (Console.ReadLine() != "quit")
{
    Console.WriteLine("看不懂");
}
await defaultServerHost.StopAsync();
Console.WriteLine("退出了");
Console.ReadKey();