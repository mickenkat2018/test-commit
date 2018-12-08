using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loan.Application.Automation.Framework.PageObjects;
using Loan.Application.Webdriver;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using NUnit.Framework;
using Loan.Application.Common.Framework;
namespace Loan.Application.Automation.Framework.Pages
{
    public class HomePage : HomePageElements
    {
        /// <summary>
        /// driver 
        /// </summary>
        private IDriver driver;

        /// <summary>
        /// Common methods
        /// </summary>
        private CommonMethods commonMethods;

        /// <summary>
        /// Home page constructor
        /// </summary>
        public HomePage()
        {
            ////set driver from scenarioContext
            this.driver = ScenarioContext.Current["Driver"] as IDriver;
            this.commonMethods = new CommonMethods();
        }

        /// <summary>
        /// Asserts that user is on home page
        /// </summary>
        public void AssertIsAtHomePage()
        {
            this.driver.WaitForPageLoad(90);
            this.driver.WaitForElementVisible(this.HomePageSwiperSlider, 30);

            if (!this.driver.IsElementVisible(this.HomePageSwiperSlider))
            {
                Assert.Fail("User should be on Home Page but was not");
            }
        }

        /// <summary>
        /// Navigates to Loans Page
        /// </summary>
        public void NavigateToLoansPage()
        {
            this.driver.GoToUrl(this.commonMethods.GetAppConfigPropertyValue(CommonConstants.ClientUrl) + CommonConstants.ShortTermLoanRelativeUrl);
            this.driver.WaitForPageLoad(120);
        }
    }
}
