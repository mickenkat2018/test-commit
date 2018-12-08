using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Loan.Application.Automation.Framework.Pages;
using Loan.Application.Common.Framework;

namespace Loan.Application.Tests.Steps
{
    [Binding]
    public sealed class StepDefinitions
    {
        /// <summary>
        /// Set all class objects below, driver instance passed to them through scenario session
        /// </summary>
        public HomePage HomePage = new HomePage();

        public LoanCalculatorPage LoanCalculatorPage = new LoanCalculatorPage();

        public CommonMethods CommonMethods = new CommonMethods();

        /// <summary>
        /// Assert we're on client site, Url in app config
        /// </summary>
        [Given(@"Loan applicant navigates to Auden home page")]
        public void GivenLoanApplicantNavigatesToAudenHomePage()
        {
            this.HomePage.AssertIsAtHomePage();
        }

        /// <summary>
        /// Navigate to Loands page
        /// </summary>
        [When(@"He clicks on Loans navigation button on the top of the page")]
        public void WhenHeClicksOnLoansNavigationButtonOnTheTopOfThePage()
        {
            this.HomePage.NavigateToLoansPage();
        }

        /// <summary>
        /// Assert we're on Loans page
        /// </summary>
        [Then(@"He should be redirected to the Loans Calculator Page")]
        public void ThenHeShouldBeRedirectedToTheLoansCalculatorPage()
        {
            this.LoanCalculatorPage.AssertIsAtLoansCalculatorPage();
        }

        /// <summary>
        /// Selects Weekely as repayment type
        /// </summary>
        [Then(@"He should select Weekely as instalment repayment")]
        public void ThenHeShouldSelectWeekelyAsInstalmentRepayment()
        {
            this.LoanCalculatorPage.SelectInstalmentType("Weekly");
        }

        /// <summary>
        /// Selects Days as repayment type
        /// </summary>
        [Then(@"He should select Day as instalment repayment")]
        public void ThenHeShouldSelectDayaAsInstalmentRepayment()
        {
            this.LoanCalculatorPage.SelectInstalmentType("Daily");
        }

        /// <summary>
        /// Assert minimum and maximum loans
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        [Then(@"Should assert that minimum loan is (.*) and maximum loan is (.*)")]
        public void ThenShouldAssertThatMinimumLoanIsAndMaximumLoanIs(string min, string max)
        {
            this.LoanCalculatorPage.AssertMinAndMaxLoan(min, max);
        }

        /// <summary>
        /// Assert Slider amount is matching Loan amount
        /// </summary>
        [Then(@"Assert Selected loan amount is displayed")]
        public void ThenAssertSelectedLoanAmountIsDisplayed()
        {
            this.LoanCalculatorPage.AssertSelectedSliderAmountIsDisplayed();
        }

        [When(@"The applicatnt selects a repayment day as a weekend (.*)")]
        public void WhenTheApplicatntSelectsARepaymentDayAsAWeekend(int day)
        {
            ScenarioContext.Current.Add("DayNumber", day.ToString());
        }

        [When(@"The applicatnt selects a repayment day of the week (.*)")]
        public void WhenTheApplicatntSelectsARepaymentDayOfTheWeekThu(string dayOfWeek)
        {
            this.LoanCalculatorPage.SelectRepaymentDayOfWeek(dayOfWeek);
        }


        /// <summary>
        /// If Repayment falls on weekend, then date is set back to previous working day
        /// </summary>
        [Then(@"Assert repayment date is pushed back to nearest weekday")]
        public void ThenAssertRepaymentDateIsPushedBackToNearestWeekday()
        {
            this.LoanCalculatorPage.AssertRepaymentDateRestsBackIfWeekendIsSelected(ScenarioContext.Current["DayNumber"]as string);
        }

        /// <summary>
        /// This asserts if repayment falls on week day, date is kept
        /// </summary>
        /// <param name="DayNumber"></param>
        [Then(@"If first repayment day falls on a weekday (.*), then selected repayment date is kept")]
        public void ThenIfFirstRepaymentDayFallsOnAWeekday(int DayNumber)
        {
            this.LoanCalculatorPage.AssertRepaymentDateIsKeptIfFallsOnAWeeekday(DayNumber.ToString());
        }

        [Then(@"Assert repayment date is a future date")]
        public void ThenAssertRepaymentDateIsAFutureDate()
        {
            this.LoanCalculatorPage.AssertRepaymentDateIsAFutureDate();
        }

    }
}
