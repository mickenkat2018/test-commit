using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Loan.Application.Webdriver;
using Loan.Application.Common;
using Loan.Application.Automation.Framework.Pages;
using System.Configuration;
using Loan.Application.Common.Framework;

namespace Loan.Application.Tests
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        public IDriver Driver { get; set; }

        public CommonMethods CommonMethods { get; set; }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Driver = new Driver();
            Driver.StartBrowser();
            ////Alot can be saved into specflow scenario context and accessed anywhere within the framework
            ////Saves passing driver instance as parameter on constructors
            ScenarioContext.Current.Add("Driver", Driver);
            this.CommonMethods = new CommonMethods();
            this.Driver.GoToUrl(this.CommonMethods.GetAppConfigPropertyValue(CommonConstants.ClientUrl));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            this.Driver.StopBrowser();
        }
    }
}
