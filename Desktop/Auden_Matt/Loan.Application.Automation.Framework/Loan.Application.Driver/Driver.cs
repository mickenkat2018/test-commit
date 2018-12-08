
namespace Loan.Application.Webdriver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using NUnit.Framework;
    using OpenQA.Selenium.Support.UI;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Remote;

    public class Driver : IDriver
    {
        /// <summary>
        /// _driver instance
        /// </summary>
        private IWebDriver _driver;

        /// <summary>
        /// Current Browser
        /// </summary>
        /// <returns></returns>
        public IWebDriver Browser()
        {
            return this._driver;
        }

        /// <summary>
        /// Starts a named driver, default set to chrome
        /// </summary>
        /// <param name="browser"></param>
        public void StartBrowser(string browser = "")
        {
            switch (browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    _driver.Manage().Window.Maximize();
                    break;
                default:
                    _driver = new ChromeDriver();
                    _driver.Manage().Window.Maximize();
                    break;
            }
        }

        public void StopBrowser()
        {
            this.Browser().Dispose();
        }

        /// <summary>
        /// Clicks on the named visible element
        /// </summary>
        /// <param name="locator"></param>
        public void Click(string locator)
        {
            if (string.IsNullOrEmpty(locator))
            {
                throw new Exception("Element can not be null");
            }

            try
            {
                this.FindElementVisible(locator).Click();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Clicks on all multiple visible elements on the page
        /// </summary>
        /// <param name="locator"></param>
        public void ClickAllVisible(string locator)
        {
            try
            {
                foreach (IWebElement element in this.GetAllWebElements(locator))
                {
                    if (element.Displayed)
                    {
                        element.Click();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        /// <summary>
        /// Enters text into an element visible on the page
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="locator"></param>
        public void Enter(string keys, string locator)
        {
            var element = this.FindElementVisible(locator);
            if (element != null)
            {
                element.Clear();
                element.SendKeys(keys);
            }
            else
            {
                throw new Exception("Element " + locator + " is not found");
            }
        }

        /// <summary>
        /// Waits until an elemet is visible on the page within a specified time frame
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeInseconds"></param>
        public void WaitForElementVisible(string locator, double timeInseconds)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks);
            double totalTime = this.ClockTime(ts);
            while (this.FindElementVisible(locator) == null && totalTime <= timeInseconds)
            {
                totalTime = Convert.ToInt32(this.ClockTime(ts));
                if (totalTime >= timeInseconds)
                {
                    Assert.Fail("Unable to find " + locator + " after " + totalTime + " seconds");
                }
            }
        }

        /// <summary>
        /// Waits Until the DOM is ready / page is rendered
        /// </summary>
        /// <param name="timeInSeconds"></param>
        public void WaitForPageLoad(double timeInSeconds)
        {
            IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(this._driver, TimeSpan.FromSeconds(timeInSeconds));
            wait.Until(a => ((IJavaScriptExecutor)this._driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        /// <summary>
        /// Returns true if element is visible on the page else false
        /// </summary>
        /// <param name="element"></param>
        public bool IsElementVisible(string element)
        {
            if (this.FindElementVisible(element) != null)
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// Returns true if specified "text" is present on the page else false
        /// </summary>
        /// <param name="text"></param>
        public bool IsTextPresent(string text)
        {
            if (this.Browser().PageSource.Contains(text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns text held within a page object/ element
        /// </summary>
        /// <param name="locator"></param>
        public string GetText(string locator)
        {
           var element = this.FindElementVisible(locator);
            if (element !=null)
            {
                return element.Text;
            }

            return null;
        }

        /// <summary>
        /// Returns on that elemet visible and enabled within the page
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public IWebElement FindElementVisible(string locator)
        {
            try
            {
                if (this.FindElement(By.Id(locator)) != null)
                {
                    foreach (IWebElement element in this._driver.FindElements(By.Id(locator)))
                    {
                        if (element.Displayed)
                        {
                            return element;
                        }
                    }
                }
                else if (this.FindElement(By.ClassName(locator)) != null)
                {
                    foreach (IWebElement element in this._driver.FindElements(By.ClassName(locator)))
                    {
                        if (element.Displayed)
                        {
                            return element;
                        }
                    }
                }
                else if (this.FindElement(By.CssSelector(locator)) != null)
                {
                    foreach (IWebElement element in this._driver.FindElements(By.CssSelector(locator)))
                    {
                        if (element.Displayed)
                        {
                            return element;
                        }
                    }
                }
                else if (this.FindElement(By.LinkText(locator)) != null)
                {
                    foreach (IWebElement el in this._driver.FindElements(By.LinkText(locator)))
                    {
                        if (el.Displayed)
                        {
                            return el;
                        }
                    }
                }
                else if (this.FindElement(By.Name(locator)) != null)
                {
                    foreach (IWebElement element in this._driver.FindElements(By.Name(locator)))
                    {
                        if (element.Displayed)
                        {
                            return element;
                        }
                    }
                }
                else if (this.FindElement(By.XPath(locator)) != null)
                {
                    foreach (IWebElement element in this._driver.FindElements(By.XPath(locator)))
                    {
                        if (element.TagName == "select" && element.Enabled)
                        {
                            return element;
                        }

                        if (element.Displayed)
                        {
                            return element;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        /// <summary>
        /// Simple clock timer, returns clock ticks as seconds
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private double ClockTime(TimeSpan timeSpan)
        {
            var timeInSec1 = new TimeSpan(DateTime.Now.Ticks);
            var TimeInSec2 = timeSpan - timeInSec1;
            return Math.Abs(TimeInSec2.TotalSeconds);
        }

        /// <summary>
        /// Returns an element by specified locator
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        private IWebElement FindElement(By by)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                return _driver.FindElement(by);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all elements with a matching locator, whether visible or not
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        private ICollection<IWebElement> GetAllWebElements(string locator)
        {
            ICollection<IWebElement> el = null;
            if (this.FindElement(By.Id(locator)) != null)
            {
                el = this.Browser().FindElements(By.Id(locator));
            }
            else if (this.FindElement(By.Name(locator)) != null)
            {
                el = this.Browser().FindElements(By.Name(locator));
            }
            else if (this.FindElement(By.LinkText(locator)) != null)
            {
                el = this.Browser().FindElements(By.LinkText(locator));
            }
            else if (this.FindElement(By.TagName(locator)) != null)
            {
                el = this.Browser().FindElements(By.TagName(locator));
            }
            else if (this.FindElement(By.XPath(locator)) != null)
            {
                el = this.Browser().FindElements(By.XPath(locator));
            }
            else if (this.FindElement(By.ClassName(locator)) != null)
            {
                el = this.Browser().FindElements(By.ClassName(locator));
            }

            return el;
        }

        /// <summary>
        /// Returns a list of only visible elements on the page by specific locator
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public IList<IWebElement> GetVisibleElements(string locator)
        {
            IList<IWebElement> elements = new List<IWebElement>();
            foreach (var e in this.GetAllWebElements(locator))
            {
                if (e.Displayed)
                {
                    elements.Add(e);
                }
            }

            return elements;
        }

        /// <summary>
        /// Navigates to specified url
        /// </summary>
        /// <param name="url"></param>
        public void GoToUrl(string url)
        {
            this._driver.Navigate().GoToUrl(url);
            this.WaitForPageLoad(60);
        }

        public void MoveToElement(string element, int x = 0, int y = 0)
        {
            Actions action = new Actions(this._driver);
            action.MoveToElement(this.FindElementVisible(element), x, y);
            action.Click().Perform();
        }

        public string ElementValue(string locator, string attributeName)
        {
            var element = this.FindElementVisible(locator);

            if (element != null)
            {
                return element.GetAttribute(attributeName);
            }

            return null;
        }

        public void SetAttribute(string locator, string attributeName, string attributeValue)
        {
            ((IJavaScriptExecutor)this._driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", this.FindElementVisible(locator), attributeName, attributeValue);
        }

        public void WaitAfewSeconds(int timeInSeconds = 2)
        {
            System.Threading.Thread.Sleep(timeInSeconds * 1000);
        }
    }
}
