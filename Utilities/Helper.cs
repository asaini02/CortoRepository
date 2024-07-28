using AngleSharp.Dom;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using XyzBankProject;
using static System.Net.Mime.MediaTypeNames;

namespace XyzBankProject
{
    public class Helper
    {
        private IWebDriver driver;
        private ExtentTest extentTest;
        public Helper(IWebDriver driver, ExtentTest extentTest)
        {
            this.driver = driver;
            this.extentTest = extentTest;
        }

        public void ClickElement(By locator)
        {
            try
            {
                WaitForElementToBeClickable(locator);
                driver.FindElement(locator).Click();
                extentTest.Log(Status.Pass, $"Clicked element located by: {locator}");
            }
            catch (Exception ex)
            {
                extentTest.Log(Status.Fail, $"Failed to click element located by: {locator}. \n Exception: {ex.Message}");
                throw;
            }
        }

        public void SendKeys(By locator, string text)
        {
            try
            {
                WaitForElementToBeClickable(locator);
                var element = driver.FindElement(locator);
                element.Clear();
                element.SendKeys(text);
                extentTest.Log(Status.Pass, $"Entered <b>'{text}'</b> to element located by: {locator}");
            }
            catch (Exception ex)
            {
                extentTest.Log(Status.Fail, $"Failed to send keys '{text}' to element located by: {locator}. \n Exception: {ex.Message}");
                throw;
            }
        }

        public string GetText(By locator)
        {
            string text = driver.FindElement(locator).Text;
            if (!String.IsNullOrEmpty(text))
            {
                extentTest.Log(Status.Pass, $"Text '{text}' is present in element located by: {locator}");
            }
            else
            {
                extentTest.Log(Status.Fail, $"Text '{text}' is not present in element located by: {locator}");
            }
            return text;
        }

        public void WaitForTextToBePresentInElementLocated(By locator, string textTobePresent)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(locator, textTobePresent));
        }
        public void WaitForTextToBePresentInElementLocated(By locator, string textTobePresent, int timeoutInSeconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(drv =>
            {
                var elementText = drv.FindElement(locator).Text;
                return elementText.Contains(textTobePresent);
            });
        }

        public void WaitForElementIsVisible(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public void WaitForElementToBeClickable(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public void WaitForAlert()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public void Wait(double seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        public void SwitchToAlert_And_Accept()
        {
            WaitForAlert();
            string alertText = driver.SwitchTo().Alert().Text;
            extentTest.Log(Status.Info, $"Text displayed in Alert = <b>{alertText}</b> ");
            driver.SwitchTo().Alert().Accept();
            extentTest.Log(Status.Info, $"Alert accepted.");
        }

        public void SelectDropdown(By locator, string dropdownValue)
        {
            WaitForElementToBeClickable(locator);
            SelectElement ss = new SelectElement(driver.FindElement(locator));
            ss.SelectByText(dropdownValue);
            extentTest.Log(Status.Info, $"<b>{dropdownValue}</b> is selected in dropdown by locator : {locator}.");
        }
    }
}