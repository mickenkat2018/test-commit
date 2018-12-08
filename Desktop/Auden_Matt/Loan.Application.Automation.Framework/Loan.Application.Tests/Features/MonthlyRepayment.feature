Feature: MonthlyRepayment
	This feature file is for step by step mothly repayment selection by applicant
	
@Monthly
Scenario Outline: Choosing to pay monthly
	Given Loan applicant navigates to Auden home page
	When He clicks on Loans navigation button on the top of the page
	Then He should be redirected to the Loans Calculator Page
	And Should assert that minimum loan is <Minimum> and maximum loan is <Maximum>
	And Assert Selected loan amount is displayed
	When The applicatnt selects a repayment day as a weekend <Weekend>
	Then Assert repayment date is pushed back to nearest weekday
	And If first repayment day falls on a weekday <Weekday>, then selected repayment date is kept

	Examples: 
	| Minimum | Maximum | Weekend | Weekday |
	| 200     | 1000    | 6       | 3       |


	## Note Weekend (6) falls on 6th Jan 2019 which is a Sunday whilst Weekday (3) = 3rd Jan which is a Thursday
