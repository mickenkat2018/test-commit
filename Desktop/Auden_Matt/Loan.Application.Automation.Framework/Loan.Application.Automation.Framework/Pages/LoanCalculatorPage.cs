using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loan.Application.Automation.Framework.PageObjects;
using Loan.Application.Webdriver;
using TechTalk.SpecFlow;
using Loan.Application.Common.Framework;
using NUnit.Framework;

namespace Loan.Application.Automation.Framework.Pages
{
    public class LoanCalculatorPage : LoanCalculatorPageElements
    {
        private IDriver driver;
        private CommonMethods commonMethods;

        public LoanCalculatorPage()
        {
            this.driver = ScenarioContext.Current["Driver"] as IDriver;
            this.commonMethods = new CommonMethods();
        }

        public void AssertIsAtLoansCalculatorPage()
        {
            this.driver.WaitForPageLoad(90);
            Assert.True(this.driver.IsElementVisible(this.LoanCalculator));
        }

        /// <summary>
        /// Returns min loan amount
        /// </summary>
        /// <returns></returns>
        public string GetMinLoan()
        {
            return this.driver.ElementValue(this.MinMaxLoanAmount, "aria-valuemin");
        }

        /// <summary>
        /// Returns max loan amout
        /// </summary>
        /// <returns></returns>
        public string GetMaxLoan()
        {
            return this.driver.GetText(this.SliderHeaderSubtitle).Split('-').Last().Trim();
        }

        /// <summary>
        /// Asserts min and max amounts that can be given out
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void AssertMinAndMaxLoan(string min, string max)
        {
            Assert.AreEqual(min, this.GetMinLoan().Trim());
            Assert.AreEqual( "£" + max, this.GetMaxLoan().Trim());
        }

        /// <summary>
        /// Asserts selected amount on the slider is reflected on as intended loan amount
        /// </summary>
        public void AssertSelectedSliderAmountIsDisplayed()
        {
            this.driver.MoveToElement(this.SliderTrack, 400);
            this.driver.WaitAfewSeconds();
            var selectedAmount = this.driver.ElementValue(this.MinMaxLoanAmount, "aria-valuenow");
            Assert.True(this.driver.GetText(this.SliderSelectedAmount).Contains(selectedAmount));
            Assert.AreEqual(this.driver.GetText(this.SelectedCalculatorLoanValue), selectedAmount);
        }

        /// <summary>
        /// Asserts first repayment date can not be set on weekends
        /// </summary>
        /// <param name="SelectedRepaymentDay"></param>
        public void AssertRepaymentDateRestsBackIfWeekendIsSelected(string SelectedRepaymentDay)
        {
            ////Click plus button
            this.driver.ClickAllVisible(this.BtnRepaymentDayPlus);
            this.driver.WaitAfewSeconds(3);
            this.driver.Click(this.DayOfTheWeek(SelectedRepaymentDay));
            this.driver.WaitAfewSeconds(3);
            var SystemResetDate = this.driver.GetText(this.RepaymentDateSingleDate).Split(' ').FirstOrDefault();
            //// Assert the new date is less than the initial requested repayment date
            Assert.Greater(int.Parse(SelectedRepaymentDay), int.Parse(SystemResetDate));
        }

        public void AssertRepaymentDateIsKeptIfFallsOnAWeeekday(string SelectedRepaymentDay)
        {
            ////Click plus button
            this.driver.ClickAllVisible(this.BtnRepaymentDayPlus);
            this.driver.WaitAfewSeconds(3);
            this.driver.Click(this.DayOfTheWeek(SelectedRepaymentDay));
            this.driver.WaitAfewSeconds(3);
            var SystemResetDate = this.driver.GetText(this.RepaymentDateSingleDate).Split(' ').FirstOrDefault();
            //// Assert the new date is less than the initial requested repayment date
            Assert.AreEqual(int.Parse(SelectedRepaymentDay), int.Parse(SystemResetDate));
        }

        public void SelectRepaymentDayOfWeek(string day)
        {
            this.driver.ClickAllVisible(this.BtnRepaymentDayPlus);
            this.driver.WaitAfewSeconds(3);
            this.driver.ClickAllVisible(this.DayOfTheWeek(int.Parse(day)));
        }

        public void SelectInstalmentType(string instalmentType)
        {
            switch (instalmentType)
            {
                case "Weekly":
                    this.driver.ClickAllVisible(this.WeeklyRepaymentTab);
                    this.driver.WaitForPageLoad(30);
                    this.driver.WaitAfewSeconds(3);
                    break;
                case "Daily":
                    this.driver.ClickAllVisible(this.DailyRepaymentTab);
                    this.driver.WaitForPageLoad(30);
                    this.driver.WaitAfewSeconds(3);
                    break;
                default:
                    this.driver.ClickAllVisible(this.MonthlRepaymentTab);
                    this.driver.WaitForPageLoad(30);
                    this.driver.WaitAfewSeconds(3);
                    break;

            }
        }

        public void AssertRepaymentDateIsAFutureDate()
        {
            foreach (var element in this.driver.GetVisibleElements(this.FirstRepaymentMultipleDates))
            {
                Assert.IsTrue(DateTime.Parse(element.Text) > DateTime.Today);
            }
        }
    }
}
