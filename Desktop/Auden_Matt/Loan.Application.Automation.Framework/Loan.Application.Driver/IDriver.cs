using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Loan.Application.Webdriver
{
    public interface IDriver
    {
        void StartBrowser(string browser = null);
        void Click(string element);
        void Enter(string keys, string element);
        void ClickAllVisible(string locator);
        void WaitForElementVisible(string locator, double timeInSeconds);
        void WaitForPageLoad(double timeInSeconds);
        bool IsElementVisible(string locator);
        bool IsTextPresent(string expectedTextOnPage);
        string GetText(string locator);
        void StopBrowser();
        void GoToUrl(string url);
        IList<IWebElement> GetVisibleElements(string locator);
        void MoveToElement(string locator, int x = 0, int y = 0);
        string ElementValue(string locator, string attributeName);
        void SetAttribute(string locator, string attributeName, string attributeValue);
        void WaitAfewSeconds(int timeInSeconds = 2);
    }
}
