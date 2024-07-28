using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace XyzBankProject.Pages
{
    public class CustomerLoginPage
    {
        private IWebDriver driver;
        private ExtentTest extentTest;
        private Helper helper;

        public CustomerLoginPage(IWebDriver driver, ExtentTest extentTest)
        {
            this.driver = driver;
            this.extentTest = extentTest;
            helper = new Helper(this.driver, extentTest);
        }

        #region Locators
        private By nameDropdownByXpath { get { return By.XPath("//select[@id='userSelect']"); } }
        private By loginBtnByXpath { get { return By.XPath("//*[@type='submit']"); } }
        #endregion

        public void SelectYourNameDropdown(string nameTobeSelected)
        {
            helper.WaitForElementIsVisible(nameDropdownByXpath);
            helper.WaitForElementToBeClickable(nameDropdownByXpath);
            helper.SelectDropdown(nameDropdownByXpath, nameTobeSelected);
        }

        public void ClickLoginButton()
        {
            Helper helper = new Helper(driver, extentTest);
            helper.WaitForElementIsVisible(loginBtnByXpath);
            helper.WaitForElementToBeClickable(loginBtnByXpath);
            helper.ClickElement(loginBtnByXpath);
        }
    }
}