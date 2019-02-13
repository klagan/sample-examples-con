Feature: GivenASampleTest
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Return HTTP 200 OK
	Given a working webapi
	When I call GET with a value of 101
	Then I receive a response of HTTP 200 OK