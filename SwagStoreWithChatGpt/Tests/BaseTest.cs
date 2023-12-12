using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SwagStoreWithChatGpt.Tests
{
    public abstract class BaseTest
	{
        [ThreadStatic]
        protected static IWebDriver? driver;

        protected ExtentReports extent;
        protected ExtentTest test;

        protected void Given(string description, Action action)
        {
            Console.WriteLine($"Given {description}");
            action();
        }

        protected void When(string description, Action action)
        {
            Console.WriteLine($"When {description}");
            action();
        }

        protected void Then(string description, Action action)
        {
            Console.WriteLine($"Then {description}");
            action();
        }

        protected void And(string description, Action action)
        {
            Console.WriteLine($"And {description}");
            action();
        }


        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            string dir = TestContext.CurrentContext.TestDirectory + "\\";
            string fileName = this.GetType().ToString() + ".html";
            ExtentSparkReporter htmlReporter = new ExtentSparkReporter(dir + fileName);

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

        }


        /**
         * 
         * 
         * using Selenium WebDriverManager
         * with this we no longer need to install ChromeDriver 
         * or any browser driver as a dependency
         */
        [SetUp]
        public virtual void SetUp()
        {
            if (driver == null)
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public virtual void TearDown()
        {

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }

            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); // Dispose of the driver explicitly
                driver = null; // Set the driver to null to release the reference
            }

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);

        }


        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            extent.Flush();
        }

        protected void TakeScreenshot(string testName)
        {
            if (driver == null || !(driver is ITakesScreenshot))
            {
                Console.WriteLine("Driver is not initialized or does not support taking screenshots.");
                return;
            }

            try
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                var fileName = $"{testName}-{timestamp}.png";
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var screenshotsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                var screenshotPath = Path.Combine(screenshotsDirectory, fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(screenshotsDirectory);

                screenshot.SaveAsFile(screenshotPath); // Updated method call
                Console.WriteLine($"Screenshot saved: {screenshotPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while taking screenshot: {ex.Message}");
            }
        }

    }
}

