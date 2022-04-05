Feature: CreditCardSpecflow

Scenario: Verify that wrong credit card number not accepted
	Given the card owner is <cardOwner>
	When I request with card number
	Then the result must invalid

Examples:
	| cardOwner |
	| ek        |
	| e         |
	|           |

Scenario: Verify that wrong CVC not accepted
	Given the CVC is <cvc>
	When I set validations, getting validators
	Then CVC must invalid

Examples:
	| cvc    |
	| 123456 |
	| 12     |
	|        |
	| abc    |
	| 1a2    |

	
Scenario: Verify that wrong expiration date not accepted

Scenario: Verify that wrong card owner information not accepted

Scenario: Verify that card owner field doesnt have card number

Scenario: Verify that expired credit card doesn't accepted

Scenario: Verify that unknown credit card doesn't accepted

Scenario: Verify that valid credit card