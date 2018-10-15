using System.Collections.Generic;
using AventStack.ExtentReports;
using NUnit.Framework;
using System.Linq;
using System;


namespace PortalTask.Helpers
{

    public class Reporter
    {
        private static ExtentTest Logger =>
            (ExtentTest)TestContext.CurrentContext.Test.Properties.Get("htmlLogger");

        public static void Log(Status logStatus, string logMessage) => Logger.Log(logStatus, logMessage);

        public static void LogInfo(string info)
        {
            var formatedInfo = $"{DateTime.Now} INFO: {info}";
            TestContext.Out.WriteLine(formatedInfo);
            Logger.Log(Status.Info, info);
        }

        public static void LogWarn(string warn)
        {
            var formatedInfo = $"{DateTime.Now} WARN: {warn}";
            TestContext.Out.WriteLine(formatedInfo);
            Logger.Log(Status.Warning, warn);
        }

        public static void LogError(string error)
        {
            var formatedError = $"{DateTime.Now} ERROR: {error}";
            TestContext.Out.WriteLine(formatedError);
            Logger.Log(Status.Error, error);
        }

        public static void LogListOfValues(IEnumerable<string> list)
        {
            var preLog = "List of Values: ";
            var log = "";

            foreach (string item in list) log = string.Join(", ", list.ToArray());

            LogInfo(preLog + log);
        }
    }
}
