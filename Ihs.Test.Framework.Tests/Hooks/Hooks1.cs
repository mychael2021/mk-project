using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Ihs.Test.Framework.Webdriver;
using NUnit.Framework;
using System.IO;
using Ihs.Test.Framework.Common;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;

namespace Ihs.Test.Framework.Tests.Hooks
{
    [Binding]
    public sealed class Hooks1
    {
        IDriver _driver;
        private static AventStack.ExtentReports.ExtentReports extentReport;
        private static ExtentTest _scenarioTestReport;
        private static ExtentTest _featureTestReport;
        private static ExtentHtmlReporter _htmlReporter;
        private static string _reportPath;
        private static string _browser;
        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
             SpecflowHelper.InsertScenarioSteps(scenarioContext, _scenarioTestReport);
        }

        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {
            scenarioContext.Remove(CommonConstants.Step);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            _driver = new Driver();
            Random rnd = new Random();
            _driver.StartBrowser(rnd.Next(1, 3) < 2 ? CommonConstants.Chrome : CommonConstants.Firefox, 
                path: Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Drivers");

            _browser = _driver.BrowserName();
            _driver.Navigate(CommonConstants.DotnetFiddleWebsiteUrl);
            scenarioContext.Add("driver", _driver);
            var scenarioName = TestContext.CurrentContext.Test.Name.Split('(').FirstOrDefault();
            var f = TestContext.CurrentContext.Test.Name.Split('(').LastOrDefault().Trim(')').Split(',');
            scenarioName = scenarioName + ": Given First name =>" + f[1] + " AND Expected Output text => " + f[0] + " Browser =>" + _browser;
            _scenarioTestReport = _featureTestReport.CreateNode<Scenario>(scenarioName);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Dispose();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureTestReport = extentReport.CreateTest<Feature>(featureContext.FeatureInfo.Description);
        }
        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            extentReport.AddSystemInfo("Feature Description:", featureContext.FeatureInfo.Description);
            extentReport.AddSystemInfo("Browser:", _browser);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _reportPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
            + "\\Reports\\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MMMM") + "\\" +
            DateTime.Now.ToString("dd") + "\\" + DateTime.Now.ToString("HH-mm-ss") + "\\";
            _htmlReporter = new ExtentHtmlReporter(_reportPath);
            _htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extentReport = new AventStack.ExtentReports.ExtentReports();

            extentReport.AttachReporter(_htmlReporter);
            CommonMethods.CreateDirectoryIfDoesntExist(_reportPath);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extentReport.Flush();
        }
    }
}
