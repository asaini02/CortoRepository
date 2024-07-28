using XyzBankProject.Pages;

namespace XyzBankProject.TestScripts
{
    public class MainTestScript : BaseTest
    {
        [Test, Category("EndToEndTest")]
        public void TestEnd2EndFlow()
        {
            #region Go to Manager Login > AddCustomer > OpenAccount.
            TestManager_AddCustomer_OpenAccount();
            #endregion

            #region Go to Home page.
            var node0 = extentTest.Value.CreateNode("Home page");
            HomePage homePage = new HomePage(driver.Value, node0);
            homePage.ClickHome();
            #endregion

            #region Go to Customer Login > Deposit > Withdrawl >View Transactions.
            TestCustomer_Login_Deposit_Withdrawl_Transactions();
            #endregion
        }

        [Test, Category("Regression")]
        public void TestManager_AddCustomer_OpenAccount()
        {
            var node1 = extentTest.Value.CreateNode("Home page - Bank Manager Login");
            HomePage homePage = new HomePage(driver.Value, node1);
            homePage.ClickBankManagerLogin();

            var node2 = extentTest.Value.CreateNode("Manager - Add customer button - Fill data in all fields.");
            ManagerDashboard managerDashboard = new ManagerDashboard(driver.Value, node2);
            managerDashboard.ClickAddCustomerButton();
            string firstName = "Amandeep";
            string lastName = "Saini";
            string postCode = "2142";
            managerDashboard.AddCustomers(firstName, lastName, postCode);

            var node3 = extentTest.Value.CreateNode("Manager - Add customer - Accept Alert");
            managerDashboard = new ManagerDashboard(driver.Value, node3);
            managerDashboard.AcceptAlert();

            var node4 = extentTest.Value.CreateNode("Manager - Open Account ");
            managerDashboard = new ManagerDashboard(driver.Value, node4);
            managerDashboard.ClickOpenAccountButton();
            string currency = "Dollar";
            managerDashboard.EnterDataInOpenAccount(firstName + " " + lastName, currency);
            managerDashboard.AcceptAlert();
        }

        [Test, Category("Smoke")]
        public void TestManagerLogin()
        {
            HomePage homePage = new HomePage(driver.Value, extentTest.Value);
            homePage.ClickBankManagerLogin();            
        }

        private void TestCustomer_Login_Deposit_Withdrawl_Transactions()
        {
            var node1 = extentTest.Value.CreateNode("Home page - Customer Login");
            HomePage homePage = new HomePage(driver.Value, node1);
            homePage.ClickCustomerLogin();
            CustomerLoginPage customerLoginPage = new CustomerLoginPage(driver.Value, node1);
            customerLoginPage.SelectYourNameDropdown("Amandeep Saini");
            customerLoginPage.ClickLoginButton();

            var node2 = extentTest.Value.CreateNode("Customer - Deposit");
            CustomerDashboardPage customerDashboardPage = new CustomerDashboardPage(driver.Value, node2);
            customerDashboardPage.ClickDepositButton();
            customerDashboardPage.EnterAmount("100");
            customerDashboardPage.ClickSubmitDepositButton();
            customerDashboardPage.ValidateSuccessMessage("Deposit Successful");

            var node3 = extentTest.Value.CreateNode("Customer - Withdrawl");
            customerDashboardPage = new CustomerDashboardPage(driver.Value, node3);
            customerDashboardPage.ClickWithdrawlButton();
            customerDashboardPage.EnterAmount("20");
            customerDashboardPage.ClickSubmitWithdrawlButton();
            customerDashboardPage.ValidateSuccessMessage("Transaction successful");

            var node4 = extentTest.Value.CreateNode("Customer - Transactions");
            customerDashboardPage.ClickTransactionsButton();
            CustomerViewTransactionPage customerViewTransactionPage = new CustomerViewTransactionPage(driver.Value, node4);
            customerViewTransactionPage.ValidateHeadersInTransactionTable();
            customerViewTransactionPage.ValidateNumberOfRecordsInTransactionTable(2);
            customerViewTransactionPage.ValidateDataInEachRecordInTransactionViews();
        }
    }
}