﻿using CoreApiProject.Core.Services;

using System.IO;
using System;

namespace CoreApiProject.Infrastructure.Service
{
    public class LoggerService : ILoggerService
    {
        public void Log(string feature, string innerex, string message, string stacktrace, string logfilepath)
        {
            var logFilePath = @"D:\Repository\RealEstateRentalsCoreApi\RealEstateCore\Logs\";
            var filename = "Log_" + Guid.NewGuid().ToString().Replace("-", "") + DateTime.UtcNow.ToString().Replace(" ", "").Replace("/", "").Replace(":", "");
            var filepath = Path.Combine(logFilePath + filename + ".txt");

            if (!File.Exists(filepath))
            {
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("Feature:\n" + feature + "\n" + "Inner Exception:\n" + innerex + "\n" + "Message:\n" + message + "\n" + "Stack Trace:\n" + stacktrace);
                    writer.Close();
                }
            }
        }
    }
}
