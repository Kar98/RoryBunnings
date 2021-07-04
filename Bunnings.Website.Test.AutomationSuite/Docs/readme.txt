Start time: 16:30
Finished basic setup at 1800
Started again 13:30

Brief:
Develop 3 test cases for the search functionality with an agile dev environment in mind.

Assumptions:
No existing automation framework.
Need front end and backend tests.
CI CD intentions
Mobile testing needed

Strategy:
I'm assuming there is no automation in place currently and you need a test suite going ahead. It's best to have all your tests
in one spot to avoid having too many libraries to manage. If you need to put it into a test pipeline, it's a nightmare to have to
run the different scripts, then collate, then parse the results and output the test results into a single spot. 

I've chosen Selenium with C#. It does require coding knowledge and is not as lightweight as other tools, but it gives you the flexibility
of implementing API level tests or front end tests. It also allows to be able to make database calls and to even implement your
t-SQLt tests into a single spot. This is helpful when you are running the automation suite as it removes test data dependencies.

Architecture:
PageObjects is how you interact with the page. Each page extends a base method and will have a series of fields that will have an 
attribute associated with it. This attribute controls the findElement method. When the page is instantiated, it will use reflection
to crawl the attributes and assign the values to the fields. It caters for the types string, int, IWebElement. Typically each useful
element on the page will have a field and it's up to the automater to design the tests to call these fields or create methods to 
perform actions on the page (eg, ClickSaveAndClose(), rather than save.Click() waitforElement("xpath/here") close.Click() )