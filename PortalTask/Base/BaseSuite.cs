using System.Runtime.CompilerServices;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using PortalTask.Helpers;

namespace PortalTask.Base
{
    /// <summary>
    /// Base Test suite class to instantiate reporting of tests. 
    /// </summary>
    [SetUpFixture]
    [Parallelizable]
    public class BaseSuite
    {
        private static readonly IReadOnlyDictionary<TestStatus, Func<string, Status>> LogStatusMap = new Dictionary<TestStatus, Func<string, Status>>
        {
            { TestStatus.Inconclusive, label => Status.Warning },
            { TestStatus.Skipped, label => Status.Skip },
            { TestStatus.Passed, label => Status.Pass },
            { TestStatus.Warning, label => Status.Warning },
            { TestStatus.Failed, label => label.Equals("error", StringComparison.OrdinalIgnoreCase) ? Status.Error : Status.Fail }
        };

        private ExtentReports extent;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            extent = ExtentManager.Instance;
        }

        [OneTimeTearDown]
        public void FixtureTeardown()
        {
            extent.Flush();
        }

        [TearDown]
        public void TearDown()
        {
            var result = TestContext.CurrentContext.Result;
            var message = !string.IsNullOrEmpty(result.Message) ? $"<pre>{result.Message}</pre>" : string.Empty;
            var stacktrace = !string.IsNullOrEmpty(result.StackTrace) ? $"<pre>{result.StackTrace}</pre>" : string.Empty;

            var status = LogStatusMap[result.Outcome.Status](result.Outcome.Label);
            Reporter.Log(status, $"Test ended with {status}{message}{stacktrace}");
        }

        protected void RunTest<T>([CallerMemberName] string callerMemberName = "")
            where T : BaseTest, new()
        {
            T test = null;
            try
            {
                TestContext.CurrentContext.Test.Properties.Add("htmlLogger", extent.CreateTest(callerMemberName));
               
                test = new T();
                test.Run();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
