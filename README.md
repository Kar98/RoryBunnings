Saturday:
Start time: 16:30
Finished basic setup at 2000

Sunday: 
Started again 13:30
504s at 15:00 :(
Finished at 16:30
Writing up docs/git push afterwards

Total time was 6.5 hours. 
2 hours for getting framework setup, then 2 hours writing up the tests, then 2 for getting the Specflow stuff setup. 

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

I chose 3 test cases, 1 to search for an item, 1 to check that recent searches are updated, 1 is to check if the popular searches are correct
(I mocked the DB call I make). I think these are very common scenarios which are the best to automate. 

Architecture:
PageObjects is how you interact with the page. Each page extends a base method and will have a series of fields that will have an 
attribute associated with it. This attribute controls the findElement method. When the page is instantiated, it will use reflection
to crawl the attributes and assign the values to the fields. It caters for the types string, int, IWebElement. Typically each useful
element on the page will have a field and it's up to the automater to design the tests to call these fields or create methods to 
perform actions on the page (eg, ClickSaveAndClose(), rather than save.Click() waitforElement("xpath/here") close.Click() )

You can set a wait for each page object, if you pass in a By into the pageLocator parameter in the page object's constructor. Saves
having to constantly use wait.until(blah) all the time when you want to know if a page is ready to load.

Note that i needed to have 2 versions of NUnit because it couldnt' find the tests when I downgraded to Specflows version. I have 2 
different test suites because I didn't realise you were looking for Specflow experience so I retrofitted my NUnit tests into Specflow.

Extras:
Mobile testing does work, but the current implementation would need some additional work for a proper live environment. Currently I'm using
a simple bool passed in from the runsettings, but I would need to have a comprehensive look at how the site works in mobile, before building this.
Potentially need to have 2 separate suites if the way the page is built for mobiles is drastically different than the desktop site.
