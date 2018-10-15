using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System.Reflection;
using System.IO;

namespace PortalTask.Base
{
    internal class ExtentManager
    {
        public static ExtentReports Instance { get; } = new ExtentReports();

        static ExtentManager()
        {
            Instance.AttachReporter(new ExtentHtmlReporter(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TestReport.html"));
        }
    }
}
