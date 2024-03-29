﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace NDRS.MySqlServer.Loggers
{
    /// <summary>
    /// 
    /// </summary>
    /// Author: xjm
    /// Created: 2022/5/18 10:02:25
    /// Email: 326308290@qq.com
    public sealed class NDRSLoggerFactory
    {
        static ILoggerFactory _defaultFactory;


        static ILoggerFactory NewDefaultFactory()
        {
            var f = new NullLoggerFactory();
            return f;
        }

        /// <summary>
        ///     Gets or sets the default factory.
        /// </summary>
        public static ILoggerFactory DefaultFactory
        {
            get
            {
                ILoggerFactory factory = Volatile.Read(ref _defaultFactory);
                if (factory == null)
                {
                    factory = NewDefaultFactory();
                    ILoggerFactory current = Interlocked.CompareExchange(ref _defaultFactory, factory, null);
                    if (current != null)
                    {
                        return current;
                    }
                }
                return factory;
            }
            set => Volatile.Write(ref _defaultFactory, value);
        }
        public static ILogger<T> CreateLogger<T>() => DefaultFactory.CreateLogger<T>();
        public static ILogger CreateLogger(string categoryName) => DefaultFactory.CreateLogger(categoryName);
    }
}
