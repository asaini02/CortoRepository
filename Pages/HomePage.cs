using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace XyzBankProject.Pages
{
    public class HomePage
    {
        IWebDriver driver;
        ExtentTest extentTest;
        public HomePage(IWebDriver driver, ExtentTest extentTest)
        {
            this.driver = driver;
            this.extentTest = extentTest;
        }

        #region Locators
        private By CustomerBtnByXpath { get { return By.XPath("//button[text()='Customer Login']"); } }
        private By BankManagerBtnByXpath { get { return By.XPath("//button[text()='Bank Manager Login']"); } }
        private By HomeBtnByXpath { get { return By.XPath("//button[@class='btn home']"); } }
        #endregion

        public void ClickHome()
        {
            Helper helper = new Helper(driver, extentTest);
            helper.ClickElement(this.HomeBtnByXpath);
        }

        public void ClickCustomerLogin()
        {
            Helper helper = new Helper(driver, extentTest);
            helper.ClickElement(this.CustomerBtnByXpath);
        }

        public void ClickBankManagerLogin()
        {
            Helper helper = new Helper(driver, extentTest);
            helper.ClickElement(this.BankManagerBtnByXpath);
            helper.Wait(2);
        }
    }
}