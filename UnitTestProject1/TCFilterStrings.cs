using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace TCforMvcApp
{
    [TestClass]
    public class FilterByText
    {
            private static IWebDriver driver;
            private StringBuilder verificationErrors;
            private static string baseURL;
            private bool acceptNextAlert = true;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            Actions action = new Actions(driver);

        [ClassInitialize]
            public static void InitializeClass(TestContext testContext)
            {
                driver = new ChromeDriver();
                baseURL = "http://localhost:61603";
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);

        }

        [ClassCleanup]
            public static void CleanupClass()
            {
                try
                {
                    //driver.Quit();// quit does not close the window
                    driver.Close();
                    driver.Dispose();
                }
                catch (Exception)
                {
                    // Ignore errors if unable to close the browser
                }
            }

            [TestInitialize]
            public void InitializeTest()
            {
                verificationErrors = new StringBuilder();
            }

            [TestCleanup]
            public void CleanupTest()
            {
                Assert.AreEqual("", verificationErrors.ToString());
            }

            [TestMethod]
            public void TheFilterByTextTest()
            {
                driver.Navigate().GoToUrl(baseURL);
                driver.FindElement(By.XPath("(.//a[@title='Column Settings'])[5]")).Click();
                Thread.Sleep(2000);
                var elementFilter = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Filter'])")));
                action.MoveToElement(elementFilter);
                Thread.Sleep(2000);
                var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Is equal to'])")));
                action.MoveToElement(element).Perform();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Is equal to'])")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cotains'])")).Click();
                String smallestTextInGrid = driver.FindElement(By.XPath("//div[@id='grid']/div[3]/table/tbody/tr[1]/td[5]")).Text;
                Thread.Sleep(3000);
            }
            private bool IsElementPresent(By by)
            {
                try
                {
                    driver.FindElement(by);
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }

            private bool IsAlertPresent()
            {
                try
                {
                    driver.SwitchTo().Alert();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            }

            private string CloseAlertAndGetItsText()
            {
                try
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    if (acceptNextAlert)
                    {
                        alert.Accept();
                    }
                    else
                    {
                        alert.Dismiss();
                    }
                    return alertText;
                }
                finally
                {
                    acceptNextAlert = true;
                }
            }
    }
}