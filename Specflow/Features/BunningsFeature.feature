Feature: Bunnings website
	3 basic scenarios

@search
@basic
Scenario: Do a search
	Given I am on the homepage
	When I search for an item
	Then some results should be returned

@search
@basic
Scenario: Check recent searches
	Given I am on the homepage
	When I search for an item bbq and check recent searches
	Then the recent searches should be updated with bbq

@search
@basic
Scenario: Popular searches are updated
	Given I am on the homepage
	When I look at the popular searches
	Then the popular searches match the database