Feature: WeeklyRepayment
	This feature file is for step by step weekely repayment selection by applicant
	
@Weekly
Scenario Outline: Choosing to pay weekly
	Given Loan applicant navigates to Auden home page
	When He clicks on Loans navigation button on the top of the page
	Then He should be redirected to the Loans Calculator Page
	Then He should select Weekely as instalment repayment
	And Should assert that minimum loan is <Minimum> and maximum loan is <Maximum>
	And Assert Selected loan amount is displayed
	When The applicatnt selects a repayment day of the week <DayOfWeek>
	Then Assert repayment date is a future date

	Examples: 
	| Minimum | Maximum | DayOfWeek |
	| 200     | 1000    | 4      |

		## Note: DayOfWeek (4) is a Thursday