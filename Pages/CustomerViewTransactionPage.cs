using AventStack.ExtentReports;
using OpenQA.Selenium;
using System.Collections;

namespace XyzBankProject.Pages
{
    public class CustomerViewTransactionPage
    {
        private IWebDriver driver;
        private ExtentTest extentTest;
        private Helper helper;

        public CustomerViewTransactionPage(IWebDriver driver, ExtentTest extentTest)
        {
            this.driver = driver;
            this.extentTest = extentTest;
            helper = new Helper(this.driver, extentTest);
        }

        #region Locators
        private By tableHeadersByXpath { get { return By.XPath("//tr[1]/td/a[1]"); } }
        private By resetTransactionsByXpath { get { return By.XPath("//table/descendant::thead/descendant::tr"); } }
        private By tableRowsCountByXpath { get { return By.XPath("//table/descendant::tbody/descendant::tr"); } }
        #endregion

        public void ValidateHeadersInTransactionTable()
        {
            ArrayList actualHeadersList = new ArrayList();
            ArrayList expectedHeadersList = new ArrayList() { "Date-Time", "Amount", "Transaction Type" };

            helper.WaitForElementIsVisible(tableHeadersByXpath);
            IList<IWebElement> headings = driver.FindElements(tableHeadersByXpath);
            foreach (IWebElement heading in headings)
            {
                actualHeadersList.Add(heading.Text);
            };
            Assert.AreEqual(expectedHeadersList, actualHeadersList);
            string printHeaderValues = "";
            foreach (string header in actualHeadersList)
            {
                printHeaderValues = printHeaderValues + header + ", ";

            }
            extentTest.Log(Status.Pass, "<b>Header values in Transaction Table are = </b>" + printHeaderValues);
        }

        public void ValidateNumberOfRecordsInTransactionTable(int expectedCount)
        {
            int actualRowsCount = driver.FindElements(tableRowsCountByXpath).Count();
            Assert.AreEqual(expectedCount, actualRowsCount);
            extentTest.Log(Status.Pass, "Number of expected and actual rows in Transaction Table are equal, <b> Rows Count </b> = " + actualRowsCount);
        }

        public void ValidateDataInEachRecordInTransactionViews()
        {

            IList<IWebElement> rows = driver.FindElements(tableRowsCountByXpath);

            foreach (IWebElement row in rows)
            {
                string rowData = "";
                IList<IWebElement> tableDatas = row.FindElements(By.XPath("./descendant::td"));
                foreach (IWebElement tableData in tableDatas)
                {
                    rowData = rowData + tableData.Text + ", ";

                }
                extentTest.Log(Status.Pass, "<b>Table's Row content is </b>: " + rowData);
            }
        }
    }
}