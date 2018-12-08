                              ####  MATT RHODES

#This Extercise is written in .Net C-sharp (C#) as it's what I'm most comfortable with.
#I have used Selenium Webdriver to interact with the web objects and Nunit Framework for assertions
#The Test framework is as BDD framework and used Specflow to achieve this 
#I have also used Page Object Model to implement the backend logic, each Page class implements their coresponding Page element's class
This keeps the code organised and easy to maintain

The solution consits of 4 sub-projects
 - PageObject (All pages and element classes)
 - Driver (Consits driver actions)
 - Common (For all shared methods, classes and/properties)
 - Test project (Consists of features, steps)

The Tests have 3 feature files separated, one for monthly, weekly and Daily. I have separated them for readability and maintainability reasons,
I'am very aware that they can all be encorporated into a single feature file. However, there is only only Code Bindings class for all the 3 feature files, again I could have separated them but for time constraints.

# EXECUTION REQUIREMENSTS
 -Visual Studio
 -Specflow
 -.Net Framework 4.71

All tested and working, I welcome suggestions for improvements :)
 
