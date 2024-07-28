using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace XyzBankProject.Pages
{
    public class ManagerDashboard
    {
        private IWebDriver driver;
        private ExtentTest extentTest;
        private Helper helper;

        public ManagerDashboard(IWebDriver driver, ExtentTest extentTest)
        {
            this.driver = driver;
            this.extentTest = extentTest;
            helper = new Helper(this.driver, extentTest);
        }

        #region Locators - Menu buttons
        private By AddCustomerMenuBtnByXpath { get { return By.XPath("//button[@ng-class='btnClass1']"); } }
        private By OpenAccountMenuBtnByXpath { get { return By.XPath("//button[@ng-class='btnClass2']"); } }
        private By CustomersMenuBtnByXpath { get { return By.XPath("//button[@ng-class='btnClass3']"); } }
        #endregion

        #region Locators - Add customer
        private By FirstNameByXpath { get { return By.XPath("//input[@placeholder='First Name']"); } }
        private By LastNameByXpath { get { return By.XPath("//input[@placeholder='Last Name']"); } }
        private By PostCodeByXpath { get { return By.XPath("//input[@placeholder='Post Code']"); } }
        private By AddCustomerSubmitBtnByXpath { get { return By.XPath("//button[@type='submit' and text()='Add Customer']"); } }
        #endregion

        #region Locators - Open Account
        private By customerDropdownByXpath { get { return By.XPath("//select[@id='userSelect']"); } }
        private By currencyDropdownByXpath { get { return By.XPath("//select[@id='currency']"); } }
        private By processButtonByXpath { get { return By.XPath("//button[@type='submit' and text()='Process']"); } }
        #endregion

        #region Menu buttons
        public void ClickAddCustomerButton()
        {
            helper.ClickElement(AddCustomerMenuBtnByXpath);
            helper.WaitForElementToBeClickable(FirstNameByXpath);
        }

        public void ClickOpenAccountButton()
        {
            helper.ClickElement(OpenAccountMenuBtnByXpath);
        }

        public void ClickCustomerButton()
        {
            helper.ClickElement(CustomersMenuBtnByXpath);
        }
        #endregion

        #region Add Customers methods.
        public void AddCustomers(string firstName, string lastName, string postCode)
        {
            helper.SendKeys(FirstNameByXpath, firstName);
            helper.SendKeys(LastNameByXpath, lastName);
            helper.SendKeys(PostCodeByXpath, postCode);
            helper.ClickElement(AddCustomerSubmitBtnByXpath);
        }

        public void AcceptAlert()
        {
            helper.SwitchToAlert_And_Accept();
        }
        #endregion

        #region Open Account Methods.
        public void EnterDataInOpenAccount(string customerDropdownValue, string currencyDropdownValue)
        {
            helper.SelectDropdown(customerDropdownByXpath, customerDropdownValue);
            helper.SelectDropdown(currencyDropdownByXpath, currencyDropdownValue);
            helper.Wait(2);
            helper.ClickElement(processButtonByXpath);
        }
        #endregion
    }
}