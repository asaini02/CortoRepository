using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;

namespace XyzBankProject
{
    public class BaseTest
    {
        #region Base class variables
        public ExtentReports extentReports;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public ThreadLocal<ExtentTest> extentTest = new ThreadLocal<ExtentTest>();
        string reportPath = "C:\\Users\\Amandeep Saini\\source\\repos\\XyzBankProject\\TestResults\\" + $"Report_{DateTime.UtcNow.ToString("yyyyMMdd_hhMMss")}.html";
        #endregion

        [OneTimeSetUp]
        public void OnTimeSetupMethod()
        {
            #region Declare ExtentReports class to generate Html report.
            extentReports = new ExtentReports();
            #endregion

            #region Attach reportPath string to ExtentSparkerReporter class.            
            ExtentSparkReporter extentSparkReporter = new ExtentSparkReporter(reportPath);
            #endregion

            #region attach extentSparkReporter object to ExtentReport class.
            extentReports.AttachReporter(extentSparkReporter);
            #endregion
        }

        [SetUp]
        public void InitiateBrowser()
        {
            extentTest.Value = extentReports.CreateTest(TestContext.CurrentContext.Test.Name, "Execution started from base test");

            #region Fetch browser from App.config file.
            var browserName = ConfigurationManager.AppSettings.Get("BrowserName");
            if (string.IsNullOrEmpty(browserName))
            {
                throw new Exception("BrowserName is null.");
            }
            StartBrowser(browserName);
            #endregion

            #region Fetch application URL from App.config file.
            var url = ConfigurationManager.AppSettings.Get("ApplicationUrl");
            if (string.IsNullOrEmpty(browserName))
            {
                throw new Exception("Application URL is null.");
            }
            driver.Value.Navigate().GoToUrl(url);
            #endregion

            driver.Value.Manage().Window.Maximize();
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void TearDown()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var message = TestContext.CurrentContext.Result.Message;
            if (testStatus == TestStatus.Failed)
            {
                extentTest.Value.Fail("Test outcome is Fail.", TakeScreenshot(driver.Value));
                extentTest.Value.Log(Status.Fail, "Log stackTrace = " + stackTrace, TakeScreenshot(driver.Value));
                extentTest.Value.Log(Status.Fail, "Log message = " + message, TakeScreenshot(driver.Value));
            }
            else if (TestContext.CurrentContext.Result.Outcome.Status.ToString().ToLower().Equals("inconclusive"))
            {
                extentTest.Value.Log(Status.Info, "Test outcome is inconclusive.", TakeScreenshot(driver.Value));
            }
            else if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Passed))
            {
                extentTest.Value.Log(Status.Pass, "Test outcome is pass.", TakeScreenshot(driver.Value));
            }

            driver.Value.Quit();
            try
            {
                extentReports.Flush();  // Finalize and save the report
            }
            catch (IOException ex)
            {
                extentTest.Value.Log(Status.Error, $"Error flushing report: {ex.Message}");
            }
        }

        private void StartBrowser(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new ChromeDriver();
                    break;
            }
        }

        public static Media TakeScreenshot(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            string scrAsBas64Str = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(scrAsBas64Str, $"Screenshot_{DateTime.UtcNow.ToString("yyyyMMdd_hhMMss")}.png").Build();
        }
    }
}