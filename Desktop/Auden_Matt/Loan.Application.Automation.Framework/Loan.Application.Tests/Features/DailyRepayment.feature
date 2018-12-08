Feature: DailyRepayment
	This feature file is for step by step daily repayment selection by applicant
	
@Daily
Scenario Outline: Choosing to pay Daily
	Given Loan applicant navigates to Auden home page
	When He clicks on Loans navigation button on the top of the page
	Then He should be redirected to the Loans Calculator Page
	Then He should select Day as instalment repayment
	And Should assert that minimum loan is <Minimum> and maximum loan is <Maximum>
	And Assert Selected loan amount is displayed

	Examples: 
	| Minimum | Maximum | 
	| 200     | 600    |
