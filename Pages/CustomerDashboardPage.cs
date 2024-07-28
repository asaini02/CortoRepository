using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace XyzBankProject.Pages
{
    public class CustomerDashboardPage
    {
        private IWebDriver driver;
        private ExtentTest extentTest;
        private Helper helper;

        public CustomerDashboardPage(IWebDriver driver, ExtentTest extentTest)
        {
            this.driver = driver;
            this.extentTest = extentTest;
            this.helper = new Helper(driver, extentTest);
        }

        #region Locators : Transaction Summary page 
        private By transactionsBtnByXpath { get { return By.XPath("//button[contains(text(),'Transactions')]"); } }
        private By DepositMenuBtnByXpath { get { return By.XPath("//button[@ng-class='btnClass2' and contains(text(),'Deposit')]"); } }
        private By DepositSubmitBtnByXpath { get { return By.XPath("//button[@type='submit' and text()='Deposit']"); } }
        private By WithdrawlMenuBtnByXpath { get { return By.XPath("//button[@ng-class='btnClass3' and contains(text(),'Withdrawl')]"); } }
        private By WithdrawSubmitBtnByXpath { get { return By.XPath("//button[@type='submit' and text()='Withdraw']"); } }
        private By AmountTextByXpath { get { return By.XPath("//input[@type='number' and @placeholder='amount']"); } }
        private By messageByXpath { get { return By.XPath("//*[@name='myForm']/../span[@ng-show='message']"); } }
        #endregion

        public void ClickTransactionsButton()
        {
            helper.Wait(4);
            helper.ClickElement(transactionsBtnByXpath);
        }

        public void ClickDepositButton()
        {
            helper.WaitForElementToBeClickable(DepositMenuBtnByXpath);
            helper.ClickElement(DepositMenuBtnByXpath);
        }

        public void ClickWithdrawlButton()
        {
            helper.ClickElement(WithdrawlMenuBtnByXpath);
        }

        public void EnterAmount(string amount)
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));
            helper.WaitForElementToBeClickable(AmountTextByXpath);
            helper.SendKeys(AmountTextByXpath, amount);
            extentTest.Log(Status.Info, $"<b>Amount entered </b>= {amount}");
        }

        public void ClickSubmitDepositButton()
        {
            helper.WaitForElementToBeClickable(DepositSubmitBtnByXpath);
            helper.ClickElement(DepositSubmitBtnByXpath);
            BaseTest.TakeScreenshot(driver);
        }

        public void ClickSubmitWithdrawlButton()
        {
            helper.WaitForElementToBeClickable(WithdrawSubmitBtnByXpath);
            helper.ClickElement(WithdrawSubmitBtnByXpath);
            BaseTest.TakeScreenshot(driver);
        }

        public void ValidateSuccessMessage(string expectedMessage)
        {
            helper.WaitForTextToBePresentInElementLocated(messageByXpath, expectedMessage, 20);
            string? actualMessage = helper.GetText(messageByXpath);
            Assert.AreEqual(expectedMessage, actualMessage);
            extentTest.Log(Status.Info, "<b>Success message = </b>" + actualMessage);
        }
    }
}