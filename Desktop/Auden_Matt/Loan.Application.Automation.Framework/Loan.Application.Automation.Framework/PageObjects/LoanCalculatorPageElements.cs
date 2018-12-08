using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Application.Automation.Framework.PageObjects
{
    public class LoanCalculatorPageElements
    {
        protected internal string LoanCalculator
        {
            get
            {
                return "LoanCalculator";
            }
        }

        protected internal string MinMaxLoanAmount
        {
            get
            {
                return "//div[@class='slider-handle min-slider-handle round']";
            }
        }

        protected internal string SliderHeaderSubtitle
        {
            get
            {
                return "//span[@class='component-slider__header-subtitle']//span[@class='subtitle']";
            }
        }

        protected internal string InstalmentType(string type)
        {
            if(!string.IsNullOrEmpty(type))
            {
                return "//a[@class='component-instalment-type-selector__tabs__tab__link'][text()='" + type + "']";
            }
            else
            {
                throw new Exception("Instalment type not specified");
            }
        }

        protected internal string BtnRepaymentDayPlus
        {
            get
            {
                return "//span[@class='icon icon-A_plusFilled plus']";
            }
        }

        protected internal string FirstRepaymentMultipleDates
        {
            get
            {
                return "//div[@class='component-calculator-date-selector__first-repayment-selector']//label";
            }
        }

        protected internal string RepaymentDateSingleDate
        {
            get
            {
                return "//div[@class='component-calculator-date-selector__first-repayment-selector--single-date']";
            }
        }

        protected internal string DayOfTheWeek(string day)
        {
            return "//a[contains(@class, 'component-calculator-date-selector__day months-selector_day')]//span[text()='" + day + "']";
        }

        protected internal string DayOfTheWeek(int day)
        {
            return "//a[contains(@class, 'component-calculator-date-selector__day weeks-selector_day ')][@data-propvalue='" + day + "']";
        }

        protected internal string SelectedCalculatorLoanValue
        {
            get
            {
                return "//div[@class='value-info-meta']//div[@class='item'][1]//span[@class='integer']";
            }
        }

        protected internal string SliderTrack
        {
            get
            {
                return "//div[@class='slider-track']";
            }
        }

        protected internal string SliderSelectedAmount
        {
            get
            {
                return "//span[@class='slider-display-value component-slider__amount-integer']";
            }
        }

        protected internal string WeeklyRepaymentTab
        {
            get
            {
                return "//a[@class='component-instalment-type-selector__tabs__tab__link'][text()='Weekly']";
            }
        }

        protected internal string DailyRepaymentTab
        {
            get
            {
                return "//a[@class='component-instalment-type-selector__tabs__tab__link'][text()='Days']";
            }
        }

        protected internal string MonthlRepaymentTab
        {
            get
            {
                return "//a[@class='component-instalment-type-selector__tabs__tab__link'][text()='Monthly']";
            }
        }
    }
}
